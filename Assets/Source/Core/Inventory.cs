using System;
using System.Collections.Generic;
using System.Linq;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core.Blending;

namespace RootieSmoothie.Core
{
    public class Inventory
    {
        public Action OnIngredientsUpdated;

        public IReadOnlyList<Ingredient> AvailableIngredients => _availableIngredients.ToList();
        public int MaxIngredientsAvailableAtOnce => _availableIngredients.Length;
        public float TimeToNewIngredientSpawnSec => _ingredientSpawnTimer.TimeToEnd;

        private List<IngredientDefinition> _potentialIngredients;
        private Ingredient[] _availableIngredients;
        private Timer _ingredientSpawnTimer;
        private float _newIngredientSpawnDurationSec;

        public Inventory(List<IngredientDefinition> potentialIngredients,
            int maxIngredientsAvailableAtOnce,
            float maxIngredientAvailabilityDurationSec)
        {
            potentialIngredients.ThrowIfNullOrEmptyArgument(nameof(potentialIngredients));
            maxIngredientsAvailableAtOnce.ThrowIfNotPositive(nameof(maxIngredientsAvailableAtOnce));
            maxIngredientAvailabilityDurationSec.ThrowIfNotPositive(nameof(maxIngredientAvailabilityDurationSec));

            _potentialIngredients = potentialIngredients;
            _availableIngredients = new Ingredient[maxIngredientsAvailableAtOnce];
            _newIngredientSpawnDurationSec = maxIngredientAvailabilityDurationSec;
            _ingredientSpawnTimer = new Timer();
        }

        public void Start(float timeNow)
        {
            InitAvailableIngredients();
            ResetIngredientTimer(timeNow);
        }

        private void InitAvailableIngredients()
        {
            for (int i = 0; i < MaxIngredientsAvailableAtOnce; i++)
                _availableIngredients[i] = GetRandomIngredient();

            OnIngredientsUpdated?.Invoke();
        }

        private Ingredient GetRandomIngredient()
        {
            var randomIngredientDefinition = _potentialIngredients.GetRandomElement();
            return new Ingredient(randomIngredientDefinition);
        }

        private void ResetIngredientTimer(float timeNow)
        {
            _ingredientSpawnTimer.Start(_newIngredientSpawnDurationSec, timeNow);
        }

        public void Update(float timeNow)
        {
            _ingredientSpawnTimer.Update(timeNow);

            if (_ingredientSpawnTimer.IsDone)
            {
                SpawnNewAvailableIngredient();
                ResetIngredientTimer(timeNow);
            }
        }

        private void SpawnNewAvailableIngredient()
        {
            MoveIngredientsForwards();
            AddNewLastIngredient();

            OnIngredientsUpdated?.Invoke();
        }

        private void MoveIngredientsForwards()
        {
            for (int i = 0; i < _availableIngredients.Length - 1; i++)
                _availableIngredients[i] = _availableIngredients[i + 1];
        }

        private void AddNewLastIngredient()
        {
            Ingredient newRandomIngredient = GetRandomIngredient();
            _availableIngredients[_availableIngredients.Length - 1] = newRandomIngredient;
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            for (int i = 0; i < _availableIngredients.Length; i++)
            {
                if (_availableIngredients[i] == ingredient)
                {
                    _availableIngredients[i] = null;
                }
            }

            OnIngredientsUpdated?.Invoke();
        }
    }
}
