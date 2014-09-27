using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Structures
{
  struct sd_base_character_info : SBStruct
  {
    public int CharacterId;
    public sd_character_data CharacterData;
    public sd_character_sheet_data CharacterSheetData;
    public sd_item[] Items;

    public void WriteTo (MessageWriter writer)
    {
      writer.Write (CharacterId);
      writer.Write (CharacterData);
      writer.Write (CharacterSheetData);
      writer.Write (Items);
    }
  }


}
