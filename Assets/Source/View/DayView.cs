using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RootieSmoothie.View
{
    public class DayView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currentDayNumberText = null;
        [SerializeField]
        private Button _nextDayButton = null;

        // TODO: Implement this, create RatingView script
        //[SerializeField]
        //private RatingView _ratingView = null;

        private Game _game;
        private Day _day;

        public void Initialize(Game game)
        {
            game.ThrowIfNullArgument(nameof(game));
            _game = game;

            _game.OnDayStarted += OnDayStarted;
            _game.OnDayEnded += OnDayEnded;
        }

        private void OnDayStarted(Day day)
        {
            day.ThrowIfNullArgument(nameof(day));
            _day = day;

            _currentDayNumberText.text = $"Day {_day.DayNumber}";
            // TODO: Close daily rating popup here
            _nextDayButton.interactable = false;
        }

        private void OnDayEnded(Day day)
        {
            // TODO: Show daily rating popup from here
            _nextDayButton.interactable = true;
        }

        // TODO: Hook this method up to a button in the daily rating popup!
        // To be called when the button is clicked
        public void OnNextDayInputGiven()
        {
            _game.StartNextDay();
        }
    }
}
