using System;
using System.Collections.Generic;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core.Blending;

namespace RootieSmoothie.Core
{
    public class Game
    {
        public Action<Day> OnDayStarted;
        public Action<Day> OnDayEnded;

        private const int MaxIngredientsPerSmoothieCount = 8;
        private const int MaxOrderCount = 10;
        private const int MaxIngredientsAvailableAtOnce = 5;
        private const float NewIngredientSpawnDurationSec = 2f;

        private List<OrderDefinition> _orderDefinitions;
        private List<IngredientDefinition> _ingredientDefinitions;

        public Day Day { get; private set; }
        public Blender Blender { get; private set; }
        public Inventory Inventory { get; private set; }

        public Game(List<OrderDefinition> orderDefinitions,
            List<IngredientDefinition> ingredientDefinitions)
        {
            orderDefinitions.ThrowIfNullOrEmptyArgument(nameof(orderDefinitions));
            ingredientDefinitions.ThrowIfNullOrEmptyArgument(nameof(ingredientDefinitions));

            _orderDefinitions = orderDefinitions;
            _ingredientDefinitions = ingredientDefinitions;
            Initialize();
        }

        private void Initialize()
        {
            Day = new Day(MaxOrderCount, _orderDefinitions, 1);
            Blender = new Blender(MaxIngredientsPerSmoothieCount);
            Inventory = new Inventory(_ingredientDefinitions,
                MaxIngredientsAvailableAtOnce,
                NewIngredientSpawnDurationSec);
        }

        public void Start(float timeNow)
        {
            Inventory.Start(timeNow);
            StartNewDay(1, timeNow);
        }

        public void Update(float timeNow)
        {
            Inventory.Update(timeNow);
            Day.UpdateOrderTimers(timeNow);
        }

        public void SelectIngredient(Ingredient selectedIngredient, float timeNow)
        {
            selectedIngredient.ThrowIfNullArgument(nameof(selectedIngredient));

            if (!Blender.TryAddIngredient(selectedIngredient.Definition))
                return;

            Inventory.RemoveIngredient(selectedIngredient);

            if (Blender.HasMaxIngredients)
                CompleteCurrentOrder(timeNow);
        }

        public void CompleteCurrentOrder(float timeNow)
        {
            var completedSmoothie = Blender.CompleteSmoothie();
            Day.CompleteOrder(Day.PendingOrders[0], completedSmoothie);

            if (Day.HasDayEnded)
            {
                UnityEngine.Debug.Log($"The day has ended! Rating: {Day.Rating.AverageRating} stars!");

                OnDayEnded?.Invoke(Day);
                return;
            }

            if (Day.TryStartNewOrder(timeNow))
            {
                Blender.StartSmoothie(_ingredientDefinitions.GetRandomElement());
            }
        }

        public void StartNextDay(float timeNow)
        {
            int previousDayNumber = Day.DayNumber;
            StartNewDay(previousDayNumber + 1, timeNow);
        }

        private void StartNewDay(int dayNumber, float timeNow)
        {
            Blender.StartSmoothie(_ingredientDefinitions.GetRandomElement());

            if (Day != null)
                Day.OnOrderTimerRanOut -= OnOrderTimerRanOut;

            int previousDayNumber = Day.DayNumber;
            Day = new Day(MaxOrderCount, _orderDefinitions, dayNumber);
            Day.OnOrderTimerRanOut += OnOrderTimerRanOut;
            Day.Start(timeNow);
            OnDayStarted?.Invoke(Day);
        }

        private void OnOrderTimerRanOut(Order order, float timeNow)
        {
            CompleteCurrentOrder(timeNow);
        }
    }
}
