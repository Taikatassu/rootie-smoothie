using System;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core.Blending;
using UnityEngine;
using UnityEngine.UI;

namespace RootieSmoothie.View.Blending
{
    public class IngredientView : MonoBehaviour
    {
        public Action<Ingredient> OnIngredientSelected;

        [SerializeField]
        private Image _image = null;
        [SerializeField]
        private Button _button = null;

        private Ingredient _ingredient;

        public void Initialize(Ingredient ingredient)
        {
            _ingredient = ingredient;
            SetVisuals();
        }

        private void SetVisuals()
        {
            if (_ingredient == null)
            {
                _image.gameObject.SetActive(false);
                _button.gameObject.SetActive(false);
            }
            else
            {
                _image.gameObject.SetActive(true);
                _button.gameObject.SetActive(true);
                _image.color = _ingredient.Definition.Color;
            }
        }

        // Called when the button is clicked
        public void SelectIngredient()
        {
            OnIngredientSelected?.Invoke(_ingredient);
        }
    }
}
