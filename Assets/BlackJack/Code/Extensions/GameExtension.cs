using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Code.Extensions
{
    public static class GameExtension
    {
        private static readonly Random _random = new Random();

        public static void Shuffle<T>(this Stack<T> stack)
        {
            var values = stack.ToArray();
            stack.Clear();
            foreach (var value in values.OrderBy(x => _random.Next()))
                stack.Push(value);
        }
    }
}