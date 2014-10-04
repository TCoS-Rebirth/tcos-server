using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.GameServer.Gameplay;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_cs_create_character_ack : SBPacket
  {
    public int Status;
    public sd_base_character_info CharacterInformation;
    
    public s2c_cs_create_character_ack (c2s_cs_create_character charData)
    {
      Status = 0;
      CharacterInformation = new sd_base_character_info ();

      CharacterInformation.CharacterData = new sd_character_data ();
      CharacterInformation.CharacterData.Dead = 1;
      CharacterInformation.CharacterData.FactionId = 1;
      CharacterInformation.CharacterData.LastUsedTimeStamp = 0;
      CharacterInformation.CharacterData.Money = 0;
      CharacterInformation.CharacterData.Name = charData.Name;
      CharacterInformation.CharacterData.Position = new FVector (0, 0, 6200.0f);
      CharacterInformation.CharacterData.Rotator = new FRotator (0, 0, 0);
      CharacterInformation.CharacterData.WorldId = World.PT_HAWKSMOUTH_ID;
      byte genderRaceBody = (byte)((charData.RaceId | (charData.GenderId << 1) | (charData.BodyTypeId << 2)) & 0x0F);
      
      //Actually there is a 4th tattoo not handled here (and a "DisplayLogo" bit hidden in the Head byte)
      //because currently not used by the game
      CharacterInformation.CharacterData.AppearancePart1 = genderRaceBody | (charData.HeadTypeId<<4) | (charData.BodyColour<<11)
      | (charData.ChestTattooId << 19) | (charData.LeftArmTattooId << 23) | (charData.RightArmTattooId << 27);
      CharacterInformation.CharacterData.AppearancePart2 = (charData.HairColour<<3) | (charData.VoiceId<<19) | (charData.HairTypeId<<23);

      CharacterInformation.CharacterSheetData = new sd_character_sheet_data ();
      CharacterInformation.CharacterSheetData.ClassId = charData.ClassID;
      CharacterInformation.CharacterSheetData.Health = 100.0f;
      CharacterInformation.CharacterSheetData.SelectedSkillDeckID = 0;

      Console.WriteLine ("Weapon Id: {0}", charData.MeleeWeaponId);
      //Test
      CharacterInformation.Items = new sd_item[1];

      CharacterInformation.Items[0] = new sd_item ();
      CharacterInformation.Items[0].ItemId = charData.ChestArmourId;
      Console.WriteLine ("ItemId: {0}", charData.ChestArmourId);
      CharacterInformation.Items[0].ItemTypeId = 72470;
      CharacterInformation.Items[0].CharacterId = 41267;//42;
      CharacterInformation.Items[0].EItemLocationType = 1;
      CharacterInformation.Items[0].LocationId = (int)EEquipmentSlot.ES_CHESTARMOUR;
      CharacterInformation.Items[0].Colour1 = charData.ChestArmourColour1;
      CharacterInformation.Items[0].Colour2 = charData.ChestArmourColour2;
    }

    public override Message Generate ()
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
