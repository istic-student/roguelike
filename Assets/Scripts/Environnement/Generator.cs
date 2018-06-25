using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Environnement
{
    public class Generator : MonoBehaviour {

        public Vector2 gridPos;

        public Tile WallTile, FloorTile;

        float tilesSize = 32;
        int roomSizeX = 9, roomSizeY = 17;
        Vector2 roomSizeInTiles = new Vector2(9,17);

        Tilemap Floor, Wall;

        // Use this for initialization
        void Start() {
            Floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
            Wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
            GenerateRoomTiles();
        }

        void GenerateRoomTiles() {
            Debug.Log("Generation de la map");
            GenerateFloorTiles();
            GenerateWallTiles();
        }

        void GenerateFloorTiles() {
            Vector3Int currentCell = Floor.WorldToCell(gridPos);
            for (int x = 0; x < roomSizeX; x++)           
            {                
                for (int y = 0; y < roomSizeY; y++)
                {
                    Floor.SetTile(new Vector3Int(currentCell.x + y, currentCell.y + x, currentCell.z), FloorTile);
                }   
            }
        }

        void GenerateWallTiles() {
            Vector3Int currentCell = Floor.WorldToCell(gridPos);
            //Nort wall
            for (int x = 0; x < roomSizeX; x++)           
            {               
                Wall.SetTile(new Vector3Int(currentCell.x + x, currentCell.y, currentCell.z), WallTile);
            }
        }
    }
}