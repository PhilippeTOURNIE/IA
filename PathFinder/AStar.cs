using System;
using System.Collections.Generic;
using System.Linq;


namespace PathFinder
{

        public class AStar : Algorithm
        {
            public AStar(Graph _graph, IHM _gui) : base(_graph, _gui) { }

            private List<Node> nodesToVisit;
            private bool exitReached;
            private Node currentNode;

            protected override void Run()
            {
                Initialisation();

                while (nodesToVisit.Count != 0 && !exitReached)
                {
                    ChooseNextNodeToApply();

                    if (currentNode == graph.ExitNode())
                    {
                        exitReached = true;
                    }
                    else
                    {
                        ApplyAllArcsFromCurrentNode();
                    }
                }
            }

            private void Initialisation()
            {
                graph.ComputeEstimatedDistanceToExit();
                nodesToVisit = graph.NodesList();
                exitReached = false;
            }

            private void ChooseNextNodeToApply()
            {
                currentNode = nodesToVisit.FirstOrDefault();
                foreach (Node newNode in nodesToVisit)
                {
                    if ((newNode.DistanceFromBeginning + newNode.EstimatedDistanceToExit) < (currentNode.DistanceFromBeginning + currentNode.EstimatedDistanceToExit))
                    {
                        currentNode = newNode;
                    }
                }
            }

            private void ApplyAllArcsFromCurrentNode()
            {
                List<Arc> arcsFromCurrentNode = graph.ArcsList(currentNode);

                foreach (Arc arc in arcsFromCurrentNode)
                {
                    if (arc.FromNode.DistanceFromBeginning + arc.Cost < arc.ToNode.DistanceFromBeginning)
                    {
                        arc.ToNode.DistanceFromBeginning = arc.FromNode.DistanceFromBeginning + arc.Cost;
                        arc.ToNode.Precursor = arc.FromNode;
                    }
                }

                nodesToVisit.Remove(currentNode);
            }
        }
    }
