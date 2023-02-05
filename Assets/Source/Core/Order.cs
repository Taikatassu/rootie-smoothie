using System;
using System.Collections.Generic;
using System.Linq;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core.Blending;

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
            Rating = GetRandomRating();
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
