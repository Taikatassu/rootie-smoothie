using RootieSmoothie.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace RootieSmoothie.CommonExtensions
{
    public static class CommonExtensions
    {
        // Collections:
        public static T GetRandomElement<T>(this List<T> collection, Random random = null)
        {
            if (random == null)
                random = new Random();

            return collection[random.Next(0, collection.Count)];
        }

        public static T GetRandomElement<T>(this T[] collection, Random random = null)
        {
            if (random == null)
                random = new Random();

            return collection[random.Next(0, collection.Length)];
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }

        public static void ThrowIfNullOrEmptyArgument<T>(this IEnumerable<T> collection, string argumentName)
        {
            if (collection.IsNullOrEmpty())
                throw new ArgumentNullException($"Collection {argumentName} was null!");
        }

        public static void ThrowIfNullOrEmptyReference<T>(this IEnumerable<T> collection, string referenceName)
        {
            if (collection.IsNullOrEmpty())
                throw new NullReferenceException($"Collection {referenceName} was null!");
        }

        // Objects:
        public static void ThrowIfNullReference(this object o, string referenceName)
        {
            if (o == null)
                throw new NullReferenceException($"Reference to {referenceName} was null!");
        }

        public static void ThrowIfNullArgument(this object o, string argumentName)
        {
            if (o == null)
                throw new ArgumentNullException($"Argument {argumentName} was null!");
        }

        // Ints:
        public static void ThrowIfNegative(this int value, string valueName)
        {
            if (value < 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was negative!");
        }

        public static void ThrowIfPositive(this int value, string valueName)
        {
            if (value > 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was positive!");
        }

        public static void ThrowIfNotNegative(this int value, string valueName)
        {
            if (value >= 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was not negative!");
        }

        public static void ThrowIfNotPositive(this int value, string valueName)
        {
            if (value <= 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was not positive!");
        }

        // Floats:
        public static void ThrowIfNegative(this float value, string valueName)
        {
            if (value < 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was negative!");
        }

        public static void ThrowIfPositive(this float value, string valueName)
        {
            if (value > 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was positive!");
        }

        public static void ThrowIfNotNegative(this float value, string valueName)
        {
            if (value >= 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was not negative!");
        }

        public static void ThrowIfNotPositive(this float value, string valueName)
        {
            if (value <= 0)
                throw new InvalidOperationException($"Value ({value}) of {valueName} was not positive!");
        }

        // Scriptable object conversion:
        public static List<OrderDefinition> ToDefinitionList(
            this List<OrderScriptableObject> scriptableObjects)
        {
            List<OrderDefinition> definitions = new List<OrderDefinition>();
            foreach (var scriptableObject in scriptableObjects)
                definitions.Add(scriptableObject.Definition);

            return definitions;
        }

        public static List<IngredientDefinition> ToDefinitionList(
            this List<IngredientScriptableObject> scriptableObjects)
        {
            List<IngredientDefinition> definitions = new List<IngredientDefinition>();
            foreach (var scriptableObject in scriptableObjects)
                definitions.Add(scriptableObject.Definition);

            return definitions;
        }
    }
}
