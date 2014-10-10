using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageExtractor
{

  public class SBProperty
  {
    public PropertyType Type;
    public string Name;
    public string Value;
    public string ArrayIndex;
    public string StructName;
    public string Size;

    public override string ToString ()
    {
      StringBuilder builder = new StringBuilder ();
      builder.AppendLine ("Name: " + Name);
      builder.AppendLine ("Type: " + Type + " (Size: " + Size +")");
      builder.AppendLine ("Value: " + Value);
      if (ArrayIndex != null)
        builder.AppendLine ("ArrayIndex: " + ArrayIndex);
      if (StructName != null)
        builder.AppendLine ("StructName: " + StructName);

      return builder.ToString ();
    }
  }

  public class SBObject
  {
    public string Name;
    public string ClassName;
    public string SuperClassName;
    public int Size;
    public string Package;
    //Name => SBproperty
    public Dictionary<string,SBProperty> Properties;

    public override string ToString ()
    {
      StringBuilder builder = new StringBuilder ();
      builder.AppendLine ("--------------------------------------------");
      builder.AppendLine ("Object name: " + Name);
      builder.AppendLine ("Object class: " + ClassName);
      builder.AppendLine ("Object super class: " + SuperClassName);
      builder.AppendLine ("Object Package: " + Package + "\n");
      builder.AppendLine ("Properties:");
      foreach (KeyValuePair<string, SBProperty> prop in Properties)
      {
        builder.AppendLine (prop.Value.ToString());
      }
      builder.AppendLine ("");

      return builder.ToString();
    }
  }

  class SBObjectReader
  {
    private Package package;

    public SBObjectReader (Package package)
    {
      this.package = package;
    }

    public SBObject ReadObject (SBFileReader fileReader, ExportEntry entry)
    {
      SBObject result = new SBObject ();
      result.Name = package.NameTable[entry.ObjectName];
      result.Properties = new Dictionary<string, SBProperty> ();
      result.ClassName = package.FindReferenceName(entry.ClassReference);
      result.Package = package.FindReferenceName(entry.PackageReference);
      result.Size = entry.SerialSize;
      result.SuperClassName = package.FindReferenceName (entry.SuperReference);

      bool hasExecutionStack = false;

      //Detect stack
      if ((entry.ObjectFlags & ObjectFlags.RF_HasStack) != 0)
        hasExecutionStack = true;

      if (entry.SerialSize <= 0)
        return result;

      //Read object data if present

      fileReader.Seek (entry.SerialOffset, SeekOrigin.Begin);
      //If object has a stack, read it but do nothing with it
      if (hasExecutionStack)
      {
        int stateFrameNode;
        int dummy;
        stateFrameNode = fileReader.ReadIndex ();
        dummy = fileReader.ReadIndex ();
        fileReader.ReadInt64 ();
        fileReader.ReadInt32 ();
        if (stateFrameNode != 0)
        {
          dummy = fileReader.ReadIndex ();
        }
      }

      //Name index of the next property to be read
      int propertyNameIndex;

      //.::Loop through all the properties of the current object::.
      do
      {
        SBProperty property = new SBProperty ();

        propertyNameIndex = fileReader.ReadIndex ();
        property.Name = package.NameTable[propertyNameIndex];

        //Detect end of property list
        if (package.NameTable[propertyNameIndex] == "DRFORTHEWIN" || package.NameTable[propertyNameIndex] == "None")
          break;

        //Read the property info byte
        byte infoByte = fileReader.ReadByte ();

        //Parse property info byte
        byte type = (byte)(infoByte & 0x0F);
        byte size = (byte)((infoByte & 0x70) >> 4);
        byte arrayFlag = (byte)(infoByte & 0x80);

        //Arrayflag can also be the value of the property if it's a boolean property (see doc)
        if (arrayFlag != 0 && (PropertyType)type != PropertyType.BooleanProperty)
        {
          //Read arrayIndex, array index format is in the doc
          int arrayIndex = fileReader.ReadByte ();

          if (arrayIndex >= 128 || arrayIndex == 0)
          {
            fileReader.Seek (-1, SeekOrigin.Current);
            arrayIndex = fileReader.ReadInt16 ();

            if (arrayIndex >= 16384)
            {
              fileReader.Seek (-2, SeekOrigin.Current);
              arrayIndex = fileReader.ReadInt32 () - (0xC0 << 24);
            }
            else
              arrayIndex -= (0x80 << 8);
          }
          property.ArrayIndex = arrayIndex.ToString ();
        }
        //Display value if not array flag but boolean value
        else if ((PropertyType)type == PropertyType.BooleanProperty)
          property.Value = (arrayFlag > 0 ? "True" : "False");

        //If type = struct, you have to read its name
        if ((PropertyType)type == PropertyType.StructProperty)
        {
          int structNameIndex;
          structNameIndex = fileReader.ReadIndex ();
          property.StructName = package.NameTable[structNameIndex];
        }

        int realSize = 0;
        //Read real size (see doc)
        if (size == 0)
          realSize = 1;
        else if (size == 1)
          realSize = 2;
        else if (size == 2)
          realSize = 4;
        else if (size == 3)
          realSize = 12;
        else if (size == 4)
          realSize = 16;
        else if (size == 5)
          realSize = fileReader.ReadByte ();
        else if (size == 6)
          realSize = fileReader.ReadInt16 ();
        else if (size == 7)
          realSize = fileReader.ReadInt32 ();

        property.Type = (PropertyType)type;
        property.Size = realSize.ToString ();

        //Handle specific properties
        switch ((PropertyType)type)
        {
          case PropertyType.ByteProperty:
            property.Value = fileReader.ReadByte ().ToString ();
            break;

          case PropertyType.IntegerProperty:
            property.Value = fileReader.ReadInt32 ().ToString ();
            break;

          case PropertyType.FloatProperty:
            property.Value = fileReader.ReadFloat ().ToString();
            break;

          case PropertyType.NameProperty:
            property.Value = package.NameTable[fileReader.ReadIndex()];
            break;

          case PropertyType.StrProperty:
            int stringSize;
            stringSize = fileReader.ReadIndex ();

            byte[] stringBytes = fileReader.ReadBytes (stringSize);
            string stringValue = System.Text.Encoding.ASCII.GetString (stringBytes);
            property.Value = stringValue;
            break;

          case PropertyType.ObjectProperty:
            int objectReference = fileReader.ReadIndex ();
            property.Value = package.FindReferenceName (objectReference) + " (reference)";
            break;

           //Structs are a special case
          case PropertyType.StructProperty:

            if (property.StructName == "Vector")
              property.Value = "(X=" + fileReader.ReadFloat () + "; Y=" + fileReader.ReadFloat () + "; Z=" + fileReader.ReadFloat () + ")";
            else if (property.StructName == "Rotator")
              property.Value = "(Pitch=" + fileReader.ReadInt32 () + "; Yaw=" + fileReader.ReadInt32 () + "; Roll=" + fileReader.ReadInt32 () + ")";
            else if (property.StructName == "Color")
              property.Value = "(R=" + fileReader.ReadByte () + "; G=" + fileReader.ReadByte () + "; B=" + fileReader.ReadByte () + "; A=" + fileReader.ReadByte () + ")";
            else if (property.StructName == "LocalizedString")
              property.Value = package.GetLocalizedString (fileReader.ReadInt32 ());
            else
              fileReader.Seek (realSize, SeekOrigin.Current);
            break;

          default:
            fileReader.Seek (realSize, SeekOrigin.Current);
            break;
        }

        bool success = false;
        string key = property.Name;
        int cpt = 0;
        do
        {
          try
          {
            result.Properties.Add (key, property);
            success = true;
          }
          catch (ArgumentException)
          {
            success = false;
            key = key + cpt.ToString ();
            ++cpt;
          }
        } while (!success);

      } while (package.NameTable[propertyNameIndex] != "DRFORTHEWIN" && package.NameTable[propertyNameIndex] != "None" && fileReader.Position < entry.SerialOffset + entry.SerialSize);

      return result;
    }
  }
}
