using System;
using System.Collections.Generic;
using System.Linq;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core.Blending;
using UnityEngine;

namespace RootieSmoothie.Core
{
    public class Order
    {
        public Action<Order, float> OnTimerRanOut;

        public OrderDefinition Definition { get; private set; }
        public bool IsCompleted { get; private set; }
        public RatingFullStars Rating { get; private set; }
        public float TimeLeft => _timer.TimeToEnd;

        private Timer _timer;

        private const float OrderMaxDuration = 10f;

        public Order(OrderDefinition definition, float startTime)
        {
            Definition = definition;
            IsCompleted = false;

            _timer = new Timer();
            _timer.Start(OrderMaxDuration, startTime);
        }

        public void Complete(Smoothie smoothie)
        {
            if (IsCompleted)
                throw new InvalidOperationException($"Cannot re-complete completed order!");

            smoothie.ThrowIfNullArgument(nameof(smoothie));

            Rate(smoothie);
            IsCompleted = true;

            UnityEngine.Debug.Log($"Order completed with a rating of {Rating} stars!");
        }

        private void Rate(Smoothie smoothie)
        {
            smoothie.ThrowIfNullArgument(nameof(smoothie));

            // TODO: Evaluate smoothie against definition
            var orderColor = Definition.Color;
            var smoothieColor = smoothie.Color;
            var a = new Vector3(orderColor.r, orderColor.g, orderColor.b);
            var b = new Vector3(smoothieColor.r, smoothieColor.g, smoothieColor.b);
            var dist = Vector3.Distance(a, b);
            if (dist <= 0.25f)
                Rating = RatingFullStars.Five;
            else if (dist <= 0.5f)
                Rating = RatingFullStars.Four;
            else if (dist <= 0.8f)
                Rating = RatingFullStars.Three;
            else if (dist <= 1f)
                Rating = RatingFullStars.Two;
            else
                Rating = RatingFullStars.One;
        }

        private RatingFullStars GetRandomRating()
        {
            List<RatingFullStars> possibleRatings
                = Enum.GetValues(typeof(RatingFullStars))
                    .Cast<RatingFullStars>().ToList();

            return possibleRatings.GetRandomElement();
        }

        public void UpdateTimer(float timeNow)
        {
            _timer.Update(timeNow);

            if (_timer.IsDone)
                OnTimerRanOut(this, timeNow);
        }
    }
}
