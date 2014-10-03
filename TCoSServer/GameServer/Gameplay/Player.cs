using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.GameServer.Gameplay
{
  class Player
  {
    public const uint NO_CHARACTER = 0;

    /// <summary>
    /// Player account ID.
    /// </summary>
    public uint AccountID { get; set; }

    /// <summary>
    /// Character currently played by the player.
    /// Determined by the choice in the character selection screen.
    /// </summary>
    public Character CurrentCharacter { get; set; }

    public Player ()
    {
      //TODO fill with DB data
      AccountID = 1;
      CurrentCharacter = null;
    }

    public void SetCurrentCharacterById (uint characterId)
    {
      CurrentCharacter = new Character (characterId);
      //TODO WITH DB
      CurrentCharacter.CurrentWorldID = World.CHARACTER_SELECTION_ID;
    }

    //TODO
    public void AddNewCharacter ()
    {
    }

    //TODO
    public uint getNumCharacters ()
    {
      return 0;
    }

  }
}
