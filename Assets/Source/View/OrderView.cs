using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core;
using System;
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
        private TextMeshProUGUI _orderTimerText = null;
        [SerializeField]
        private GameObject _ratingObject = null;
        [SerializeField]
        private TextMeshProUGUI _ratingText = null;
        [SerializeField]
        private Button _completeButton = null;

        private Order _order;
        private Action<float> _onCompleteInputGiven;

        public void StartOrder(Order order, Action<float> onCompleteInputGiven)
        {
            order.ThrowIfNullArgument(nameof(order));
            onCompleteInputGiven.ThrowIfNullArgument(nameof(onCompleteInputGiven));

            _order = order;
            _onCompleteInputGiven = onCompleteInputGiven;
            gameObject.SetActive(true);

            _requiredColorImage.color = _order.Definition.Color;
            _requiredColorText.text = _order.Definition.RequiredIngredientId;
            _ratingObject.SetActive(false);

            _completeButton.gameObject.SetActive(true);
        }

        public void CompleteOrder(Order order)
        {
            if (order != _order)
                throw new InvalidOperationException($"Trying to complete wrong Order for the wrong OrderView");

            _ratingObject.SetActive(true);
            _ratingText.text = $"Wow! {order.Rating} stars!";

            _completeButton.gameObject.SetActive(false);
        }

        // Called when the button is clicked
        public void OnCompleteInputGiven()
        {
            _onCompleteInputGiven?.Invoke(Time.time);
        }

        private void Update()
        {
            _orderTimerText.text = $"{_order.TimeLeft.ToString("0.00")} sec left!";
        }
    }
}
