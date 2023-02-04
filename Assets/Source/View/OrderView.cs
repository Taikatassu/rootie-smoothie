using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RootieSmoothie.View
{
    public class OrderView : MonoBehaviour
    {
        [SerializeField]
        private Image _requiredColorImage = null;
        [SerializeField]
        private TextMeshProUGUI _requiredColorText = null;
        [SerializeField]
        private GameObject _ratingObject = null;
        [SerializeField]
        private TextMeshProUGUI _ratingText = null;

        private Day _day;
        private Order _order;

        public void Initialize(Day day)
        {
            day.ThrowIfNullArgument(nameof(day));
            _day = day;

            _day.OnOrderStarted += OnOrderStarted;
            _day.OnOrderCompleted += OnOrderCompleted;
        }

        private void OnOrderStarted(Order order)
        {
            _order = order;

            _requiredColorImage.color = _order.Definition.Color;
            _requiredColorText.text = _order.Definition.RequiredIngredientId;
            _ratingObject.SetActive(false);
        }

        private void OnOrderCompleted(Order order)
        {
            _ratingObject.SetActive(true);
            _ratingText.text = $"Wow! {order.Rating} stars!";
        }

        private void OnDestroy()
        {
            _day.OnOrderStarted -= OnOrderStarted;
            _day.OnOrderCompleted -= OnOrderCompleted;
        }
    }
}
