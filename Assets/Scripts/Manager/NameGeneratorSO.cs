
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Name Generator")] 
public class NameGeneratorSO : ScriptableObject
{
    static readonly string[] Vogals         = { "a", "e", "i", "o", "u" };
    static readonly string[] Consontants    = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z" };

    public enum CharacterType { Vogal, Consontant }

    [SerializeField] string generatedName;
    [SerializeField] string randomizedName;
    [SerializeField] CharacterVariation[] characterVariations;

    public string GenerateName(int maxAmountOfCharacters)
    {
        generatedName = string.Empty;

        while (generatedName.Length != maxAmountOfCharacters)
            generatedName += GetRandomCharacter(generatedName);

        return generatedName;
    }

    string GetRandomCharacter(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            int random = Utilities.RandomZeroOrOne();

            if (random == 0)
                return Vogals.GetRandom();
            else
                return Consontants.GetRandom();
        }

        else
        {
            string lastCharacter = name.Last().ToString();
            CharacterType characterType = GetCharacterType(lastCharacter);

            if (characterType == CharacterType.Vogal)
                return Consontants.GetRandom();

            else
                return Vogals.GetRandom();
        }
    }


    public string RandomizeName(string reference)
    {
        randomizedName = string.Empty;
        char[] characters = reference.ToCharArray();

        for (int i = 0; i < characters.Length; i++)
        {
            string currentChar  = characters[i].ToString();
            string nextChar     = characters[UtilsArray.ClampIntToArrayLength(i + 1, characters)].ToString();

            CharacterType currentType   = GetCharacterType(currentChar);
            CharacterType nextType      = GetCharacterType(nextChar);

            // both current & next characters need to be of different types in order to randomize
            int random = Utilities.RandomZeroOrOne();

            if (random == 0)
                randomizedName += currentChar;
            else
                randomizedName += GetCharacterVariation(currentChar);
        }

        return randomizedName;
    }

    string GetCharacterVariation(string character)
    {
        for (int i = 0; i < characterVariations.Length; i++)
        {
            if (characterVariations[i].character == character)
                return characterVariations[i].variations.GetRandom();
        }

        return character;
    }

    CharacterType GetCharacterType(string character)
    {
        if (Vogals.Contains(character))
            return CharacterType.Vogal;
        else
            return CharacterType.Consontant;
    }

    bool AreCharactersOfSameType(CharacterType currentChar, CharacterType nextChar)
    {
        if (currentChar == nextChar)
            return true;
        else
            return false;
    }
}



[System.Serializable]
public class CharacterVariation
{
    public string character;
    public string[] variations;
}