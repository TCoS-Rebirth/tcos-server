using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;
using TCoSServer.GameServer.Gameplay.Enums;

namespace TCoSServer.GameServer.Network.Packets
{
    class s2r_interactivelevelelement_sv2clrel_endclientsubaction : SBPacket
    {
        public Int32 InteractiveLevelElementRelevanceId;
        public Int32 Unknown1;//Suboption?
        public Int32 SubActionIndex;
        public Int32 Unknown3;//aReverse?

        public override Message Generate()
        {
            Message result = null;
            using (MessageWriter writer = new MessageWriter(GameMessageIds.S2R_INTERACTIVELEVELELEMENT_SV2CLREL_ENDCLIENTSUBACTION))
            {
                writer.Write(InteractiveLevelElementRelevanceId);
                writer.Write(Unknown1);
                writer.Write(SubActionIndex);
                writer.Write(Unknown3);
                result = writer.ComputeMessage();
            }
            return result;
        }
    }
}
