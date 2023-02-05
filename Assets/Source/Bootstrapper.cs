using System.Collections.Generic;
using RootieSmoothie.CommonExtensions;
using RootieSmoothie.Content;
using RootieSmoothie.Core;
using RootieSmoothie.View;
using RootieSmoothie.View.Blending;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RootieSmoothie
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField]
        private List<OrderScriptableObject> _orderDefinitions = null;
        [SerializeField]
        private List<IngredientScriptableObject> _ingredientDefinitions = null;

        [Header("Views")]
        [SerializeField]
        private BlenderView _blenderView = null;
        [SerializeField]
        private InventoryView _inventoryView = null;
        [SerializeField]
        private OrdersView _ordersView = null;
        [SerializeField]
        private DayView _dayView = null;

        private Game _game;

        private void Awake()
        {
            _orderDefinitions.ThrowIfNullOrEmptyReference(nameof(_orderDefinitions));

            _game = new Game(
                _orderDefinitions.ToDefinitionList(),
                _ingredientDefinitions.ToDefinitionList());

            _blenderView.Initialize(_game.Blender);
            _inventoryView.Initialize(_game.Inventory, _game.SelectIngredient);
            _ordersView.Initialize(_game);
            _dayView.Initialize(_game);
        }

        private void Start()
        {
            _game.Start(Time.time);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("Main");

            _game.Update(Time.time);
        }
    }
}
