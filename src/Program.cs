using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int argCount = 2;
            int n = -1;
            string s;
            int[] shortcuts = null;
            while (argCount > 0 && ((s = Console.ReadLine()) != null))
            {
                if (n == -1)
                {
                    n = int.Parse(s);
                }
                else
                {
                    shortcuts = s.Split(' ').Select(a => int.Parse(a)).ToArray();
                }

                argCount--;
            }

            Console.WriteLine(string.Join(' ', MikeAndShortcuts(n, shortcuts).Select(val => val.ToString()).ToArray()));
        }

        private static int[] MikeAndShortcuts(int n, int[] shortcuts)
        {
            var minCosts = new int[n];
            var queue = new Queue<int>();

            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                var x = queue.Dequeue();

                if (x + 1 < n && minCosts[x + 1] == 0)
                {
                    minCosts[x + 1] = minCosts[x] + 1;
                    queue.Enqueue(x + 1);
                }

                if (x - 1 > 0 && minCosts[x - 1] == 0)
                {
                    minCosts[x - 1] = minCosts[x] + 1;
                    queue.Enqueue(x - 1);
                }

                var shortcut = shortcuts[x] - 1;
                if (shortcut > x && minCosts[shortcut] == 0)
                {
                    minCosts[shortcut] = minCosts[x] + 1;
                    queue.Enqueue(shortcut);
                }
            }

            return minCosts;
        }
    }
}
