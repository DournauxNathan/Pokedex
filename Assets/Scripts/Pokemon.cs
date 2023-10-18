using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public int id;
    public string name;
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


}
