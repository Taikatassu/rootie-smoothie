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

            _game.OnDayStarted += OnDayStarted;
            _game.OnDayEnded += OnDayCompleted;
        }

        private void InitializeOrderViews()
        {
            _completedOrderView = CreateOrderView();
            _currentOrderView = CreateOrderView();
            ResetOrderViews();
        }

        private OrderView CreateOrderView()
        {
            var newOrderView = Instantiate(_orderViewPrefab, _orderViewsParent);
            return newOrderView;
        }

        private void ResetOrderViews()
        {
            _completedOrderView.gameObject.SetActive(false);
            _currentOrderView.gameObject.SetActive(false);
        }

        private void OnDayStarted(Day day)
        {
            day.OnOrderStarted += OnOrderStarted;
            day.OnOrderCompleted += OnOrderCompleted;

            OnOrderStarted(day.PendingOrders[0]);
        }

        private void OnOrderStarted(Order order)
        {
            SwitchCurrentAndCompletedOrderViews();
            _currentOrderView.StartOrder(order, _game.CompleteCurrentOrder);
        }

        private void OnOrderCompleted(Order order)
        {
            _currentOrderView.CompleteOrder(order);
        }

        private void OnDayCompleted(Day day)
        {
            day.OnOrderStarted -= OnOrderStarted;
            day.OnOrderCompleted -= OnOrderCompleted;

            ResetOrderViews();
        }

        private void SwitchCurrentAndCompletedOrderViews()
        {
            var tmpCompletedOrderView = _completedOrderView;
            _completedOrderView = _currentOrderView;
            _currentOrderView = tmpCompletedOrderView;

            _currentOrderView.transform.SetAsLastSibling();
        }

        private void OnDestroy()
        {
            _game.OnDayStarted -= OnDayStarted;
            _game.OnDayEnded -= OnDayCompleted;
        }
    }
}
