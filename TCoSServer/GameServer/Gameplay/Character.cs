using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.GameServer.Gameplay
{
  class Character
  {
    public uint ID { get; set; }
    public int CurrentWorldID { get; set; }

    //TODO
    public static void CreateNewCharacter ()
    {
    }

    public Character (uint id)
    {
      this.ID = id;
    }
    
  }
}
