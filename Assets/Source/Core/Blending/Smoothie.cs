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

            if (HasMaxIngredients)
                UnityEngine.Debug.LogWarning($"Smoothie has max ingredients ({_maxIngredientCount})!");
        }
    }
}
