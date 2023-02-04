using System;
using UnityEngine;

namespace RootieSmoothie.Content
{
    [Serializable]
    public struct OrderDefinition
    {
        public Color Color;
        public string RequiredIngredientId;
        public string CharacterAssetPath;

        public OrderDefinition(Color color, string requiredIngredientId, string characterAssetPath)
        {
            Color = color;
            RequiredIngredientId = requiredIngredientId;
            CharacterAssetPath = characterAssetPath;
        }
    }
}
