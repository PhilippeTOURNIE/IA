using System;

namespace PathFinder.Model
{
    public enum TileType { Grass, Tree, Water, Bridge, Path };

    internal static class TileTypeConverter
    {
        public static TileType TypeFromChar(Char _c)
        {
            switch (_c)
            {
                case ' ':
                    return TileType.Grass;
                case '*':
                    return TileType.Tree;
                case 'X':
                    return TileType.Water;
                case '=':
                    return TileType.Bridge;
                case '.':
                    return TileType.Path;
            }
            throw new FormatException();
        }
    }
}
