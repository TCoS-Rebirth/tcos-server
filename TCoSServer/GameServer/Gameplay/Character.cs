using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TCoSServer.GameServer.Network.Structures;

namespace TCoSServer.GameServer.Gameplay
{
  class Character
  {
    public int ID { get; set; }
    public int CurrentWorldID { get; set; }
    public FVector Position { get; set; }
    public string Name { get; set; }

    //TODO
    public static void CreateNewCharacter ()
    {
    }

    public Character (int id)
    {
      this.ID = id;
    }
    
  }
}
