using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    public Dictionary<string, SBProperty> Array = new Dictionary<string, SBProperty>(); //If the property is an ArrayProperty, this member is filled

    public override string ToString ()
    {
      StringBuilder builder = new StringBuilder ();
      builder.AppendLine ("Name: " + Name);
      builder.AppendLine ("Type: " + Type + " (Size: " + Size +")");
      if (Array.Count == 0)
        builder.AppendLine ("Value: " + Value);
      if (ArrayIndex != null)
        builder.AppendLine ("ArrayIndex: " + ArrayIndex);
      if (StructName != null)
        builder.AppendLine ("StructName: " + StructName);
      if (Array.Count>0)
      {
        builder.AppendLine("Array values:");
        builder.AppendLine("BEGIN ARRAY");
        foreach(KeyValuePair<string,SBProperty> pair in Array)
          builder.AppendLine(pair.Value.ToString());
        builder.AppendLine("END ARRAY");
        
      }

      return builder.ToString ();
    }

    public XElement ToXML()
    {
      string xmlName;
      if (Name[0] >= '0' && Name[0] <= '9')
        xmlName = "T" + Name;
      else xmlName = Name;
      XElement result = new XElement(Type.ToString(), Value);
      result.SetAttributeValue("name", xmlName);
      if (StructName != null)
        result.SetAttributeValue("StructName", StructName);

      if (Array.Count > 0)
      {
        foreach (KeyValuePair<string, SBProperty> pair in Array)
        {
          result.Add(pair.Value.ToXML());
        }
      }
      return result;
    }
  }

  public class SBObject
  {
    public SBClass Class;
    public string Name;
    public string ClassName;
    public string SuperClassName;
    public int Size;
    public string Package;
    public List<ObjectFlags> Flags;
    //Name => SBproperty
    public Dictionary<string,SBProperty> Properties;

    public override string ToString ()
    {
      StringBuilder builder = new StringBuilder ();
      builder.AppendLine ("--------------------------------------------");
      builder.AppendLine ("Object name: " + Name);
      builder.AppendLine ("Object class: " + ClassName);
      builder.AppendLine ("Object super class: " + SuperClassName);
      builder.AppendLine ("Object Package: " + Package );
      builder.AppendLine ("Object Size: " + Size);
      if (Flags.Count > 0)
      {
        builder.Append ("Object Flags: ");
        foreach (ObjectFlags flag in Flags)
          builder.Append (flag.ToString() + "; ");
      }
      builder.AppendLine ("");
      builder.AppendLine ("Class details: ");
      builder.AppendLine (Class.ToString ());
      if (Properties.Count > 0)
      {
        builder.AppendLine ("Properties (" + Properties.Count + "):");
        foreach (KeyValuePair<string, SBProperty> prop in Properties)
        {
          builder.AppendLine (prop.Value.ToString ());
        }
      }
      else
        builder.AppendLine ("No Properties");

      return builder.ToString();
    }

    public XElement ToXML()
    {
      XElement result = new XElement(ClassName);
      /*
      if (ClassName != "")
        result = new XElement(ClassName.Substring(0, ClassName.IndexOf(' ')));
      else result = new XElement("NullClass");*/
      result.SetAttributeValue("name", Name);
      if (Properties.Count > 0)
      {
        XElement properties = new XElement("Properties");
        foreach (KeyValuePair<string, SBProperty> prop in Properties)
          properties.Add(prop.Value.ToXML());
        result.Add(properties);
      }
      return result;
    }
  }

  public class SBClass
  {
    public SBClass Ancestor;
    public string Name;
    //Name => Value
    public Dictionary<string, string> Fields;

    //temp
    public string HexaDump;

    public SBClass ()
    {
      Name = "null";
      Fields = new Dictionary<string, string> ();
    }

    public override string ToString ()
    {
      StringBuilder builder = new StringBuilder ();
      builder.AppendLine ("Class name: " + Name);
      if (Ancestor != null)
      {
        builder.AppendLine ("");
        builder.AppendLine ("Class ancestor: " + Ancestor.Name);
        builder.Append (Ancestor.FieldsToString ());
        builder.AppendLine ("");
      }
      else
        builder.AppendLine ("Class ancestor: null");

      builder.AppendLine (FieldsToString ());

      return builder.ToString ();
    }

    public string FieldsToString ()
    {
      StringBuilder builder = new StringBuilder ();
      if (Fields.Count > 0)
      {
        builder.AppendLine ("Fields: ");
        foreach (KeyValuePair<string, string> keyValue in Fields)
        {
          builder.AppendLine (keyValue.Key + ": " + keyValue.Value);
        }
      }

      return builder.ToString ();
    }
  }

  class SBObjectReader
  {
    private Package package;

    public SBObjectReader (Package package)
    {
      this.package = package;
    }

    public SBObject ReadObject (SBFileReader fileReader, ExportEntry entry, bool showAncestors = true)
    {
      SBObject result = new SBObject ();
      result.Class = new SBClass ();
      result.Name = package.NameTable[entry.ObjectName];
      result.Flags = new List<ObjectFlags> ();
      result.Properties = new Dictionary<string, SBProperty> ();
      result.ClassName = package.FindReferenceName(entry.ClassReference, showAncestors);
      result.Package = package.FindReferenceName(entry.PackageReference, showAncestors);
      result.Size = entry.SerialSize;
      result.SuperClassName = package.FindReferenceName(entry.SuperReference, showAncestors);

      bool hasExecutionStack = false;

      //Detect stack
      if ((entry.ObjectFlags & (int)ObjectFlags.RF_HasStack) != 0)
      {
        hasExecutionStack = true;
        result.Flags.Add (ObjectFlags.RF_HasStack);
      }

      //Currently we cannot read null class
      if (entry.SerialSize <= 0 || result.ClassName == "null")
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

      ReadProperties(fileReader, result, entry, showAncestors);

      //Try to read class
 
      string className; ;

      if (result.ClassName.Contains("("))
        className = result.ClassName.Substring (0, result.ClassName.IndexOf ("(") - 1).Trim ();
      else className = result.ClassName;
          
      switch (className)
      {
        case "Const":
          result.Class = ReadConstClass(fileReader);
          break;
        case "Enum":
          result.Class = ReadEnumClass(fileReader);
          break;
        case "Property":
          result.Class = ReadPropertyClass(fileReader);
          break;
        case "ByteProperty":
          result.Class = ReadBytePropertyClass(fileReader);
          break;
        case "ObjectProperty":
          result.Class = ReadObjectPropertyClass(fileReader);
          break;
        case "FixedArrayProperty":
          result.Class = ReadFixedArrayPropertyClass(fileReader);
          break;
        case "ArrayProperty":
          result.Class = ReadArrayPropertyClass(fileReader);
          break;
        case "MapProperty":
          result.Class = ReadMapPropertyClass(fileReader);
          break;
        case "ClassProperty":
          result.Class = ReadClassPropertyClass(fileReader);
          break;
        case "StructProperty":
          result.Class = ReadStructPropertyClass(fileReader);
          break;
        case "IntProperty":
          result.Class = ReadIntPropertyClass(fileReader);
          break;
        case "BoolProperty":
          result.Class = ReadBoolPropertyClass(fileReader);
          break;
        case "FloatProperty":
          result.Class = ReadFloatPropertyClass(fileReader);
          break;
        case "NameProperty":
          result.Class = ReadNamePropertyClass(fileReader);
          break;
        case "StrProperty":
          result.Class = ReadStrPropertyClass(fileReader);
          break;
        case "StringProperty":
          result.Class = ReadIntPropertyClass(fileReader);
          break;
        case "Struct":
          result.Class = ReadStructClass(fileReader);
          break;
        case "Function":
          result.Class = ReadFunctionClass(fileReader);
          break;
        case "State":
          result.Class = ReadStateClass(fileReader);
          break;
        case "null":
        case "None":
          result.Class = ReadNullClass(fileReader);
          break;
        default:
          //Console.WriteLine ("Not a base unreal class");
          break;
      }

      return result;
    }

    private void ReadProperties (SBFileReader fileReader, SBObject result, ExportEntry entry, bool showAncestors)
    {
      //Name index of the next property to be read
      int propertyNameIndex;

      //.::Loop through all the properties of the current object::.
      do
      {
        SBProperty property = new SBProperty ();

        propertyNameIndex = fileReader.ReadIndex ();
        try
        {
          property.Name = package.NameTable[propertyNameIndex];
        }catch(IndexOutOfRangeException)
        {
          Console.WriteLine("Error: failed to read entire package.");
          break;
        }

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
            property.Value = fileReader.ReadFloat ().ToString ();
            break;

          case PropertyType.NameProperty:
            property.Value = package.NameTable[fileReader.ReadIndex ()];
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
            property.Value = package.FindReferenceName(objectReference, showAncestors) /*+ " (reference)"*/;
            break;

          //Structs are a special case
          case PropertyType.StructProperty:

            if (property.StructName == "Vector")
              property.Value = fileReader.ReadFloat () + ";" + fileReader.ReadFloat () + ";" + fileReader.ReadFloat ();
            else if (property.StructName == "Rotator")
              property.Value = "(Pitch=" + fileReader.ReadInt32 () + "; Yaw=" + fileReader.ReadInt32 () + "; Roll=" + fileReader.ReadInt32 () + ")";
            else if (property.StructName == "Color")
              property.Value = "(R=" + fileReader.ReadByte () + "; G=" + fileReader.ReadByte () + "; B=" + fileReader.ReadByte () + "; A=" + fileReader.ReadByte () + ")";
            else if (property.StructName == "LocalizedString")
            {
              property.Value = package.GetLocalizedString(fileReader.ReadInt32());
              //WIP Reversing ArrayProperties
              if (result.Name.Contains("InteractiveLevelElement"))
                Console.WriteLine("Description: " + property.Value + "\n");
              
            }
            else
              fileReader.Seek(realSize, SeekOrigin.Current);
            break;
          case PropertyType.ArrayProperty:
            //WIP Reversing "Actions" arrayproperty of InteractiveLevelElement
            if ((result.Name.Contains("InteractiveLevelElement") && property.Name.Contains("Actions")))
            {
              property.StructName = "MenuInteraction";
              int bytesReaded = 0;
              Console.WriteLine("Owner Name: " + result.Name);
              Console.WriteLine("ArrayProperty name: " + property.Name);
              Console.WriteLine("ArrayProperty size (in bytes): " + realSize);
              int indexSize = 0;
              int numElementsInArrayPropery = fileReader.ReadIndex(ref indexSize);
              bytesReaded += indexSize;
              Console.WriteLine("Num elements in ArrayProperty: " + numElementsInArrayPropery);

              int fieldName = 0;
              int fieldSize = 0;
              int fieldValue = 0;

              //Loop into arrayproperty's elements
              for (int k = 0; k < numElementsInArrayPropery; ++k)
              {
                fieldName = fileReader.ReadIndex(ref indexSize);
                Console.WriteLine("Field Name: " + package.NameTable[fieldName]);//MenuOptions
                bytesReaded += indexSize;


                fieldSize = fileReader.ReadIndex(ref indexSize);
                bytesReaded += indexSize;
                Console.WriteLine("Field Size: " + fieldSize);//1 (that's why ReadByte() just below)

                fieldValue = fileReader.ReadByte();
                ++bytesReaded;
                Console.WriteLine("Field Value: " + fieldValue);//5 <=> RMO_OPENDOOR

                SBProperty menuOption = new SBProperty();
                menuOption.Name = package.NameTable[fieldName];
                menuOption.Size = fieldSize.ToString();
                menuOption.Value = fieldValue.ToString();
                menuOption.Type = PropertyType.ByteProperty;
                property.Array.Add(package.NameTable[fieldName], menuOption);

                //--------------------
                fieldName = fileReader.ReadIndex(ref indexSize);
                Console.WriteLine("Field Name: " + package.NameTable[fieldName]);//StackedActions
                bytesReaded += indexSize;

                int unknownData = fileReader.ReadIndex(ref indexSize);
                bytesReaded += indexSize;
                Console.WriteLine("???: " + unknownData); //41

                int numElements = fileReader.ReadIndex(ref indexSize);
                bytesReaded += indexSize;
                Console.WriteLine("Num elements: " + numElements);//1

                SBProperty stackedAction = new SBProperty();
                stackedAction.Name = package.NameTable[fieldName];
                stackedAction.Size = numElements.ToString();
                stackedAction.Type = PropertyType.ArrayProperty;
                //Loop into StackedActions's elements
                for (int i = 0; i < numElements; ++i)
                {
                  fieldValue = fileReader.ReadIndex(ref indexSize);
                  bytesReaded += indexSize;
                  Console.WriteLine("Field value: " + package.FindReferenceName(fieldValue, false));//Interaction_Action17
                  SBProperty stackedActionEntry = new SBProperty();
                  stackedActionEntry.Name = stackedAction.Name + i;
                  stackedActionEntry.Value = package.FindReferenceName(fieldValue, false);
                  stackedActionEntry.Type = PropertyType.ObjectProperty;
                  stackedAction.Array.Add(stackedAction.Name + i, stackedActionEntry);
                }
                property.Array.Add(stackedAction.Name, stackedAction);

                //-------------------
                fieldName = fileReader.ReadIndex(ref indexSize);
                Console.WriteLine("Field Name: " + package.NameTable[fieldName]);//Requirements
                bytesReaded += indexSize;

                unknownData = fileReader.ReadIndex(ref indexSize);
                bytesReaded += indexSize;
                Console.WriteLine("???: " + unknownData);

                numElements = fileReader.ReadIndex(ref indexSize);
                bytesReaded += indexSize;
                Console.WriteLine("Num elements: " + numElements);//1
                SBProperty requirements = new SBProperty();
                requirements.Name = package.NameTable[fieldName];
                requirements.Type = PropertyType.ArrayProperty;
                requirements.Size = numElements.ToString();
                //Loop into Requirements's elements
                for (int i = 0; i < numElements; ++i)
                {
                  fieldValue = fileReader.ReadIndex(ref indexSize);
                  bytesReaded += indexSize;
                  Console.WriteLine("Field value: " + package.FindReferenceName(fieldValue));//Interaction_Action17
                  SBProperty requirementsEntry = new SBProperty();
                  requirementsEntry.Name = requirements.Name + i;
                  requirementsEntry.Value = package.FindReferenceName(fieldValue);
                  requirementsEntry.Type = PropertyType.ObjectProperty;
                  requirements.Array.Add(requirementsEntry.Name, requirementsEntry);
                }
                property.Array.Add(requirements.Name, requirements);

              }//For loop through "Action" ArrayProperty elements

              fileReader.Seek(-bytesReaded, SeekOrigin.Current);
              
              byte[] blob = fileReader.ReadBytes(realSize);
              Console.WriteLine(BitConverter.ToString(blob));

              
            }
            /*else if(property.Name.Contains("Requirements"))
            {
              Console.WriteLine("Array size (bytes): " + realSize );
              int indexSize = 0;
              int supposedArraySize = fileReader.ReadIndex(ref indexSize);
              Console.WriteLine("array size: " + supposedArraySize);
              int supposedObjectReference = fileReader.ReadIndex(ref indexSize);
              Console.WriteLine("Index size: " + indexSize);
              Console.WriteLine("SUpposed reference: " + package.FindReferenceName(supposedObjectReference, true));
              Console.WriteLine("Last byte: " + fileReader.ReadByte());
            }*/
            else if ((result.Name.Contains("Interaction_Action") && property.Name.Contains("Actions")))
            {
              property.StructName = "Content_Event";
              Console.WriteLine(result.Name);
              int indexSize = 0;
              int bytesReaded = 0;
              
              int arraySize = fileReader.ReadIndex(ref indexSize);
              bytesReaded += indexSize;

              for (int i = 0; i < arraySize; ++i)
              {
                int supposedReference = fileReader.ReadIndex(ref indexSize);
                bytesReaded += indexSize;
                Console.WriteLine("Value: " + package.FindReferenceName(supposedReference));
                Console.WriteLine("Index size: " + indexSize);
                SBProperty contentEvent = new SBProperty();
                contentEvent.Name = property.Name + i;
                contentEvent.Value = package.FindReferenceName(supposedReference);
                contentEvent.Type = PropertyType.ObjectProperty;
                property.Array.Add(contentEvent.Name, contentEvent);
              }

              fileReader.Seek(-bytesReaded, SeekOrigin.Current);

              byte[] blob = fileReader.ReadBytes(realSize);
              Console.WriteLine(BitConverter.ToString(blob));

             // Console.ReadKey();
            }
            else
              fileReader.Seek(realSize, SeekOrigin.Current);
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
    }

    //Unreal specific classes readers
    private SBClass ReadFieldClass (SBFileReader reader)
    {
      int superField = reader.ReadIndex ();
      int next = reader.ReadIndex ();
      SBClass field = new SBClass();
      field.Name = "Field";
      field.Fields.Add("SuperField", superField.ToString());
      field.Fields.Add ("Next", next.ToString());
      return field;
    }

    private SBClass ReadConstClass (SBFileReader reader)
    {
      SBClass constClass = new SBClass ();
      constClass.Ancestor = ReadFieldClass (reader);
      constClass.Name = "Const";

      int size = reader.ReadIndex ();
      string constant = System.Text.Encoding.ASCII.GetString (reader.ReadBytes (size));
      constClass.Fields.Add ("Size", size.ToString());
      constClass.Fields.Add ("Constant", constant);
      return constClass;
    }

    private SBClass ReadEnumClass (SBFileReader reader)
    {
      SBClass enumClass = new SBClass ();
      enumClass.Name = "Enum";
      enumClass.Ancestor = ReadFieldClass (reader);
      int arraySize = reader.ReadIndex ();
      enumClass.Fields.Add ("ArraySize", arraySize.ToString ());
      for (int i = 0; i < arraySize; ++i)
      {
        enumClass.Fields.Add ("ElementName"+i, package.NameTable[reader.ReadIndex ()]);
      }

      return enumClass;
    }

    private SBClass ReadPropertyClass (SBFileReader reader)
    {
      SBClass property = new SBClass ();
      property.Name = "Property";
      property.Ancestor = ReadFieldClass (reader);

      Int16 arrayDimension = reader.ReadInt16 ();
      Int16 elementSize = reader.ReadInt16 ();
      Int32 propertyFlags = reader.ReadInt32 ();
      int category = reader.ReadIndex ();
      property.Fields.Add ("ArrayDimension", arrayDimension.ToString ());
      property.Fields.Add ("ElementSize", elementSize.ToString ());
      property.Fields.Add ("PropertyFlags", propertyFlags.ToString ());
      property.Fields.Add ("Category", package.NameTable[category]);
      if ( (propertyFlags & (int)PropertyFlags.CPF_Net) != 0)
      {
        int replicationOffset = reader.ReadInt16 ();
        property.Fields.Add ("Replication Offset", replicationOffset.ToString ());
      }

      return property;
    }

    private SBClass ReadBytePropertyClass (SBFileReader reader)
    {
      SBClass byteProperty = new SBClass ();
      byteProperty.Name = "ByteProperty";
      byteProperty.Ancestor = ReadPropertyClass (reader);
      int enumType = reader.ReadIndex ();
      byteProperty.Fields.Add ("EnumType", enumType.ToString ());

      return byteProperty;
    }

    private SBClass ReadObjectPropertyClass (SBFileReader reader)
    {
      SBClass objectProperty = new SBClass ();
      objectProperty.Name = "ObjectProperty";
      objectProperty.Ancestor = ReadPropertyClass (reader);
      int objectType = reader.ReadIndex ();
      objectProperty.Fields.Add ("ObjectType", package.FindReferenceName(objectType));

      return objectProperty;
    }

    private SBClass ReadFixedArrayPropertyClass (SBFileReader reader)
    {
      SBClass fixedArrayProperty = new SBClass ();
      fixedArrayProperty.Name = "FixedArrayProperty";
      fixedArrayProperty.Ancestor = ReadPropertyClass (reader);

      int inner = reader.ReadIndex ();
      int count = reader.ReadIndex ();

      fixedArrayProperty.Fields.Add ("Inner", package.FindReferenceName(inner));
      fixedArrayProperty.Fields.Add ("Count", package.FindReferenceName(count));

      return fixedArrayProperty;
    }

    private SBClass ReadArrayPropertyClass (SBFileReader reader)
    {
      SBClass arrayProperty = new SBClass ();
      arrayProperty.Name = "ArrayProperty";
      arrayProperty.Ancestor = ReadPropertyClass (reader);

      int inner = reader.ReadIndex ();

      arrayProperty.Fields.Add ("Inner", package.FindReferenceName(inner));

      return arrayProperty;
    }

    private SBClass ReadMapPropertyClass (SBFileReader reader)
    {
      SBClass mapProperty = new SBClass ();
      mapProperty.Name = "MapProperty";
      mapProperty.Ancestor = ReadPropertyClass (reader);

      int key = reader.ReadIndex ();
      int value = reader.ReadIndex ();

      mapProperty.Fields.Add("Key", key.ToString());
      mapProperty.Fields.Add ("Value", value.ToString ());

      return mapProperty;
    }

    private SBClass ReadClassPropertyClass (SBFileReader reader)
    {
      SBClass classProperty = new SBClass ();
      classProperty.Name = "ClassProperty";
      classProperty.Ancestor = ReadPropertyClass (reader);

      int mclass = reader.ReadIndex();
      classProperty.Fields.Add ("Class", package.NameTable[mclass]);

      return classProperty;
    }

    private SBClass ReadStructPropertyClass (SBFileReader reader)
    {
      SBClass structProperty = new SBClass ();
      structProperty.Name = "StructProperty";
      structProperty.Ancestor = ReadPropertyClass (reader);

      int structType = reader.ReadIndex();

      structProperty.Fields.Add("StructType", structType.ToString());

      return structProperty;
    }

    private SBClass ReadIntPropertyClass (SBFileReader reader)
    {
      SBClass intProperty = new SBClass ();
      intProperty.Name = "IntProperty";
      intProperty.Ancestor = ReadPropertyClass (reader);

      return intProperty;
    }

    private SBClass ReadBoolPropertyClass (SBFileReader reader)
    {
      SBClass boolProperty = new SBClass ();
      boolProperty.Name = "BoolProperty";
      boolProperty.Ancestor = ReadPropertyClass (reader);

      return boolProperty;
    }

    private SBClass ReadFloatPropertyClass (SBFileReader reader)
    {
      SBClass floatProperty = new SBClass ();
      floatProperty.Name = "FloatProperty";
      floatProperty.Ancestor = ReadPropertyClass (reader);

      return floatProperty;
    }

    private SBClass ReadNamePropertyClass (SBFileReader reader)
    {
      SBClass nameProperty = new SBClass ();
      nameProperty.Name = "NameProperty";
      nameProperty.Ancestor = ReadPropertyClass (reader);

      return nameProperty;
    }

    private SBClass ReadStrPropertyClass (SBFileReader reader)
    {
      SBClass strProperty = new SBClass ();
      strProperty.Name = "StrProperty";
      strProperty.Ancestor = ReadPropertyClass (reader);

      return strProperty;
    }

    private SBClass ReadStringPropertyClass (SBFileReader reader)
    {
      SBClass stringProperty = new SBClass ();
      stringProperty.Name = "StringProperty";
      stringProperty.Ancestor = ReadPropertyClass (reader);

      return stringProperty;
    }

    private SBClass ReadStructClass (SBFileReader reader)
    {
      SBClass structClass = new SBClass ();
      structClass.Name = "Struct";
      structClass.Ancestor = ReadFieldClass (reader);
      int scriptText = reader.ReadIndex ();
      int children = reader.ReadIndex ();
      int friendlyName = reader.ReadIndex ();
      int line = reader.ReadInt32 ();
      int textPos = reader.ReadInt32 ();
      int scriptSize = reader.ReadInt32 ();
      //Try to "eat" the script code for children classes, but according
      //to the doc, it should not work, we have to reverse the bytecode...
      if (scriptSize > 0)
      reader.ReadBytes (scriptSize);
      structClass.Fields.Add ("ScriptText", package.FindReferenceName (scriptText));
      structClass.Fields.Add ("Children", package.FindReferenceName (children));
      structClass.Fields.Add ("FriendlyName", package.NameTable[friendlyName]);
      structClass.Fields.Add ("Line", line.ToString ());
      structClass.Fields.Add ("TextPos", textPos.ToString ());
      structClass.Fields.Add ("ScriptSize", scriptSize.ToString ());

      return structClass;
    }

    private SBClass ReadFunctionClass (SBFileReader reader)
    {
      SBClass function = new SBClass ();
      function.Name = "Function";
      function.Ancestor = ReadStructClass (reader);

      Int16 inative = reader.ReadInt16 ();
      byte operatorPrecedence = reader.ReadByte ();
      int functionFlags = reader.ReadInt32 ();
      function.Fields.Add ("iNative", inative.ToString ());
      function.Fields.Add ("OperatorPrecedence", operatorPrecedence.ToString ());
      function.Fields.Add ("FunctionFlags", functionFlags.ToString ());

      if ((functionFlags & (int)FunctionFlags.FUNC_Net) != 0)
      {
        Int16 replicationOffset = reader.ReadInt16 ();
        function.Fields.Add ("ReplicationOffset", replicationOffset.ToString ());
      }

      return function;
    }

    private SBClass ReadStateClass (SBFileReader reader)
    {
      SBClass state = new SBClass ();
      state.Name = "State";
      state.Ancestor = ReadStructClass (reader);

      Int64 probeMask = reader.ReadInt64 ();
      Int64 ignoreMask = reader.ReadInt64 ();
      Int16 labelTableOffset = reader.ReadInt16 ();
      Int32 stateFlags = reader.ReadInt32 ();

      state.Fields.Add ("ProbeMask", probeMask.ToString ());
      state.Fields.Add ("IgnoreMask", ignoreMask.ToString ());
      state.Fields.Add ("LabelTableOffset", labelTableOffset.ToString ());
      state.Fields.Add ("StateFlags", stateFlags.ToString ());

      return state;
    }

    private SBClass ReadNullClass (SBFileReader reader)
    {
      SBClass nullClass = new SBClass ();
      nullClass.Name = "null";
      nullClass.Ancestor = ReadStateClass (reader);

      int classFlags = reader.ReadInt32 ();
      int classGuid = reader.ReadInt32 ();
      int dependencies_count = reader.ReadIndex ();
      //for now skip the dependencies, just "eat" them
      for (int i = 0; i < dependencies_count; ++i)
      {
        reader.ReadIndex ();//Class
        reader.ReadInt32 ();//Deep
        reader.ReadInt32 ();//ScriptTextCRC
      }
      int packageImports_count = reader.ReadIndex ();
      //for now skip the package Imports, just "eat" them
      for (int i = 0; i < packageImports_count; ++i)
        reader.ReadIndex ();//PackageImport
      int classWithin = reader.ReadIndex ();
      int classConfigName = reader.ReadIndex ();

      nullClass.Fields.Add ("ClassFlags", classFlags.ToString ());
      nullClass.Fields.Add ("ClassGuid", classGuid.ToString ());
      nullClass.Fields.Add ("Dependencies Count", dependencies_count.ToString ());
      nullClass.Fields.Add ("Package Imports Count", packageImports_count.ToString ());
      nullClass.Fields.Add ("Class Within", package.FindReferenceName (classWithin));
      nullClass.Fields.Add ("Class Config Name", package.NameTable[classConfigName]);

      return nullClass;
    }
  }
}
