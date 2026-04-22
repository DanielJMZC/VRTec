using System.Collections.Generic;
using UnityEngine;

public class HamburguerStruct : MonoBehaviour
{
    private const string TOP = "UpperBun";
    private const string BOTTOM = "LowerBun";
    private const string MEAT = "Meat_Prepared";
    private const string CHEESE = "Cheese_Prepared";
    private const string TOMATO = "Tomato_Prepared";
    private const string ONION = "Onion_Prepared";
    private const string LETTUCE = "Lettuce_Prepared";

    private Dictionary<string, List<string>> recipes = new Dictionary<string, List<string>>();
    private List<string> actualrecipe = new List<string>();

    void Awake()
    {
        DefineRecipes();
    }

    private void DefineRecipes()
    {
        recipes.Add("DoubleMeat", new List<string>{MEAT, CHEESE, MEAT});
        recipes.Add("ExtraCheese", new List<string> { MEAT, CHEESE, CHEESE, TOMATO, LETTUCE });
        recipes.Add("NoMeat", new List<string> { CHEESE, TOMATO, ONION, LETTUCE });
        recipes.Add("TripleBurger", new List<string> { MEAT, CHEESE, MEAT, CHEESE, MEAT });
        recipes.Add("NoTomato", new List<string> { MEAT, CHEESE, ONION, LETTUCE });
        recipes.Add("NoOnion", new List<string> { MEAT, CHEESE, TOMATO, LETTUCE });

        actualrecipe.Add("DoubleMeat");
        actualrecipe.Add("ExtraCheese");
        actualrecipe.Add("NoMeat");
        actualrecipe.Add("TripleBurger");
        actualrecipe.Add("NoTomato");
        actualrecipe.Add("NoOnion");
    }

    public List<string> GetOrder(out string name)
    {
        int randomIndex = Random.Range(0, actualrecipe.Count);
        string selectedName = actualrecipe[randomIndex];

        name = selectedName;

        if (recipes.ContainsKey(selectedName))
        {
            List<string> order = new List<string>();
            order.Add(BOTTOM);
            order.AddRange(recipes[selectedName]);
            order.Add(TOP);
            return order;
        }
        else
        {
            Debug.LogWarning($"La receta {selectedName} no existe.");
            return new List<string>();
        }
    }
}