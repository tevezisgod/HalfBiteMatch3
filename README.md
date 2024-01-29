Overview of DOron Kanaan'sy Match-3 Game Project

CLasses and interfaces:
1. GameTile Class

What It Does: Represents each tile on the board with its own type and score.
Why It's Here: It's the core of our game's puzzle mechanics.
2. TileType Enum

What It Does: Defines the various types of tiles in the game.
Why It's Here: Adds variety and strategy elements to our game.
3. BoardStatusModel (Complements IBoardStatusModel Interface)

What It Does: Keeps track of the game board's status, like the best row and column.
Why It's Here: It's the analytical part that helps in strategizing moves.
4. RecorderModel (Aligned with IRecorderModel Interface)

What It Does: Records gameplay stats like tiles destroyed and moves made.
Why It's Here: Helps in tracking progress and provides insights into gameplay.
5. EventBus Class

What It Does: Manages the flow of events within the game.
Why It's Here: Facilitates smooth communication between different components.
6. TileFactory Class

What It Does: Handles the creation of new GameTile instances.
Why It's Here: Centralizes tile creation for consistency and rule enforcement.
7. BoardView Class

What It Does: Manages the visual representation of the game board.
Why It's Here: Transforms game data into a visual format for player interaction.
8. GameStatusView Class (Works with IGameStatusView Interface)

What It Does: Shows the current game status, like scores and potential matches.
Why It's Here: Provides visual feedback to the player on game progress.
9. BoardStatusController Class

What It Does: Links the BoardStatusModel and BoardView.
Why It's Here: Bridges the gap between game logic and visual representation.
10. GameStatusController Class (Connected to IGameStatusController Interface)

What It Does: Manages the flow of game status information.
Why It's Here: Ensures that the game's UI is in sync with the gameplay.
