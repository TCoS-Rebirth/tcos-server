using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.GameServer.Gameplay.Enums
{
  enum EPhysics : byte
  {
    PHYS_None = 0,
    PHYS_Walking = 1,
    PHYS_Falling = 2,
    PHYS_Swimming = 3,
    PHYS_Flying = 4,
    PHYS_Rotating = 5,
    PHYS_Projectile = 6,
    PHYS_Interpolating = 7,
    PHYS_MovingBrush = 8,
    PHYS_Spider = 9,
    PHYS_Trailer = 10,
    PHYS_Ladder = 11,
    PHYS_RootMotion = 12,
    PHYS_Karma = 13,
    PHYS_KarmaRagDoll = 14,
    PHYS_Hovering = 15,
    PHYS_CinMotion = 16,
    PHYS_DragonFlying = 17,
    PHYS_Jumping = 18,
    PHYS_SitGround = 19,
    PHYS_SitChair = 20,
    PHYS_Submerged = 21,
    PHYS_Turret = 22
  }
}
