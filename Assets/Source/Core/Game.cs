using System.Collections.Generic;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core.Blending;

namespace RootieSmoothie.Core
{
    public class Game
    {
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
            Day = new Day(MaxOrderCount, _orderDefinitions);
            Blender = new Blender(MaxIngredientsPerSmoothieCount);
            Inventory = new Inventory(_ingredientDefinitions,
                MaxIngredientsAvailableAtOnce,
                NewIngredientSpawnDurationSec);
        }

        public void Start(float timeNow)
        {
            Day.Start();
            Blender.StartSmoothie(_ingredientDefinitions.GetRandomElement());
            Inventory.Start(timeNow);
        }

        public void Update(float timeNow)
        {
            Inventory.Update(timeNow);
        }

        public void SelectIngredient(Ingredient selectedIngredient)
        {
            selectedIngredient.ThrowIfNullArgument(nameof(selectedIngredient));

            if (!Blender.TryAddIngredient(selectedIngredient.Definition))
                return;

            Inventory.RemoveIngredient(selectedIngredient);
        }

        public void CompleteOrder()
        {
            var completedSmoothie = Blender.CompleteSmoothie();
            Day.CompleteOrder(Day.PendingOrders[0], completedSmoothie);
        }
    }
}
