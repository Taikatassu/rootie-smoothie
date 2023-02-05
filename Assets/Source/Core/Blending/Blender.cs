using System;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using UnityEngine;

namespace RootieSmoothie.Core.Blending
{
    public class Blender
    {
        public Action<Color> OnSmoothieUpdated;

        public bool HasMaxIngredients => _smoothie.HasMaxIngredients;

        private Smoothie _smoothie;
        private int _maxIngredientsPerSmoothieCount;

        public Blender(int maxIngredientsPerSmoothieCount)
        {
            maxIngredientsPerSmoothieCount.ThrowIfNotPositive(nameof(maxIngredientsPerSmoothieCount));
            _maxIngredientsPerSmoothieCount = maxIngredientsPerSmoothieCount;
        }

        public void StartSmoothie(IngredientDefinition initialIngredient)
        {
            _smoothie = new Smoothie(_maxIngredientsPerSmoothieCount);
            _smoothie.TryAddIngredient(initialIngredient);
            OnSmoothieUpdated?.Invoke(_smoothie.Color);
        }

        public Smoothie CompleteSmoothie()
        {
            var resultingSmoothie = _smoothie;
            _smoothie = null;

            OnSmoothieUpdated?.Invoke(Color.clear);

            return resultingSmoothie;
        }

        public bool TryAddIngredient(IngredientDefinition ingredient)
        {
            if (_smoothie == null)
                return false;

            bool wasIngredientAdded = _smoothie.TryAddIngredient(ingredient);

            if (wasIngredientAdded)
                OnSmoothieUpdated?.Invoke(_smoothie.Color);

            return wasIngredientAdded;
        }
    }
}
