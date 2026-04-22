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
    private Dictionary<string, List<string>> riddles = new Dictionary<string, List<string>>();
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

        riddles.Add("DoubleMeat", new List<string> { 
            "I don't want any greens or anything fancy. Just give me the real deal — twice.", 
            "No vegetables. No distractions. I'm a simple person with a big appetite.",
            "Cheese in the middle, beef on both sides. You know what I mean."
        });

        riddles.Add("ExtraCheese", new List<string> { 
            "I had a really hard week. I need something cheesy. More cheesy than normal. Way more.",
            "One patty is fine, but make sure the cheese does most of the heavy lifting.",
            "Something with greens on top but... I want to feel the cheese before I even see the lettuce."
        });

        riddles.Add("NoMeat", new List<string> { 
            "I'm visiting a friend who's vegetarian. I figured I'd try what she eats. Something fresh, with that sharp kick.",
            "Nothing from an animal that had a face. But cheese is fine — don't worry about the cheese.",
            "Make it colorful. I want onion in there. I want to feel it."
        });

        riddles.Add("TripleBurger", new List<string> { 
            "I just ran a marathon. Metaphorically. Feed me accordingly.",
            "Three. You know what I mean by three. And the cheese has to go between each one.",
            "Forget the vegetables. I want layers — alternating, like a proper tower."
        });

        riddles.Add("NoTomato", new List<string> { 
            "Something classic, but I can't do tomatoes. Something about the texture. You understand.",
            "Cheese, meat, something crunchy and something sharp — but keep the red stuff out of it.",
            "My usual, but without whatever makes it watery. You'll figure it out."
        });

        riddles.Add("NoOnion", new List<string> { 
            "I have a date tonight. Classic burger, all the usuals — just, you know, skip the thing that lingers.",
            "Meat, cheese, tomato, the green stuff. In that order. Simple.",
            "Nothing too aggressive. Just a clean, honest burger."
        });
        

        actualrecipe.Add("DoubleMeat");
        actualrecipe.Add("ExtraCheese");
        actualrecipe.Add("NoMeat");
        actualrecipe.Add("TripleBurger");
        actualrecipe.Add("NoTomato");
        actualrecipe.Add("NoOnion");
    }

    public List<string> GetOrder(out string name, out string riddle)
    {
        int randomIndex = Random.Range(0, actualrecipe.Count);
        string selectedName = actualrecipe[randomIndex];

        name = selectedName;
        if (riddles.ContainsKey(selectedName))
        {
            List<string> options = riddles[selectedName];
            riddle = options[Random.Range(0, options.Count)];
        }
        else
        {
            riddle = "Tengo hambre...";
        }
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