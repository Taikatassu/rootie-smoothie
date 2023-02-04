using System;
using System.Collections.Generic;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core.Blending;
using Random = System.Random;

namespace RootieSmoothie.Core
{
    public class Day
    {
        public Action<Order> OnOrderStarted;
        public Action<Order> OnOrderCompleted;


        public List<Order> PendingOrders { get; private set; }
        public List<Order> CompletedOrders { get; private set; }
        public Rating Rating { get; private set; }
        public bool HasDayEnded => CompletedOrders.Count == _maxOrderCount;

        // TODO: Timer

        private int _maxOrderCount;
        private List<OrderDefinition> _potentialOrders;

        public Day(int maxOrderCount, List<OrderDefinition> potentialOrders)
        {
            maxOrderCount.ThrowIfNegative(nameof(maxOrderCount));
            potentialOrders.ThrowIfNullOrEmptyArgument(nameof(potentialOrders));

            _maxOrderCount = maxOrderCount;
            _potentialOrders = potentialOrders;

            PendingOrders = new List<Order>();
            CompletedOrders = new List<Order>();
            Rating = new Rating();
        }

        public void Start()
        {
            AddOrder(GetRandomOrder());
        }

        private void AddOrder(Order order)
        {
            if (HasDayEnded)
                throw new InvalidOperationException("Cannot add orders after day has ended!");

            order.ThrowIfNullArgument(nameof(order));

            PendingOrders.Add(order);
            OnOrderStarted?.Invoke(order);
        }

        private Order GetRandomOrder()
        {
            var randomDefinition = _potentialOrders.GetRandomElement();
            return new Order(randomDefinition);
        }

        public void CompleteOrder(Order order, Smoothie smoothie)
        {
            order.ThrowIfNullArgument(nameof(order));
            smoothie.ThrowIfNullArgument(nameof(smoothie));

            order.Complete(smoothie);
            Rating.AddRating(order.Rating);

            CompletedOrders.Add(order);
            PendingOrders.Remove(order);

            OnOrderCompleted?.Invoke(order);
        }

        public void AddRandomOrder()
        {
            AddOrder(GetRandomOrder());
        }
    }
}
