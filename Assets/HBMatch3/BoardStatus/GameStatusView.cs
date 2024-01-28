using HBMatch3.GameStatus;
using HBMatch3.Tiles;

namespace HBMatch3.BoardStatus
{
    /// <summary>
    /// Implements the IGameStatusView interface to display the game status.
    /// </summary>
    public class GameStatusView : IGameStatusView
    {
        public void UpdateStatusDisplay()
        {
            throw new System.NotImplementedException();
        }

        public void DisplayBoard(GameTile[] board)
        {
            // Logic to display the game board
        }

        public void DisplayTilesDestroyed(int count)
        {
            // Logic to display the number of tiles destroyed
        }

        public void DisplayMovesPlayed(int count)
        {
            // Logic to display the number of moves played
        }
    }
}