using UnityEngine;
using System.Collections.Generic;

public class BurgerData
{
    public string burgerName;
    public string description;
    public List<string> ingredients; // in order, bottom to top, includes "bun"

    public BurgerData(string name, string desc, List<string> ingr)
    {
        burgerName = name;
        description = desc;
        ingredients = ingr;
    }
}