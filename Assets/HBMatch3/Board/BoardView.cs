using System;
using HBMatch3.Tiles;
using UnityEngine;

namespace HBMatch3.Board
{
    public class BoardView : MonoBehaviour
    {
        private const int gridSize = 8; // Assuming an 8x8 grid for this example.
        private GameTile[,] tiles = new GameTile[gridSize, gridSize];

        // Reference to the Tile Prefab.
        [SerializeField] private GameObject tilePrefab;

        private TileFactory tileFactory;

        void Start()
        {
            tileFactory = new TileFactory(tilePrefab);
            InitializeBoardView();
        }

        private void InitializeBoardView()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    TileType randomType = (TileType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(TileType)).Length);

                    // Create the tile using the TileFactory.
                    GameTile gameTile = tileFactory.GetTile(randomType);

                    // Create and position the visual GameObject for the tile.
                    GameObject tileObject = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);
                    tileObject.name = $"Tile_{x}_{y}";

                    // Here you can set up the visual representation of the tile based on gameTile properties
                    // For example, setting the sprite based on the TileType.
                    
                    // Store the GameTile model in the array for future reference.
                    tiles[x, y] = gameTile;
                }
            }
        }

        public void UpdateBoard(GameTile[] currentBoard)
        {
            // Update each tile's appearance based on the currentBoard data.
            // This could involve activating/deactivating tiles, updating their types, etc.
        }

        // Additional methods as needed...
    }
}