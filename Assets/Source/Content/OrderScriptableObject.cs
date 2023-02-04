using UnityEngine;

namespace RootieSmoothie.Content
{
    [CreateAssetMenu(fileName = "NewOrder", menuName = "New Order", order = 1)]
    public class OrderScriptableObject : ScriptableObject
    {
        public OrderDefinition Definition;
    }
}
