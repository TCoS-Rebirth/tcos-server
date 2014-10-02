using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;
using TCoSServer.Common;

namespace TCoSServer.GameServer.Network.Packets
{
  class s2c_world_login : SBPacket
  {
    public int Unknown1;
    public int ActorId;
    public int Unknown2;
    public byte Unknown3;
    public sd_playerpawn_login_stream PawnStream;
    public sd_player_stat_stream PlayerStatsStream;
    public int UnknownSlider;
    public int Unknown4;
    public int Unknown5;
    public int Unknown6;
    public int[] Effects;
    public sd_base_character_info CharacterInfo;
    public int[] UnknownArray1;
    public sd_skill[] LearnedSkills;
    public int[] UnknownArray2;//not an array of int, see doc. Waiting for reverse and implem, leave null
    public int[] UnknownArray3;
    public sd_quest[] Quests;
    public int[] UnknownArray4;//not an array of int, see doc. Waiting for reverse and implem, leave null
    public int PlayerGroup;//0 = player, 1 = error, above = GameMaster

    public override Message Generate ()
    {
      Message result = null;
      using (MessageWriter writer = new MessageWriter (GameMessageIds.S2C_WORLD_LOGIN))
      {
        writer.Write (Unknown1);
        writer.Write (ActorId);
        writer.Write (Unknown2);
        writer.Write (Unknown3);
        writer.Write (PawnStream);
        writer.Write (PlayerStatsStream);
        writer.Write (UnknownSlider);
        writer.Write (Unknown4);
        writer.Write (Unknown5);
        writer.Write (Unknown6);
        writer.Write (Effects);
        writer.Write (CharacterInfo);
        writer.Write (UnknownArray1);
        writer.Write (LearnedSkills);
        writer.Write (UnknownArray2);
        writer.Write (UnknownArray3);
        writer.Write (Quests);
        writer.Write (UnknownArray4);
        writer.Write (PlayerGroup);

        result = writer.ComputeMessage ();
      }
      return result;
    }

  }
}
