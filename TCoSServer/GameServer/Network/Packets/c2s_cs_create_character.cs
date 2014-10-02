using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;
using System.Collections;

namespace TCoSServer.GameServer.Network.Packets
{
  class c2s_cs_create_character : SBPacket
  {
    public string Name;
    public int ClassID;
    public int FixedSkill1Id;
    public int FixedSkill2Id;
    public int FixedSkill3Id;
    public int CustomSkill1Id;
    public int CustomSkill2Id;
    public int RangedWeaponIdDuplicated;

    //Lod0 
    public byte VoiceId ;
    public byte RightArmTattooId;
    public byte LeftArmTattooId;
    public byte ChestTattooId;
    public byte RightGauntletColour1;
    public byte RightGauntletColour2;
    public byte LeftGauntletColour1;
    public byte LeftGauntletColour2;
    public byte RightGloveColour1;
    public byte RightGloveColour2;
    public byte LeftGloveColour1;
    public byte LeftGloveColour2;

    //Lod1
    public byte ShinRightColour1;
    public byte ShinRightColour2;
    public byte ShinLeftColour1;
    public byte ShinLeftColour2;
    public byte ThighRightColour1;
    public byte ThighRightColour2;
    public byte ThighLeftColour1;
    public byte ThighLeftColour2;
    public byte BeltColour1;
    public byte BeltColour2;
    public byte RightShoulderColour1;
    public byte RightShoudlerColour2;
    public byte LeftShoulderColour1;
    public byte LeftShoulderColour2;
    public byte HelmetColour1;
    public byte HelmetColour2;
    public byte ShoesColour1;
    public byte ShoesColour2;
    public byte PantsColour1;
    public byte PantsColour2;

    //Lod2
    public byte RangedWeaponId;
    public byte ShieldId;
    public byte MeleeWeaponId;
    public byte ShinRightId;
    public byte ShinLeftId;
    public byte ThighRightId;
    public byte TighLeftId;
    public byte BeltId;
    public byte GauntletRightId;
    public byte GauntletLeftId;
    public byte ShoulderRightId;
    public byte ShoulderLeftId;
    public byte HelmetId;
    public byte ShoesId;
    public byte PantsId;
    public byte GloveRightId;
    public byte GloveLeftId;

    //Lod3
    public byte ChestArmourColour1;
    public byte ChestArmourColour2;
    public byte ChestArmourId;
    public byte TorsoColour1;
    public byte TorsoColour2;
    public byte TorsoId;
    public byte HairColour;
    public byte HairTypeId;
    public byte BodyColour;
    public byte HeadTypeId;
    public byte BodyTypeId;
    public byte GenderId;
    public byte RaceId;

    public byte[] Lod0;
    public byte[] Lod1;
    public byte[] Lod2;
    public byte[] Lod3;
    

