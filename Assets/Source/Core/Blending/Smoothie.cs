using System.Collections.Generic;
using RootieSmoothie.Content;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace RootieSmoothie.Core.Blending
{
    public class Smoothie
    {
        public List<IngredientDefinition> UsedIngredients { get; private set; }
        public Color Color { get; private set; }
        public float Hue { get; private set; }
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
            BlendColor();
            UnityEngine.Debug.Log($"Ingredient {ingredient.Id} added to the smoothie!");

            if (HasMaxIngredients)
                UnityEngine.Debug.Log($"Smoothie has max ingredients ({_maxIngredientCount})!");
        }

        private void BlendColor()
        {
            var newColor = UsedIngredients[0].Color;
            for (var i = 1; i < UsedIngredients.Count; ++i)
            {
                var blendColor = UsedIngredients[i].Color;
                var mixedColor = Color.Lerp(newColor, blendColor, 0.5f);
                var a = new Vector3(mixedColor.r, mixedColor.g, mixedColor.b);
                var b = new Vector3(newColor.r, newColor.g, newColor.b);
                var dist = Vector3.Distance(a, b);
                if (dist < 0.35f)
                {
                    newColor = blendColor;
                }
                else
                {
                    newColor = mixedColor;
                }
            }
            Color = newColor;

        }
        
    }
}
