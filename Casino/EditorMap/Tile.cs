namespace EditorMap
{
    public class Tile
    {
        public string TileType { get; set; }
        public bool Rotate { get; set; }

        public Tile(string tileType, bool rotate = false)
        {
            TileType = tileType;
            Rotate = rotate;
        }
    }
}