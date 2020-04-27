using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.Model
{
    public struct City
    {
        public String Name;

        public City(string p)
        {
            Name = p;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
