using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BurgerMenuUI : MonoBehaviour
{
    // ── UI References ──────────────────────────────────────────────
    public TMP_Text nameLabel;
    public TMP_Text descriptionLabel;
    public Button nextButton;
    public Button backButton;

    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Image slot5;
    public Image slot6;
    public List<Image> ingredientSlots; // 6 slots, assign in Inspector
    public Button exitButton;
    public GameObject menuCanvas;

    // ── Sprites ────────────────────────────────────────────────────
    // Assign all ingredient sprites in the Inspector
    public Sprite bunSprite;
    public Sprite meatSprite;
    public Sprite cheeseSprite;
    public Sprite lettuceSprite;
    public Sprite tomatoSprite;
    public Sprite onionSprite;

    // ── State ──────────────────────────────────────────────────────
    private int _currentIndex = 0;
    private List<BurgerData> _burgers;

    // ── Hardcoded ingredient colors ────────────────────────────────
    private static readonly Dictionary<string, Color> IngredientColors = new Dictionary<string, Color>
    {
        { "bun",     new Color(0.83f, 0.65f, 0.35f) }, // golden brown
        { "meat",    new Color(0.48f, 0.24f, 0.12f) }, // dark brown
        { "cheese",  new Color(1.00f, 0.85f, 0.20f) }, // yellow
        { "lettuce", new Color(0.30f, 0.75f, 0.30f) }, // green
        { "tomato",  new Color(0.90f, 0.20f, 0.20f) }, // red
        { "onion",   new Color(0.85f, 0.70f, 0.90f) }, // light purple
    };

    private void Start()
    {
        nextButton.onClick.AddListener(OnNextPressed);
        backButton.onClick.AddListener(OnBackPressed);

        ingredientSlots = new List<Image> { slot1, slot2, slot3, slot4, slot5, slot6 };

        _burgers = BurgerDatabase.Instance.All;
        exitButton.onClick.AddListener(OnExitPressed);

        DisplayCurrent();
        
    }

    // ── Navigation ─────────────────────────────────────────────────
    public void OnNextPressed()
    {
        if (_burgers == null || _burgers.Count == 0) return;
        _currentIndex = (_currentIndex + 1) % _burgers.Count;
        DisplayCurrent();
    }

    public void OnBackPressed()
    {
        if (_burgers == null || _burgers.Count == 0) return;
        _currentIndex = (_currentIndex - 1 + _burgers.Count) % _burgers.Count;
        DisplayCurrent();
    }

    public void OnExitPressed()
    {
        menuCanvas.SetActive(false);
    }   

    // ── Display ────────────────────────────────────────────────────
    private void DisplayCurrent()
    {
        BurgerData burger = _burgers[_currentIndex];

        nameLabel.text        = burger.burgerName;
        descriptionLabel.text = burger.description;

        // Clear all slots first
        foreach (Image slot in ingredientSlots)
        {
            slot.sprite  = null;
            slot.color   = Color.clear;
            slot.enabled = false;
        }

        // Fill slots left to right with ingredients
        for (int i = 0; i < burger.ingredients.Count && i < ingredientSlots.Count; i++)
        {
            string ingredient = burger.ingredients[i];
            Image slot = ingredientSlots[i];

            slot.sprite  = GetSprite(ingredient);
            slot.color   = GetColor(ingredient);
            slot.enabled = true;
        }
    }

    private Sprite GetSprite(string ingredient)
    {
        switch (ingredient)
        {
            case "bun":     return bunSprite;
            case "meat":    return meatSprite;
            case "cheese":  return cheeseSprite;
            case "lettuce": return lettuceSprite;
            case "tomato":  return tomatoSprite;
            case "onion":   return onionSprite;
            default:
                Debug.LogWarning($"[BurgerMenuUI] No sprite for ingredient: {ingredient}");
                return null;
        }
    }

    private Color GetColor(string ingredient)
    {
        if (IngredientColors.TryGetValue(ingredient, out Color c)) return c;
        return Color.white;
    }
}