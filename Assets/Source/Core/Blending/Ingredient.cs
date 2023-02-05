using RootieSmoothie.Content;

namespace RootieSmoothie.Core.Blending
{
    public class Ingredient
    {
        public IngredientDefinition Definition { get; private set; }

        public Ingredient(IngredientDefinition definition)
        {
            Definition = definition;
        }
    }
}
