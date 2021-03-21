using System;
using System.Collections.Generic;

namespace PathFinder.Model
{
    public class Map : Graph
    {
        Tile[,] tiles;
        int nbRows;
        int nbCols;

        Tile beginningNode;
        Tile exitNode;

        List<Node> nodesList = null;
        List<Arc> arcsList = null;

        private String[] mapRows;

        public Map(String _map, int _beginRow, int _beginColumn, int _exitRow, int _exitColumn)
        {
            CreateTilesArray(_map);
            FillMap();
            InitBeginAndEndNodes(_beginRow, _beginColumn, _exitRow, _exitColumn);
            InitNodesAndArcsLists();
        }

        private void CreateTilesArray(string _map)
        {
            mapRows = _map.Split(new char[] { '\n' });
            nbRows = mapRows.Length;
            nbCols = mapRows[0].Length;
            tiles = new Tile[nbRows, nbCols];
        }

        private void FillMap()
        {
            for (int row = 0; row < nbRows; row++)
            {
                for (int col = 0; col < nbCols; col++)
                {
                    tiles[row, col] = new Tile(TileTypeConverter.TypeFromChar(mapRows[row][col]), row, col);
                }
            }
        }

        private void InitBeginAndEndNodes(int _beginRow, int _beginColumn, int _exitRow, int _exitColumn)
        {
            beginningNode = tiles[_beginRow, _beginColumn];
            beginningNode.DistanceFromBeginning = beginningNode.Cost();
            exitNode = tiles[_exitRow, _exitColumn];
        }

        private void InitNodesAndArcsLists()
        {
            NodesList();
            ArcsList();
        }

        public Node BeginningNode()
        {
            return beginningNode;
        }

        public Node ExitNode()
        {
            return exitNode;
        }

        public List<Node> NodesList()
        {
            if (nodesList == null)
            {
                nodesList = new List<Node>();
                foreach (Node node in tiles)
                {
                    nodesList.Add(node);
                }
            }
            return nodesList;
        }

        public List<Node> NodesList(Node _currentNode)
        {
            List<Node> nodesList = new List<Node>();

            int currentRow = ((Tile)_currentNode).Row;
            int currentCol = ((Tile)_currentNode).Col;

            // Add neighbors if not null and reachable 
            if (currentRow - 1 >= 0 && tiles[currentRow - 1, currentCol].IsValidPath())
            {
                nodesList.Add(tiles[currentRow - 1, currentCol]);
            }
            if (currentCol - 1 >= 0 && tiles[currentRow, currentCol - 1].IsValidPath())
            {
                nodesList.Add(tiles[currentRow, currentCol - 1]);
            }
            if (currentRow + 1 < nbRows && tiles[currentRow + 1, currentCol].IsValidPath())
            {
                nodesList.Add(tiles[currentRow + 1, currentCol]);
            }
            if (currentCol + 1 < nbCols && tiles[currentRow, currentCol + 1].IsValidPath())
            {
                nodesList.Add(tiles[currentRow, currentCol + 1]);
            }

            return nodesList;
        }

        public int NodesCount()
        {
            return nbRows * nbCols;
        }

        public List<Arc> ArcsList()
        {
            if (arcsList == null)
            {
                arcsList = new List<Arc>();

                for (int row = 0; row < nbRows; row++)
                {
                    for (int col = 0; col < nbCols; col++)
                    {
                        if (tiles[row, col].IsValidPath())
                        {
                            // Top
                            if (row - 1 >= 0 && tiles[row - 1, col].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, col], tiles[row - 1, col], tiles[row - 1, col].Cost()));
                            }
                            // Left
                            if (col - 1 >= 0 && tiles[row, col - 1].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, col], tiles[row, col - 1], tiles[row, col - 1].Cost()));
                            }
                            // Bottom
                            if (row + 1 < nbRows && tiles[row + 1, col].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, col], tiles[row + 1, col], tiles[row + 1, col].Cost()));
                            }
                            // Right
                            if (col + 1 < nbCols && tiles[row, col + 1].IsValidPath())
                            {
                                arcsList.Add(new Arc(tiles[row, col], tiles[row, col + 1], tiles[row, col + 1].Cost()));
                            }
                        }
                    }
                }
            }
            return arcsList;
        }

        public List<Arc> ArcsList(Node _currentNode)
        {
            List<Arc> list = new List<Arc>();

            int currentRow = ((Tile)_currentNode).Row;
            int currentCol = ((Tile)_currentNode).Col;

            // Return all arcs to neighbors if reachable
            if (currentRow - 1 >= 0 && tiles[currentRow - 1, currentCol].IsValidPath())
            {
                list.Add(new Arc(_currentNode, tiles[currentRow - 1, currentCol], tiles[currentRow - 1, currentCol].Cost()));
            }
            if (currentCol - 1 >= 0 && tiles[currentRow, currentCol - 1].IsValidPath())
            {
                list.Add(new Arc(_currentNode, tiles[currentRow, currentCol - 1], tiles[currentRow, currentCol - 1].Cost()));
            }
            if (currentRow + 1 < nbRows && tiles[currentRow + 1, currentCol].IsValidPath())
            {
                list.Add(new Arc(_currentNode, tiles[currentRow + 1, currentCol], tiles[currentRow + 1, currentCol].Cost()));
            }
            if (currentCol + 1 < nbCols && tiles[currentRow, currentCol + 1].IsValidPath())
            {
                list.Add(new Arc(_currentNode, tiles[currentRow, currentCol + 1], tiles[currentRow, currentCol + 1].Cost()));
            }

            return list;
        }

        public double CostBetweenNodes(Node _fromNode, Node _toNode)
        {
            return ((Tile)_toNode).Cost();
        }

        public String ReconstructPath()
        {
            String resPath = "";
            Tile currentNode = exitNode;
            Tile prevNode = (Tile) exitNode.Precursor;
            while (prevNode != null)
            {
                resPath = "-" + currentNode.ToString() + resPath;
                currentNode = prevNode;
                prevNode = (Tile) prevNode.Precursor;
            }
            resPath = currentNode.ToString() + resPath;
            return resPath;
        }

        public void ComputeEstimatedDistanceToExit()
        {
            foreach (Tile tile in tiles)
            {
                tile.EstimatedDistanceToExit = Math.Abs(exitNode.Row - tile.Row) + Math.Abs(exitNode.Col - tile.Col);
            }
        }


        public void Clear()
        {
            nodesList = null;
            arcsList = null;
            for (int row = 0; row < nbRows; row++)
            {
                for (int col = 0; col < nbCols; col++)
                {
                    tiles[row, col].DistanceFromBeginning = double.PositiveInfinity;
                    tiles[row, col].Precursor = null;
                }
            }
            beginningNode.DistanceFromBeginning = beginningNode.Cost();
        }
    }
}
