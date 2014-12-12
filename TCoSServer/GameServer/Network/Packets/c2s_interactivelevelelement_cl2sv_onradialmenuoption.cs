using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCoSServer.Common;
using TCoSServer.GameServer.Gameplay.Enums;

namespace TCoSServer.GameServer.Network.Packets
{
    class c2s_interactivelevelelement_cl2sv_onradialmenuoption : SBPacket
    {
        public Int32 InteractiveLevelElementRelevanceId;
        public Int32 CharacterId;
        public ERadialMenuOptions RadialMenuOptions;

        protected override void InternalRead(MessageReader reader)
        {
            InteractiveLevelElementRelevanceId = reader.ReadInt32();
            CharacterId = reader.ReadInt32();
            RadialMenuOptions = (ERadialMenuOptions) reader.ReadInt32();
        }
    }

}
