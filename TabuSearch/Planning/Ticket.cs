using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintPlanning
{
    public class Ticket
    {
        public double Priority { get; set; }
        public double StoryPoint { get; set; }

        String Name { get; set; }

        public Ticket(String _name, double _weight, double _value)
        {
            Name = _name;
            Priority = _weight;
            StoryPoint = _value;
        }

        public override string ToString()
        {
            return Name + " (" + Priority + ", " + StoryPoint + ")";
        }
    }
}