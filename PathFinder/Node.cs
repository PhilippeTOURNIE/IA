using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder
{
    public abstract class Node
    {
        private Node precursor = null;
        internal Node Precursor
        {
            get
            {
                return precursor;
            }
            set
            {
                precursor = value;
            }
        }

        private double distanceFromBeginning = double.PositiveInfinity;
        internal double DistanceFromBeginning
        {
            get
            {
                return distanceFromBeginning;
            }
            set
            {
                distanceFromBeginning = value;
            }
        }

        internal double EstimatedDistanceToExit { get; set; }
    }
}
