using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;
using TCoSServer.GameServer.Gameplay.Enums;

namespace TCoSServer.GameServer.Network.Packets
{
    class s2r_interactivelevelelement_sv2clrel_startclientsubaction : SBPacket
    {
        public Int32 InteractiveLevelElementRelevanceId;
        public Int32 Unkown1;//I think it's unused but to be verified...
        public Int32 SubActionIndex;//To be verified...
        public Int32 Unknown2;//I think it's unused but to be verified...
        public Int32 OtherRelevanceId;//Unknwown, compared with 0xFFFFFFFF

        public override Message Generate()
        {
            Message result = null;
            using (MessageWriter writer = new MessageWriter(GameMessageIds.S2R_INTERACTIVELEVELELEMENT_SV2CLREL_STARTCLIENTSUBACTION))
            {
                writer.Write(InteractiveLevelElementRelevanceId);
                writer.Write(Unkown1);
                writer.Write(SubActionIndex);
                writer.Write(Unknown2);
                writer.Write(OtherRelevanceId);
                result = writer.ComputeMessage();
            }
            return result;
        }
    }
}
