using System.Collections.Generic;
using UnityEngine;

namespace HBMatch3.Tiles
{
    public class TileFactory
    {
        private Queue<GameTile> pool;
        private GameObject tilePrefab;

        public TileFactory(GameObject prefab)
        {
            pool = new Queue<GameTile>();
            tilePrefab = prefab;
        }

        public GameTile GetTile(TileType type)
        {
            GameTile tile;

            if (pool.Count > 0)
            {
                tile = pool.Dequeue();
                // Reactivate the tile if you are also pooling GameObjects
            }
            else
            {
                tile = new GameTile(type, DetermineScoreByType(type));
                // If you are using GameObjects, instantiate them here
            }

            // Additional setup for the tile, if necessary
            SetupTile(tile, type);

            return tile;
        }

        public void ReleaseTile(GameTile tile)
        {
            // Reset tile state if necessary
            ResetTile(tile);

            // Deactivate the tile's GameObject if you are using GameObject pooling
            // ...

            pool.Enqueue(tile);
        }

        private void SetupTile(GameTile tile, TileType type)
        {
            // Configure tile properties
            tile.Type = type;
            tile.Score = DetermineScoreByType(type);

            // If using GameObjects, set up the GameObject here
            // ...
        }

        private void ResetTile(GameTile tile)
        {
            // Reset tile properties if necessary
            // ...
        }

        private int DetermineScoreByType(TileType type)
        {
            switch (type)
            {
                case TileType.Blue: return 1000;
                case TileType.Green: return 1200;
                case TileType.Purple: return 1400;
                case TileType.Grey: return 1300;
                case TileType.Yellow: return 1600;
                case TileType.Box: return 2000;
                case TileType.Grass: return 4000;
                case TileType.Ice: return 6000;
                default: return 0;
            }
        }
    }
}