    protected override void InternalRead (MessageReader reader)
    {
      Lod0 = reader.ReadByteArray ();
      Lod1 = reader.ReadByteArray ();
      Lod2 = reader.ReadByteArray ();
      Lod3 = reader.ReadByteArray ();
      Name = reader.ReadString ();
      ClassID = reader.ReadInt32 ();
      FixedSkill1Id = reader.ReadInt32 ();
      FixedSkill2Id = reader.ReadInt32 ();
      FixedSkill3Id = reader.ReadInt32 ();
      CustomSkill1Id = reader.ReadInt32 ();
      CustomSkill2Id = reader.ReadInt32 ();
      RangedWeaponIdDuplicated = reader.ReadInt32 ();
      

      //Parse compressed data, reverse order is easier to understand
      Array.Reverse(Lod0);
      Array.Reverse(Lod1);
      Array.Reverse(Lod2);
      Array.Reverse(Lod3);

      //Lod0
      RightGauntletColour1 = Lod0[5];
      RightGauntletColour2 = Lod0[6];
      LeftGauntletColour1 = Lod0[7];
      LeftGauntletColour2 = Lod0[8];
      RightGloveColour1 = Lod0[9];
      RightGloveColour2 = Lod0[10];
      LeftGloveColour1 = Lod0[11];
      LeftGloveColour2 = Lod0[12];

      //Lod0 special stuff
      VoiceId = Lod0[0];
      RightArmTattooId = (byte) (Lod0[3] & 0x0F);
      LeftArmTattooId = (byte) ((Lod0[4] & 0xF0)>>4);
      ChestTattooId = (byte) (Lod0[4] & 0x0F);

      //Lod1
      ShinRightColour1 = Lod1[0];
      ShinRightColour2 = Lod1[1];
      ShinLeftColour1 = Lod1[2];
      ShinLeftColour2 = Lod1[3];
      ThighRightColour1 = Lod1[4];
      ThighRightColour2 = Lod1[5];
      ThighLeftColour1 = Lod1[6];
      ThighLeftColour2 = Lod1[7];
      BeltColour1 = Lod1[8];
      BeltColour2 = Lod1[9];
      RightShoulderColour1 = Lod1[10];
      RightShoudlerColour2 = Lod1[11];
      LeftShoulderColour1 = Lod1[12];
      LeftShoulderColour2 = Lod1[13];
      HelmetColour1 = Lod1[14];
      HelmetColour2 = Lod1[15];
      ShoesColour1 = Lod1[16];
      ShoesColour2 = Lod1[17];
      PantsColour1 = Lod1[18];
      PantsColour2 = Lod1[19];

      //Lod2
      //8-bits  Unused [0] (8 bits)

      //4-bits  Unused [1] (4 bits upper)
      //4 bits  Ranged Weapon ID upper [1] (4 bits lower)

      //4-bits  Ranged Weapon ID lower [2] (4 bits upper)
      //4-bits  Shield ID upper [2] (4 bits lower)

      //2-bits  Shield ID [3] (2 bits upper)
      //6-bits  Melee Weapon ID upper [3] (6 bits lower)

      //2-bits  Melee Weapon ID lower [4] (2 bits upper)
      //6-bits  Shin Right [4] (6 bits)

      //6-bits  Shin Left [5] (6 bits)
      //2-bits  Thigh Right [5] (2 bits)

      //4-bits  Thigh Right [6] (4 bits)
      //4-bits  Thigh Left [6] (4 bits)

      //2-bits  Thigh Left [7] (2 bits)
      //6-bits  Belt [7] (6 bits)

      //6-bits  Gauntlet Right [8] (6 bits)
      //2-bits  Gauntlet Left [8] (2 bits)

      //4-bits  Gauntlet Left [9] (4 bits)
      //4-bits  Shoulder Right [9] (4 bits)

      //2-bits  Shoulder Right [10] (2 bits)
      //6-bits  Shoulder Left [10] (6 bits)

      //6-bits  Helmet [11] (6 bits)
      //2-bits  Shoes [11] (2 bits)

      //4-bits  Shoes [12] (4 bits)
      //4-bits  Pants [12] (4 bits)

      //4-bits  Pants [13] (4 bits)
      //4-bits  Glove right [13] (4 bits)

      //2-bits  Glove right [14] (2 bits)
      //6-bits  Glove left [14] (6 bits)

      RangedWeaponId = (byte) ( (Lod2[2] & 0xF0)<<4 | (Lod2[1]>>4) );
      ShieldId = (byte) ( ((Lod2[3] & 0x0F)<<2) | ((Lod2[3] & 0xC0)>>6) );
      MeleeWeaponId = (byte) (((Lod2[3] & 0x3F)<<2) | ((Lod2[4] & 0xC0)>>6));
      ShinRightId = (byte) (Lod2[4] & 0x3F);
      ShinLeftId = (byte) ((Lod2[5] & 0xFC)>>2);
      ThighRightId = (byte) ( ((Lod2[5] & 0x03)<< 4) | ((Lod2[6] & 0xF0)>>4));
      TighLeftId = (byte) ( ((Lod2[6] & 0x0F)<<2) | ((Lod2[7] & 0xC0)>>6) );
      BeltId = (byte) (Lod2[7] & 0x3F);
      GauntletRightId = (byte) ((Lod2[8] & 0xFC)>>2);
      GauntletLeftId = (byte) ( ((Lod2[8]&0x03)<<4) | ((Lod2[9] & 0xF0)>>4)  );
      ShoulderRightId = (byte) ( ((Lod2[9] & 0x0F)<<2) | ((Lod2[10] & 0xC0)>>6) );
      ShoulderLeftId = (byte) ( (Lod2[10] & 0x3F) );
      HelmetId = (byte) ((Lod2[11] & 0xFC)>>2);
      ShoesId = (byte) ( ((Lod2[11] & 0x03)<<4) | ((Lod2[12] & 0xF0)>>4) );
      PantsId = (byte) ( ((Lod2[12] & 0x0F)<<4) | ((Lod2[13] & 0xF0)>>4));
      GloveRightId = (byte) ( ( (Lod2[13] & 0x0F)<<2) | ( (Lod2[14] & 0xC0)>>6) );
      GloveLeftId = (byte) (Lod2[14] & 0x3F);

      //Lod3
      //1-Bit   Unused [0]
      //7-bits  Chest Colour 1 [0] (7 bits)
      //1-bits  Chest Colour 1 [1] (1 bits)
      //7-bits  Chest Colour 2 [1] (7 bits lower)
      //1-bits  Chest Colour 2 [2] (1 bits upper)
      //6-bits  Chest Armour   [2] (6 bits lower)
      //1-bits  Torso Colour 1 [2] (1 bit lower)
      //7-bits  Torso Colour 1 [3] (7 bit upper)
      //1-bits  Torso Colour 2 [3] (1 bit lower)
      //7-bits  Torso Colour 2 [4] (7 bit upper)
      //1-bits  Torso          [4] (1 bit lower)
      //7-bits  Torso          [5] (7 bits upper)
      //1-bits  Hair Colour    [5] (1 bit lower)
      //7-bits  Hair Colour    [6] (7 bits upper)
      //1-bits  Hair Style     [6] (1 bits lower)
      //5-bits  Hair Style     [7] (5 bits upper)
      //3-bits  Body Colour    [7] (3 bits lower)
      //5-bits  Body Colour    [8] (5 bits upper)
      //3-bits  Head Type      [8] (3 bits lower)
      //4-bits  Head Type      [9] (4 bits)
      //2-bits  Body Type      [9] (2 bits)
      //1-bit   Male = 0 Female = 1 [9] (1 bits)
      //1-bit   Human = 0 Daevi = 1 [9] (1 bit)

      ChestArmourColour1 = (byte) (((Lod3[0] & 0xFE)<<1) | (Lod3[1]>>7));
      ChestArmourColour2 = (byte) (((Lod3[1] & 0x7F)<<1) | (Lod3[2]>>7));
      ChestArmourId = (byte) ((Lod3[2] & 0x7E)>>1);
      TorsoColour1 = (byte) ( (Lod3[2]<<7) | (Lod3[3]>>1) );
      TorsoColour2 = (byte) ( (Lod3[3]<<7) | (Lod3[4]>>1) );
      TorsoId = (byte) ( (Lod3[4]<<7) | (Lod3[5]>>1) );
      HairColour = (byte) ( (Lod3[5]<<7) | (Lod3[6]>>1) );
      HairTypeId = (byte) ( ((Lod3[6] & 0x01)<<5) | (Lod3[7]>>3) );
      BodyColour = (byte) ( (Lod3[7]<<5) | (Lod3[8]>>3) );
      HeadTypeId = (byte) ( ((Lod3[8]&0x07)<<4) | ((Lod3[9] & 0xF0)>>4) );
      BodyTypeId = (byte) ((Lod3[9] & 0x0C)>>2);
      GenderId = (byte) ((Lod3[9] & 0x02)>>1);
      RaceId = (byte) (Lod3[9] & 0x01);
    }
  }
}
