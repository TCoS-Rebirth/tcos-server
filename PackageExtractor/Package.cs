using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageExtractor
{
  [StructLayout(LayoutKind.Sequential, Size=44, Pack=1)]
    public struct GlobalHeader
    {
      public uint Signature;
      public Int16 FileVersion;
      public Int16 LicenseeMode;
      public uint PackageFlags;
      public uint NameCount;
      public uint NameOffset;
      public uint ExportCount;
      public uint ExportOffset;
      public uint ImportCount;
      public uint ImportOffset;
      public uint HeritageCount;
      public uint HeritageOffset;
    }

    public struct ImportEntry
    {
      public int ClassPackageName;
      public int ClassName;
      public int PackageReference;
      public int ObjectName;
    }

    public struct ExportEntry
    {
      public int ClassReference;
      public int SuperReference;
      public int PackageReference;
      public int ObjectName;
      public int ObjectFlags;
      public int SerialSize;
      public int SerialOffset;
    }

  class Package
  {
    public string[] NameTable { get; set; }
    public ExportEntry[] ExportTable { get; set; }
    public ImportEntry[] ImportTable { get; set; }
    private  GlobalHeader Header;

    static public Dictionary<int, string> LocalizedStrings = new Dictionary<int, string> ();
    public List<SBObject> Objects 
    {get
      { return PackageObjects; }
    }

    private string packageFileFullName;
    private List<SBObject> PackageObjects;

    public Package (string fileFullName)
    {
      packageFileFullName = fileFullName;
    }

    public void Load (string exportFile = "")
    {
      SBFileReader fileReader = new SBFileReader (packageFileFullName);
      fileReader.Read (out Header, Marshal.SizeOf (Header));

      Console.WriteLine ("Signature: {0}", Header.Signature);
      Console.WriteLine ("File Version: {0}", Header.FileVersion);
      Console.WriteLine ("Licensee Mode: {0}", Header.LicenseeMode);
      Console.WriteLine ("Package Flags: {0}", Header.PackageFlags);
      Console.WriteLine ("Name Count: {0}", Header.NameCount);
      Console.WriteLine ("Name Offset: {0}", Header.NameOffset);
      Console.WriteLine ("Export Count: {0}", Header.ExportCount);
      Console.WriteLine ("Import Count: {0}", Header.ImportCount);
      Console.WriteLine ("Heritage Count: {0}", Header.HeritageCount);

      
      fileReader.Seek (Header.NameOffset, SeekOrigin.Begin);

      NameTable = new string[Header.NameCount];
      //----------- READ NAME TABLE --------------
      Console.WriteLine ("Read name table...");

      for (uint k = 0; k < Header.NameCount; ++k)
      {
        int nameSize = fileReader.ReadByte ();
        //Console.WriteLine ("Name size {0}", nameSize);
        byte[] stringArray;
        stringArray = fileReader.ReadBytes (nameSize);
        uint objectFlags = fileReader.ReadUInt32 ();
        string name = System.Text.Encoding.ASCII.GetString (stringArray);
        //Console.WriteLine ("Object Name: {0}", name);
        //Console.WriteLine ("Object Flags: {0}", objectFlags);
        NameTable[k] = name;
      }
        

      //------------- READ IMPORT TABLE -----------------------------
      Console.WriteLine ("Read import table...");
      ImportTable = new ImportEntry[Header.ImportCount];
      StringBuilder importText = new StringBuilder ();
      fileReader.Seek (Header.ImportOffset, SeekOrigin.Begin);
      for (uint k = 0; k < Header.ImportCount; ++k)
      {
        ImportEntry entry;
        entry.ClassPackageName = fileReader.ReadIndex ();
        entry.ClassName = fileReader.ReadIndex ();
        entry.PackageReference = fileReader.ReadInt32 ();
        entry.ObjectName = fileReader.ReadIndex ();

        ImportTable[k] = entry;
        importText.AppendLine ("-----------------------------");
        importText.AppendLine ("Class Package name: " + NameTable[entry.ClassPackageName]);
        importText.AppendLine ("Class name: " + NameTable[entry.ClassName]);
        importText.AppendLine ("Package: " + FindReferenceName (entry.PackageReference));
        importText.AppendLine ("Object name: " + NameTable[entry.ObjectName]);
      }
      if (exportFile != "")
        System.IO.File.WriteAllText (exportFile + ".import.txt", importText.ToString ());

      //----------- READ EXPORT TABLE ------------------------
      Console.WriteLine ("Read export table...");
      ExportTable = new ExportEntry[Header.ExportCount];
      fileReader.Seek (Header.ExportOffset, SeekOrigin.Begin);
      StringBuilder exportTableText = new StringBuilder ();
      for (uint k = 0; k < Header.ExportCount; ++k)
      {
        ExportEntry entry;
        entry.ClassReference = fileReader.ReadIndex ();
        entry.SuperReference = fileReader.ReadIndex ();
        entry.PackageReference = fileReader.ReadInt32 ();
        entry.ObjectName = fileReader.ReadIndex ();
        entry.ObjectFlags = fileReader.ReadInt32 ();
        entry.SerialSize = fileReader.ReadIndex ();
        entry.SerialOffset = fileReader.ReadIndex ();
        ExportTable[k] = entry;

        exportTableText.AppendLine ("-----------------------------");
        exportTableText.AppendLine ("Class name: " + FindReferenceName(entry.ClassReference));
        exportTableText.AppendLine ("Super Class name: " + FindReferenceName(entry.SuperReference));
        exportTableText.AppendLine ("Package: " + FindReferenceName (entry.PackageReference));
        exportTableText.AppendLine ("Object name: " + NameTable[entry.ObjectName]);
      }

      if (exportFile != "")
        System.IO.File.WriteAllText (exportFile + ".exporttable.txt", exportTableText.ToString ());

      //--------------- READ ALL OBJECTS EXPORTED -----------------------
      Console.WriteLine ("Read exported objects...");
      StringBuilder exportText = new StringBuilder ();
      PackageObjects = new List<SBObject>();

      SBObjectReader objectReader = new SBObjectReader (this);
      foreach (ExportEntry entry in ExportTable)
      {
        SBObject obj = objectReader.ReadObject (fileReader, entry, true);
        //Console.WriteLine (obj.ToString());
        PackageObjects.Add(obj);
        exportText.AppendLine (obj.ToString ());
        //if (Console.ReadKey ().KeyChar == 'q')
        //  break;
      }

      if (exportFile != "")
      {
        Console.WriteLine ("Write exported objects...");
        System.IO.File.WriteAllText (exportFile + ".export.txt", exportText.ToString ());
      }

      SBObject[] result = Array.FindAll(PackageObjects.ToArray(), o => o.Name.Contains("SBWorldPortal"));
      //result = Array.FindAll(result, o => o.Properties.ContainsKey("NavigationTag"));
      //result = Array.FindAll(result, o => o.Properties["Tag"].Value == "GildedLeafEntranceEast");

      SBObject[] result2 = Array.FindAll(PackageObjects.ToArray(), o => o.Name.Contains("PlayerStart"));
      result2 = Array.FindAll(result2, o => o.Properties.ContainsKey("NavigationTag"));
      //result2 = Array.FindAll(result2, o => o.Properties["NavigationTag"].Value == "GildedLeafEntranceEast");
      StringBuilder locations = new StringBuilder();
      foreach (SBObject o in result)
      {
        //Console.WriteLine(o.ToString());
        //locations.Append(o.Properties["NavigationTag"].Value + ": " + o.Properties["Location"].Value + "  \r\n");
       // Console.WriteLine(o.Properties["NavigationTag"].Value + ": " + o.Properties["Location"].Value);  
        //Console.ReadKey();
      }
      if (exportFile != "")
        System.IO.File.WriteAllText(exportFile + "_spawns.export.txt", locations.ToString());

      Console.WriteLine ("Done");
    }

    public string FindReferenceName (int reference, bool showAncestors = true)
    {
      if (reference > 0)
      {
        string result = NameTable[ExportTable[reference - 1].ObjectName];
        if (showAncestors)
        {
          string ancestors = FindReferenceName(ExportTable[reference - 1].PackageReference);
          if (ancestors != "null")
            result = ancestors + "." + result;
        }
        return result;
      }
      else if (reference < 0)
      {
        string result = NameTable[ImportTable[-reference - 1].ObjectName];
        if (showAncestors)
        {
          string ancestors = FindReferenceName(ImportTable[-reference - 1].PackageReference);
          if (ancestors != "null")
            result = ancestors + "." + result;
        }

        return result;
      }
      else
        return "null";
    }

    public string GetLocalizedString (int stringID)
    {
      return LocalizedStrings[stringID];
    }

    static public void ReadLocalizedStrings (string pathToFile)
    {
      SBFileReader fileReader = new SBFileReader (pathToFile);
      fileReader.ReadInt32 ();//skip "header"

      int arraySize = fileReader.ReadInt32 ();

      for (int i = 0; i < arraySize; ++i)
      {
        int recordId = fileReader.ReadInt32 ();
        int languageNumber = fileReader.ReadInt32 ();
        int stringLength = fileReader.ReadInt32 ();
        string data = System.Text.Encoding.UTF8.GetString (fileReader.ReadBytes (stringLength));
        fileReader.ReadByte ();
        fileReader.ReadByte ();
        fileReader.ReadByte ();
        fileReader.ReadByte ();
        fileReader.ReadByte ();
        if(languageNumber == 1)
          LocalizedStrings.Add (recordId, data);


        //Console.WriteLine(recordId + ": " + data + "("+languageNumber+")");
        //Console.ReadKey ();
      }
    }

    public void ToXML(string path)
    {
      XDocument result = new XDocument();
      result.Add(new XComment("Generated by TCoS-Rebirth PackageExtractor"));
      XElement root = new XElement("Map");
      root.SetAttributeValue("name", "PT_Hawksmouth");
      result.Add(root);
      /*
      SBObject[] playerStarts = Array.FindAll(PackageObjects.ToArray(), o => o.Name.Contains("SBWorldPortal"));
      //playerStarts = Array.FindAll(playerStarts, o => o.Properties.ContainsKey("NavigationTag"));
      StringBuilder locations = new StringBuilder();
      foreach (SBObject o in playerStarts)
      {
        XElement xmlPlayerStart = new XElement("SBWorldPortal");
        xmlPlayerStart.SetAttributeValue("name", o.Name);
        xmlPlayerStart.Add(new XElement("NavigationTag", o.Properties["NavigationTag"].Value));
        xmlPlayerStart.Add(new XElement("Location", o.Properties["Location"].Value));
        xmlPlayerStart.Add(new XElement("CollisionRadius", o.Properties["CollisionRadius"].Value));
        root.Add(xmlPlayerStart);
      }
      */
      foreach(SBObject sbobject in PackageObjects)
      {
        root.Add(sbobject.ToXML());
      }
       

      try
      {
        result.Save(path);
      }catch (ArgumentException)
      { }
    }
  }
}
