using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public int id;
    public string name;
    public List<PokemonType> types;
    public int numberOfType = 0;
    public float height;
    public float weight;

    public string GetName()
    {
        return this.name = char.ToUpper(name[0]) + name.Substring(1);
    }

    public float GetHeight()
    {
        return height / 10;
    }

    public float GetWeight()
    {
        return weight * 0.1f;
    }

    public int TypeCount()
    {
        return numberOfType;
    }

    public void ParseTypes(string jsonText)
    {
        Pokemon pokemonData = JsonUtility.FromJson<Pokemon>(jsonText);
        List<PokemonType> typesCopy = pokemonData.GetTypes();
        
        foreach (var type in GetTypes())
        {
            numberOfType++;
            Debug.Log("Type: " + type.type.name);
        }
    }

    public List<PokemonType> GetTypes()
    {
        return types;
    }

}

[System.Serializable]
public class PokemonType
{
    public int slot;
    public TypeData type;
}

[System.Serializable]
public class TypeData
{
    public string name;
    public string url;
}

