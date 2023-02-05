using System.Collections.Generic;
using RootieSmoothie.Content;
using UnityEngine;

namespace RootieSmoothie.Core.Blending
{
    public class Smoothie
    {
        public List<IngredientDefinition> UsedIngredients { get; private set; }
        public Color Color { get; private set; }
        public bool HasMaxIngredients => UsedIngredients.Count == _maxIngredientCount;

        private int _maxIngredientCount;

        public Smoothie(int maxIngredientCount)
        {
            _maxIngredientCount = maxIngredientCount;
            UsedIngredients = new List<IngredientDefinition>();
        }

        public bool TryAddIngredient(IngredientDefinition ingredient)
        {
            if (HasMaxIngredients)
                return false;

            AddIngredient(ingredient);
            return true;
        }

        private void AddIngredient(IngredientDefinition ingredient)
        {
            UsedIngredients.Add(ingredient);
            Color = ingredient.Color;
            UnityEngine.Debug.Log($"Ingredient {ingredient.Id} added to the smoothie!");

            if (HasMaxIngredients)
                UnityEngine.Debug.Log($"Smoothie has max ingredients ({_maxIngredientCount})!");
        }
    }
}
