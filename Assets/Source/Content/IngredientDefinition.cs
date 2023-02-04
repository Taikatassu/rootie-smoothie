using System;
using UnityEngine;

namespace RootieSmoothie.Content
{
    [Serializable]
    public struct IngredientDefinition
    {
        public string Id;
        public string AssetPath;
        public Color Color;

        public IngredientDefinition(string id, string assetPath, Color color)
        {
            Id = id;
            AssetPath = assetPath;
            Color = color;
        }
    }
}
