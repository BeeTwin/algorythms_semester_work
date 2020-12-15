using System;

namespace algorythms_semester_work
{
    public static class RandomExtentions
    {
        /// <summary>
        ///     Returns a random double that is within a specified range.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns>
        ///     A double-precision floating point number that is greater then or equal to <paramref name="minValue"/>, 
        ///     and less than <paramref name="maxValue"/>.
        /// </returns>
        public static double NextDouble(this Random random, double minValue, double maxValue)
            => random.NextDouble() * (maxValue - minValue) + minValue;
    }
}