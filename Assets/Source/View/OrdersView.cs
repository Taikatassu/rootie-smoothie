using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core;
using UnityEngine;

namespace RootieSmoothie.View
{
    public class OrdersView : MonoBehaviour
    {
        [SerializeField]
        private OrderView _orderViewPrefab = null;
        [SerializeField]
        private Transform _orderViewsParent = null;

        private OrderView _completedOrderView;
        private OrderView _currentOrderView;

        private Game _game;

        public void Initialize(Game game)
        {
            game.ThrowIfNullArgument(nameof(game));
            _game = game;

            InitializeOrderViews();

            _game.Day.OnOrderStarted += OnOrderStarted;
            _game.Day.OnOrderCompleted += OnOrderCompleted;
        }

        private void InitializeOrderViews()
        {
            _completedOrderView = CreateOrderView();
            _currentOrderView = CreateOrderView();
        }

        private OrderView CreateOrderView()
        {
            var newOrderView = Instantiate(_orderViewPrefab, _orderViewsParent);
            newOrderView.gameObject.SetActive(false);
            return newOrderView;
        }

        private void OnOrderStarted(Order order)
        {
            SwitchCurrentAndCompletedOrderViews();
            _currentOrderView.StartOrder(order, _game.CompleteCurrentOrder);
        }

        private void SwitchCurrentAndCompletedOrderViews()
        {
            var tmpCompletedOrderView = _completedOrderView;
            _completedOrderView = _currentOrderView;
            _currentOrderView = tmpCompletedOrderView;

            _currentOrderView.transform.SetAsLastSibling();
        }

        private void OnOrderCompleted(Order order)
        {
            _currentOrderView.CompleteOrder(order);
        }

        private void OnDestroy()
        {
            _game.Day.OnOrderStarted -= OnOrderStarted;
            _game.Day.OnOrderCompleted -= OnOrderCompleted;
        }
    }
}
