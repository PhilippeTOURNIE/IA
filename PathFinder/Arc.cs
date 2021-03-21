using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder
{
    public class Arc
    {
        internal Node FromNode { get; set; }
        internal Node ToNode { get; set; }
        internal double Cost { get; set; }

        internal Arc(Node _fromNode, Node _toNode, double _costNode)
        {
            FromNode = _fromNode;
            ToNode = _toNode;
            Cost = _costNode;
        }
    }
}
