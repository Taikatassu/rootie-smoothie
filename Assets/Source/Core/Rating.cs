using System.Collections.Generic;
using System.Linq;

namespace RootieSmoothie.Core
{
    public class Rating
    {
        public float AverageRating => (float)_individualRatings.Average(r => (int)r);
        private List<RatingFullStars> _individualRatings = null;

        public Rating()
        {
            _individualRatings = new List<RatingFullStars>();
        }

        public void AddRating(RatingFullStars stars)
        {
            _individualRatings.Add(stars);
        }
    }
}
