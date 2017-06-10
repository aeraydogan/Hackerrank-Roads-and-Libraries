using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;

class Solution
{
    /// <summary>
    /// When city visited set true
    /// </summary>
    private static List<bool> visited;

    /// <summary>
    /// Cities Vektors. Fill this dictinary for both of cities that is wrote. Because Roads are bidiractinal.
    /// We can define a class for this implementation. It is increase readiblaty
    /// Key : City name as number
    /// Value : Connected city name
    /// </summary>
    private static Dictionary<int,List<int>> vektors;

    private static long minimumRoadCount = 0;

    static void Main(String[] args)
    {
        int q = Convert.ToInt32(Console.ReadLine());
        for (int a0 = 0; a0 < q; a0++)
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);
            int x = Convert.ToInt32(tokens_n[2]);
            int y = Convert.ToInt32(tokens_n[3]);

            vektors = new Dictionary<int, List<int>>();
            
            for (int a1 = 0; a1 < m; a1++)
            {
                string[] tokens_city_1 = Console.ReadLine().Split(' ');
                int city1 = Convert.ToInt32(tokens_city_1[0]);
                int city2 = Convert.ToInt32(tokens_city_1[1]);

                //Add Vector for City1
                AddVector(city1, city2);

                //Add Vektor for City2
                AddVector(city2,city1);
            }

            visited = new List<bool>(new bool[n]);
            long totalCost = 0;

            for (int i = 1; i <= n; ++i)
            {
                if (visited[i-1] == false)
                {
                    // use Deep First Search algorithm for all unvisited City
                    dfs(i);

                    // When we find city group that are connected. Calculate cheapest cost, according to roads cost or libraries cost.
                    // We have two alternative, First alternative is mininum roads count and one library. Second alternative, All of the cities have own library.
                    totalCost += ((minimumRoadCount * y + x) < (minimumRoadCount + 1) * x ? (minimumRoadCount * y + x) : (minimumRoadCount + 1) * x);
                    minimumRoadCount = 0;
                }
            }
            Console.WriteLine(totalCost.ToString());
        }
    }

    private static void AddVector(int city1, int city2)
    {
        if (!vektors.ContainsKey(city1))
        {
            var a = new List<int>();
            a.Add(city2);
            vektors.Add(city1, a);
        }
        else
        {
            var a = vektors[city1];
            a.Add(city2);
            vektors[city1] = a;
        }
    }

    /// <summary>
    /// Deep First Search
    /// </summary>
    /// <param name="roadIndex"></param>
    private static void dfs(int roadIndex)
    {
        visited[roadIndex-1] = true;
        if (vektors.ContainsKey(roadIndex))
        {
            for (int i = 0; i < vektors[roadIndex].Count(); ++i)
            {
                if (visited[vektors[roadIndex][i]-1] == false)
                {
                    minimumRoadCount++;
                    dfs(vektors[roadIndex][i]);

                }
            }
        }
    }
}
