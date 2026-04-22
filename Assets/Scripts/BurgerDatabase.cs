using UnityEngine;
using System.Collections.Generic;

public class BurgerDatabase
{
    private static BurgerDatabase _instance;
    public static BurgerDatabase Instance
    {
        get
        {
            if (_instance == null) _instance = new BurgerDatabase();
            return _instance;
        }
    }

    private List<BurgerData> _burgers = new List<BurgerData>();
    public List<BurgerData> All => _burgers;
    public int Count => _burgers.Count;

    private const string BUN     = "bun";
    private const string MEAT    = "meat";
    private const string CHEESE  = "cheese";
    private const string LETTUCE = "lettuce";
    private const string TOMATO  = "tomato";
    private const string ONION   = "onion";

    private BurgerDatabase()
    {
        _burgers.Add(new BurgerData(
            "Double Meat",
            "A carnivore's dream — two generous patties stacked back to back, separated only by a layer of melted cheese. No distractions, just pure beef.",
            new List<string> { BUN, MEAT, CHEESE, MEAT }
        ));

        _burgers.Add(new BurgerData(
            "Extra Cheese",
            "More cheese than any reasonable person would ask for. Two full slices crown a single patty alongside fresh tomato and crisp lettuce.",
            new List<string> { BUN, MEAT, CHEESE, CHEESE, TOMATO, LETTUCE }
        ));

        _burgers.Add(new BurgerData(
            "No Meat",
            "A garden-forward stack built entirely on veggies and dairy. Melted cheese binds it all together while onion adds a sharp bite.",
            new List<string> { BUN, CHEESE, TOMATO, ONION, LETTUCE }
        ));

        _burgers.Add(new BurgerData(
            "Triple Burger",
            "The indulgent tower. Three patties, two cheese layers, nothing else getting in the way. Meant for serious appetites only.",
            new List<string> { BUN, MEAT, CHEESE, MEAT, CHEESE, MEAT }
        ));

        _burgers.Add(new BurgerData(
            "No Tomato",
            "Classic flavors, clean finish. Patty, cheese, a ring of onion, and cool lettuce — no tomato in sight.",
            new List<string> { BUN, MEAT, CHEESE, ONION, LETTUCE }
        ));

        _burgers.Add(new BurgerData(
            "No Onion",
            "The traditional stack: patty, cheese, tomato, lettuce. A timeless combination with nothing to offend.",
            new List<string> { BUN, MEAT, CHEESE, TOMATO, LETTUCE }
        ));

        Debug.Log($"[BurgerDatabase] {_burgers.Count} burgers loaded.");
    }
}