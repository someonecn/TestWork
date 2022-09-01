using System;

namespace _Code_Random
{
    public static class RandomExtensions
    {
        static Random _random = new Random();
        public static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(_random.Next(v.Length));
        }
    }
}

