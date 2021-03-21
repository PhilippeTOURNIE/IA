namespace PathFinder.Model
{
    internal class Tile : Node
    {
        protected TileType tileType;

        internal int Row { get; set; }
        internal int Col { get; set; }

        public Tile(TileType _type, int _row, int _col) 
        {
            tileType = _type;
            Row = _row;
            Col = _col;
        }

        internal bool IsValidPath()
        {
            return tileType.Equals(TileType.Bridge) || tileType.Equals(TileType.Grass) || tileType.Equals(TileType.Path);
        }

        internal double Cost()
        {
            switch (tileType)
            {
                case TileType.Path :
                    return 1;
                case TileType.Bridge:
                case TileType.Grass:
                    return 2;
                default :
                    return double.PositiveInfinity;
            }
        }

        public override string ToString()
        {
            return "[" + Row + ";" + Col + ";" + tileType.ToString() + "]";
        }
    }
}
