namespace HBMatch3.Recorder
{
    public class RecorderModel : IRecorderModel
    {
        private int tilesDestroyed;
        private int movesPlayed;

        public void RecordTileDestroyed() { tilesDestroyed++; }
        public void RecordMove() { movesPlayed++; }
        public int GetTilesDestroyed() { return tilesDestroyed; }
        public int GetMovesPlayed() { return movesPlayed; }
    }
}