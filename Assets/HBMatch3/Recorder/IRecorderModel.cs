namespace HBMatch3.Recorder
{
    public interface IRecorderModel
    {
        void RecordTileDestroyed();
        void RecordMove();
        int GetTilesDestroyed();
        int GetMovesPlayed();
    }
}