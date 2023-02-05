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
        public OrderDefinition Definition { get; private set; }
        public bool IsCompleted { get; private set; }
        public RatingFullStars Rating { get; private set; }

        // TODO: Timer?

        public Order(OrderDefinition definition)
        {
            Definition = definition;
            IsCompleted = false;
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
    }
}
