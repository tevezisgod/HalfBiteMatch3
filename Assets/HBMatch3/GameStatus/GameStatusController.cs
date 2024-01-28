
using HBMatch3.GameState;

namespace HBMatch3.GameStatus
{
    public class GameStatusController : IGameStatusController
    {
        private GameStateManager gameStateManager;
        private IGameStatusView gameStatusView;

        public GameStatusController(GameStateManager gameStateManager, IGameStatusView gameStatusView)
        {
            this.gameStateManager = gameStateManager;
            this.gameStatusView = gameStatusView;
        }

        public void UpdateGameStatus()
        {
            // Logic to update game status and view
        }
    }
}