using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace RootieSmoothie.View
{
    public class OrdersView : MonoBehaviour
    {
        [SerializeField]
        private OrderView _orderViewPrefab = null;
        [SerializeField]
        private Transform _orderViewsParent = null;

        [SerializeField] private Transform _characterRoot;

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
            VisualizeCustomer(order.Definition.CharacterAssetPath);
        }

        private void VisualizeCustomer(string path)
        {
            ClearCharacterRoot();
            var character = Resources.Load<GameObject>(path);
            var go = Instantiate(character);
            var t = go.transform;
            t.parent = _characterRoot;
            t.localScale = Vector3.one;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
        }

        private void ClearCharacterRoot()
        {
            var count = _characterRoot.childCount;
            for (var i = count - 1; i >= 0; --i)
            {
                var child = _characterRoot.GetChild(i);
                Destroy(child.gameObject);
            }
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
