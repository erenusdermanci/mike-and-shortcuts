using System;
using System.IO;
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
            Console.WriteLine(string.Join(' ', MikeAndShortcuts2(n, shortcuts).Select(val => val.ToString()).ToArray()));
        }

        private static int[] MikeAndShortcuts(int n, int[] shortcuts)
        {
            var minCosts = new int[n];

            var shortcutsHashMap = new Dictionary<int, int>(); // key: starting position, value: destination
            var minShortcutCostForDestination =
                new Dictionary<int, int[]>(); // key: destination, value: starting position [0] and cost [1]

            // fill up our shortcut data
            for (var i = 0; i < n; ++i)
            {
                var destinationIdx = shortcuts[i] - 1;
                if (!minShortcutCostForDestination.ContainsKey(destinationIdx))
                    minShortcutCostForDestination[destinationIdx] = new[] { i, i };
                else if (minShortcutCostForDestination[destinationIdx][1] > i)
                    minShortcutCostForDestination[destinationIdx] = new[] { i, i };

                shortcutsHashMap[i] = destinationIdx;
            }

            minCosts[0] = 0;
            for (var i = 1; i < n; ++i)
            {
                var cost = Math.Min(i, minCosts[i - 1] + 1); // base cost

                if (minShortcutCostForDestination.ContainsKey(i))
                {
                    var costWithShortcut = minShortcutCostForDestination[i][1] + 1;
                    if (costWithShortcut < cost)
                    {
                        cost = costWithShortcut;
                    }
                }

                if (shortcutsHashMap.ContainsKey(i))
                {
                    if (minShortcutCostForDestination[shortcutsHashMap[i]][1] > cost)
                    {
                        minShortcutCostForDestination[shortcutsHashMap[i]][0] = i;
                        minShortcutCostForDestination[shortcutsHashMap[i]][1] = cost;
                    }
                }
                
                minCosts[i] = cost;
            }

            for (var i = n - 1 - 1; i > 0; --i)
            {
                if (minCosts[i - 1] > minCosts[i] + 1)
                    minCosts[i - 1] = minCosts[i] + 1;
            }

            return minCosts;
        }

        private static int[] MikeAndShortcuts2(int n, int[] shortcuts)
        {
            var minCosts = new int[n];
            var visited = new bool[n];

            for (var i = 0; i < n; ++i)
            {
                minCosts[i] = int.MaxValue;
                visited[i] = false;
            }

            minCosts[0] = 0;

            for (var i = 0; i < n - 1; ++i)
            {
                var unvisited = UnvisitedMinimumIndex(minCosts, visited, n);
                visited[unvisited] = true;

                for (var j = 0; j < n; ++j)
                {
                    var minCostU2J = Math.Min(Math.Abs((unvisited +  1) - (j + 1)), shortcuts[unvisited] == j + 1 ? 1 : int.MaxValue);
                    if (!visited[j] && minCosts[unvisited] != int.MaxValue &&
                        minCosts[unvisited] + minCostU2J < minCosts[j])
                    {
                        minCosts[j] = minCosts[unvisited] + minCostU2J;
                    }
                }
            }

            return minCosts;
        }
        
        private static int UnvisitedMinimumIndex(int[] minCosts, bool[] visited, int n)
        {
            var min = int.MaxValue;
            var minIndex = -1;

            for (var i = 0; i < n; ++i)
            {
                if (visited[i] == false && minCosts[i] <= min)
                {
                    min = minCosts[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}
