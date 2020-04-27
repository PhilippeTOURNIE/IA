using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.Model
{
    public static class Travelling
    {
        public static List<City> cities;
        public static int[][] distances;

        public static int getDistance(City c1, City c2)
        {
            return distances[cities.IndexOf(c1)][cities.IndexOf(c2)];
        }
    }
}
