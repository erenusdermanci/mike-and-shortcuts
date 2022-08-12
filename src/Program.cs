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
            MikeAndShortcuts(3, new int[] { 2, 2, 3 });
            MikeAndShortcuts(5, new int[] { 1, 2, 3, 4, 5 });
            MikeAndShortcuts(7, new int[] { 4, 4, 4, 4, 7, 7, 7 });
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

            return minCosts;
        }
    }
}