using System;
using UnityEngine;

namespace RootieSmoothie
{
    public static class RandomHelper
    {
        private static System.Random s_rnd;

        private static void InitializeRandom()
        {
            s_rnd = new System.Random();
        }

        public static float GetRandomRange(float min, float max)
        {
            if (s_rnd == null)
                InitializeRandom();
            return Mathf.Lerp(min, max, s_rnd.Next(0, int.MaxValue) / (float)int.MaxValue);
        }
    }
}