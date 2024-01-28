using System;

namespace HBMatch3.Recorder
{
    /// <summary>
    /// Description:
    ///The RecorderView class implements the IRecorderView interface and provides console-based
    /// methods for visually displaying the number of tiles destroyed and moves played.
    /// </summary>
    public class RecorderView : IRecorderView
    {
        public void DisplayTilesDestroyed(int tilesDestroyed)
        {
            Console.WriteLine($"Tiles Destroyed: {tilesDestroyed}");
        }

        public void DisplayMovesPlayed(int movesPlayed)
        {
            Console.WriteLine($"Moves Played: {movesPlayed}");
        }
    }
}