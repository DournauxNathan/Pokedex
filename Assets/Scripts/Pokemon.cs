using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private Pokemon pokemonData;

    public int id;
    [Header("BASE INFOS")]
    public string name;
    public float height;
    public float weight;
    [Header("TYPES")]
    public int numberOfType = 0;
    public List<Type> types;
    [Header("STATS")]
    public List<Stat> stats;

    public void GetData(string jsonText)
    {
        Pokemon pokemonData = JsonUtility.FromJson<Pokemon>(jsonText);

        this.name = char.ToUpper(name[0]) + name.Substring(1);
        this.height /= 10;
        this.weight /= 0.1f;
                
        foreach (var type in types)
        {
            numberOfType++;
        }

    }
    
    public int GetId()
    {
        return id;
    }

    public string GetName()
    {
        return this.name;
    }

    public float GetHeight()
    {
        return height;
    }

    public float GetWeight()
    {
        return weight;
    }

    public int TypeCount()
    {
        return numberOfType;
    }

    #region Display methods

    #endregion
    public void DisplayBaseInfo()
    {
        Debug.Log(GetId() + ", " + GetName() + ", Height: " + GetHeight() + " m, Weight: " + GetWeight() + " Kg, Types: " + TypeCount());
    }

    public void DisplayTypes()
    {
        string allTypesInfo = "";

        foreach (var type in types)
        {
            string typeName = char.ToUpper(type.type.name[0]) + type.type.name.Substring(1);
            allTypesInfo += typeName + ", ";
        }

        if (allTypesInfo.Length > 0)
        {
            // Remove the trailing comma and space
            allTypesInfo = allTypesInfo.Substring(0, allTypesInfo.Length - 2);
        }

        Debug.Log(allTypesInfo);
    }

    public void DisplayStats()
    {
        string allStatsInfo = "";

        if (stats != null)
        {
            //Debug.Log("Stats for " + name + ":");
            foreach (var stat in stats)
            {
                string statName = char.ToUpper(stat.stat.name[0]) + stat.stat.name.Substring(1);
                allStatsInfo += statName + ": "+ stat.base_stat + " | "; ;
            }
        }

        if (allStatsInfo.Length > 0)
        {
            // Remove the trailing comma and space
            allStatsInfo = allStatsInfo.Substring(0, allStatsInfo.Length - 2);
        }

        Debug.Log(allStatsInfo);
    }

}

[System.Serializable]
public class Type
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

[System.Serializable]
public class Stat
{
    public int base_stat;
    public StatData stat;
}

[System.Serializable]
public class StatData
{
    public string name;
    public string url;
}

