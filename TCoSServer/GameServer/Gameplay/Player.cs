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
    private uint accountID;

    /// <summary>
    /// Character currently played by the player.
    /// Determined by the choice in the character selection screen.
    /// </summary>
    private Character currentCharacter;

    public Player ()
    {
      //TODO fill with DB data
      accountID = 1;
      currentCharacter = null;
    }

    public void SetCurrentCharacterById (uint characterId)
    {
      currentCharacter = new Character (characterId);
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
