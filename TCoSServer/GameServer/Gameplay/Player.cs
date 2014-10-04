using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCoSServer.GameServer.Gameplay
{
  class Player
  {
    public const int NO_CHARACTER = 0;

    /// <summary>
    /// Player account ID.
    /// </summary>
    public int AccountID { get; set; }

    /// <summary>
    /// Character currently played by the player.
    /// Determined by the choice in the character selection screen.
    /// </summary>
    public Character CurrentCharacter { get; set; }
    public Character[] Characters
    {
      get
      {
        return Characters.ToArray<Character> ();
      }
    }

    private Dictionary<int, Character> characters;

    public Player ()
    {
      //TODO fill with DB data
      CurrentCharacter = null;
      characters = new Dictionary<int, Character> (20);
      AccountID = new Random ().Next ();
    }

    public void SetCurrentCharacterById (int characterId)
    {
      if (characters.ContainsKey (characterId))
        CurrentCharacter = characters[characterId];
      else
      {
        CurrentCharacter = new Character (characterId);
        characters.Add (characterId, CurrentCharacter);
      }
      //TODO WITH DB
      CurrentCharacter.CurrentWorldID = World.CHARACTER_SELECTION_ID;
    }

    //TODO
    public void AddNewCharacter (Character newCharacter)
    {
      characters.Add (newCharacter.ID, newCharacter);
    }

    //TODO
    public int getNumCharacters ()
    {
      return characters.Count;
    }

  }
}
