namespace HBMatch3.Recorder
{
    /// <summary>
    /// Description:
    /// The IRecorderView interface outlines methods for displaying statistics related to tile destruction and moves played.
    /// </summary>
    public interface IRecorderView
    {
        void DisplayTilesDestroyed(int tilesDestroyed);
        void DisplayMovesPlayed(int movesPlayed);
    }
}