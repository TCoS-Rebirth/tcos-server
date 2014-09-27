using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_item : SBStruct
  {
    public int ItemId;
    public int ItemTypeId;
    public int CharacterId;
    public byte EItemLocationType;
    public int LocationId;
    public int LocationSlot;
    public int StackSize;
    public byte Attuned;
    public byte Colour1;
    public byte Colour2;
    public int Serial;
 
    public void WriteTo (MessageWriter writer)
    {
      writer.Write (ItemId);
      writer.Write (ItemTypeId);
      writer.Write (CharacterId);
      writer.Write (EItemLocationType);
      writer.Write (LocationId);
      writer.Write (LocationSlot);
      writer.Write (StackSize);
      writer.Write (Attuned);
      writer.Write (Colour1);
      writer.Write (Colour2);
      writer.Write (Serial);
    }
  }

  public enum EItemType
  {
    IT_BodySlot,
    IT_JewelryRing,
    IT_JewelryNecklace,
    IT_JewelryQualityToken,
    IT_WeaponQualityToken,
    IT_SkillToken,
    IT_QuestItem,
    IT_Trophy,
    IT_ContainerSuitBag,
    IT_ContainerExtraInventory,
    IT_Resource,
    IT_WeaponOneHanded,
    IT_WeaponDoublehanded,
    IT_WeaponDualWielding,
    IT_WeaponRanged,
    IT_WeaponShield,
    IT_ArmorHeadGear,
    IT_ArmorLeftShoulder,
    IT_ArmorRightShoulder,
    IT_ArmorLeftGauntlet,
    IT_ArmorRightGauntlet,
    IT_ArmorChest,
    IT_ArmorBelt,
    IT_ArmorLeftThigh,
    IT_ArmorLeftShin,
    IT_ClothChest,
    IT_ClothLeftGlove,
    IT_ClothRightGlove,
    IT_ClothPants,
    IT_ClothShoes,
    IT_MiscellaneousTickets,
    IT_MiscellaneousKey,
    IT_MiscellaneousLabyrinthKey,
    IT_Recipe,
    IT_ArmorRightThigh,
    IT_ArmorRightShin,
    IT_ItemToken,
    IT_Consumable,
    IT_Broken
  }
//   41266SB GamePlay.Item_ArmorBelt
//ID: 41267, Text: SBGamePlay.Item_ArmorChest
//ID: 41268, Text: SBGamePlay.Item_ArmorHeadGear
//ID: 41269, Text: SBGamePlay.Item_ArmorLeftGauntlet
//ID: 41270, Text: SBGamePlay.Item_ArmorLeftShoulder
//ID: 41271, Text: SBGamePlay.Item_ArmorRightGauntlet
//ID: 41272, Text: SBGamePlay.Item_ArmorRightShoulder
//ID: 41275, Text: SBGamePlay.Item_BodySlot
//ID: 41276, Text: SBGamePlay.Item_ClothChest
//ID: 41277, Text: SBGamePlay.Item_ClothLeftGlove
//ID: 41278, Text: SBGamePlay.Item_ClothPants
//ID: 41279, Text: SBGamePlay.Item_ClothRightGlove
//ID: 41280, Text: SBGamePlay.Item_ClothShoes
//ID: 41281, Text: SBGamePlay.Item_ContainerExtraInventory
//ID: 41282, Text: SBGamePlay.Item_ContainerSuitBag
//ID: 41283, Text: SBGamePlay.Item_JewelryNecklace
//ID: 41284, Text: SBGamePlay.Item_JewelryQualityToken
//ID: 41285, Text: SBGamePlay.Item_JewelryRing
//ID: 41286, Text: SBGamePlay.Item_MiscellaneousKey
//ID: 41287, Text: SBGamePlay.Item_MiscellaneousLabyrinthKey
//ID: 41288, Text: SBGamePlay.Item_MiscellaneousTickets
//ID: 41289, Text: SBGamePlay.Item_QuestItem
//ID: 41290, Text: SBGamePlay.Item_Recipe
//ID: 41291, Text: SBGamePlay.Item_Resource
//ID: 41292, Text: SBGamePlay.Item_SkillToken
//ID: 41293, Text: SBGamePlay.Item_Trophy
//ID: 41294, Text: SBGamePlay.Item_WeaponDoubleHanded
//ID: 41295, Text: SBGamePlay.Item_WeaponDualWielding
//ID: 41296, Text: SBGamePlay.Item_WeaponOneHanded
//ID: 41297, Text: SBGamePlay.Item_WeaponQualityToken
//ID: 41298, Text: SBGamePlay.Item_WeaponRanged
//ID: 41299, Text: SBGamePlay.Item_WeaponShield


  public enum EItemLocationType : byte
  {
    ILT_Unknown,
    ILT_Inventory,
    ILT_Equipment, //equip and place in slot
    ILT_BodySlot, //equip but dont place (visual, probably for NPC's)
    ILT_Item,
    ILT_Mail,
    ILT_Auction,
    ILT_Skill,
    ILT_Trade,
    ILT_BodySlotInventory,
    ILT_SendMail,
    ILT_ShopBuy,
    ILT_ShopSell,
    ILT_ShopPaint,
    ILT_ShopCrafting
  }

  public enum EEquipmentSlot
  {
    ES_CHEST,
    ES_LEFTGLOVE,
    ES_RIGHTGLOVE,
    ES_PANTS,
    ES_SHOES,
    ES_HELMET,
    ES_HELMETDETAIL,
    ES_RIGHTSHOULDER,
    ES_RIGHTSHOULDERDETAIL,
    ES_LEFTSHOULDER,
    ES_LEFTSHOULDERDETAIL,
    ES_RIGHTGAUNTLET,
    ES_LEFTGAUNTLET,
    ES_CHESTARMOUR,
    ES_CHESTARMOURDETAIL,
    ES_BELT,
    ES_LEFTTHIGH,
    ES_LEFTSHIN,
    ES_MELEEWEAPON,
    ES_RANGEDWEAPON,
    ES_LEFTRING,
    ES_RIGHTRING,
    ES_NECKLACE,
    ES_SHIELD,
    ES_RIGHTTHIGH,
    ES_RIGHTSHIN,
    ES_SLOTCOUNT,
    ES_NO_SLOT
  }

}
