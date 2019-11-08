namespace EditorMap
{
    public static class TileGenerator
    {
        public static Tile CreateTile(string tileType, bool flip)
        {
            switch (tileType)
            {
                case "Floor1":
                    return new Tile(tileType);
                case "Floor2":
                    return new Tile(tileType);
                case string s when IsBrownTable(s):
                    return new Tile(tileType, flip);
                case string s when IsGreenTable(s):
                    return new Tile(tileType, flip);
                case "SlotMachine":
                    return new Tile(tileType);
                case "Bar":
                    return new Tile(tileType);
                default:
                    return null;
            }
        }

        private static bool IsGreenTable(string s)
        {
            return s == "Table6" ||
                   s == "Table7";
        }

        private static bool IsBrownTable(string s)
        {
            return s == "Table1" ||
                   s == "Table2" ||
                   s == "Table3" ||
                   s == "Table4" ||
                   s == "Table5" ||
                   s == "Table8" ||
                   s == "Table9" ||
                   s == "Table10" ||
                   s == "Table11" ||
                   s == "Table12";
        }
    }
}