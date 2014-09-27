using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.GameServer.Gameplay;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  struct s2c_cs_create_character_ack
  {
    public int Status;
    public sd_base_character_info CharacterInformation;

    public s2c_cs_create_character_ack (c2s_cs_create_character createCharacterData)
    {
      Status = 0;
      CharacterInformation = new sd_base_character_info ();
      CharacterInformation.CharacterId = 42;

      CharacterInformation.CharacterData = new sd_character_data ();
      CharacterInformation.CharacterData.AccountId = 0;
      CharacterInformation.CharacterData.Dead = 1;
      CharacterInformation.CharacterData.FactionId = 1;
      CharacterInformation.CharacterData.LastUsedTimeStamp = 0;
      CharacterInformation.CharacterData.Money = 0;
      CharacterInformation.CharacterData.Name = createCharacterData.Name;
      CharacterInformation.CharacterData.Position = new FVector (0, 0, 6200.0f);
      CharacterInformation.CharacterData.Rotator = new FRotator (0, 0, 0);
      CharacterInformation.CharacterData.WorldId = World.PT_HAWKSMOUTH_ID;

      CharacterInformation.CharacterSheetData = new sd_character_sheet_data ();
      CharacterInformation.CharacterSheetData.ClassId = createCharacterData.ClassID;
      CharacterInformation.CharacterSheetData.Health = 100.0f;
      CharacterInformation.CharacterSheetData.SelectedSkillDeckID = 0;
      //Test
      CharacterInformation.Items = new sd_item[1];

      CharacterInformation.Items[0] = new sd_item ();
      CharacterInformation.Items[0].ItemId = createCharacterData.ChestArmourId;
      Console.WriteLine ("ItemId: {0}", createCharacterData.ChestArmourId);
      Console.WriteLine ("ItemTypeId: {0}", (int)EItemType.IT_ArmorChest);
      CharacterInformation.Items[0].ItemTypeId = 72470;
      CharacterInformation.Items[0].CharacterId = 42;
      CharacterInformation.Items[0].EItemLocationType = (byte) EItemLocationType.ILT_Equipment;
      CharacterInformation.Items[0].LocationId = (int) EEquipmentSlot.ES_CHESTARMOUR;
      CharacterInformation.Items[0].Colour1 = createCharacterData.ChestArmourColour1;
      CharacterInformation.Items[0].Colour2 = createCharacterData.ChestArmourColour2;
    }

    public Message Generate ()
    {
      Message result;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_CS_CREATE_CHARACTER_ACK))
      {
        writer.Write (Status);
        writer.Write (CharacterInformation);
        result = writer.ComputeMessage ();
      }

      return result;
    }
  }
}
