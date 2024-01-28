// Manages the overall state of the game.

using HBMatch3.BoardStatus;
using HBMatch3.Recorder;
using HBMatch3.Tiles;

namespace HBMatch3.GameState
{
    public class GameStateManager
    {
        private BoardStatusModel boardStatusModel;
        private RecorderModel recorderModel;

        public GameStateManager(BoardStatusModel boardStatus, RecorderModel recorder)
        {
            boardStatusModel = boardStatus;
            recorderModel = recorder;
        }

        public GameTile[] GetCurrentBoard()
        {
            return boardStatusModel.GetBoard();
        }

        public int GetCurrentScore()
        {
            /* Implement if needed */
            return 0;}

        public int GetNumberOfMoves()
        {
            return recorderModel.GetMovesPlayed();
        }

        public int GetTilesDestroyed()
        {
            return recorderModel.GetTilesDestroyed();
        }
    }
}