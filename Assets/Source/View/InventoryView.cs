using System;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core;
using RootieSmoothie.Core.Blending;
using RootieSmoothie.View.Blending;
using UnityEngine;

namespace RootieSmoothie.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private IngredientView _ingredientViewPrefab = null;
        [SerializeField]
        private Transform _ingredientViewParent = null;

        private IngredientView[] _ingredientViews;
        private Inventory _inventory;
        private Action<Ingredient> _onSelectIngredient;

        public void Initialize(Inventory inventory, Action<Ingredient> onSelectIngredient)
        {
            inventory.ThrowIfNullArgument(nameof(inventory));

            _inventory = inventory;
            _onSelectIngredient = onSelectIngredient;

            _inventory.OnIngredientsUpdated += OnInventoryItemsChanged;
            DeletePlaceholderInventoryViews();
            InitializeIngredientViews();
        }

        private void OnInventoryItemsChanged()
        {
            var ingredients = _inventory.AvailableIngredients;
            for (int i = 0; i < ingredients.Count; i++)
                _ingredientViews[i].Initialize(ingredients[i]);
        }

        private void DeletePlaceholderInventoryViews()
        {
            for (int i = _ingredientViewParent.childCount - 1; i >= 0; i--)
                Destroy(_ingredientViewParent.GetChild(i).gameObject);
        }

        private void InitializeIngredientViews()
        {
            _ingredientViews = new IngredientView[_inventory.MaxIngredientsAvailableAtOnce];
            for (int i = 0; i < _ingredientViews.Length; i++)
            {
                var newIngredientView = Instantiate(_ingredientViewPrefab, _ingredientViewParent);
                newIngredientView.OnIngredientSelected += SelectIngredient;
                _ingredientViews[i] = newIngredientView;
            }
        }

        private void SelectIngredient(Ingredient selectedIngredient)
        {
            selectedIngredient.ThrowIfNullArgument(nameof(selectedIngredient));

            _onSelectIngredient.Invoke(selectedIngredient);
        }

        private void OnDestroy()
        {
            _inventory.OnIngredientsUpdated -= OnInventoryItemsChanged;
        }
    }
}
