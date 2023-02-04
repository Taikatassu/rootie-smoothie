using UnityEngine;

namespace RootieSmoothie.Content
{
    [CreateAssetMenu(fileName = "NewIngredient", menuName = "New Ingredient", order = 0)]
    public class IngredientScriptableObject : ScriptableObject
    {
        public IngredientDefinition Definition;
    }
}
