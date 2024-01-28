namespace HBMatch3.Tiles
{
    public class GameTile
    {
        public TileType Type { get; set; }
        public int Score { get; set; }

        // Constructor
        public GameTile(TileType type, int score)
        {
            Type = type;
            Score = score;
        }

        // You can add more properties and methods relevant to your game's logic here
    }
    
}