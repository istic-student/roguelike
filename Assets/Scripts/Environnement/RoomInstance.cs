using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Interactive.Activable;

namespace Assets.Scripts.Environnement
{
	public class RoomInstance : MonoBehaviour {
		public Vector2 gridPos;
		public RoomType RoomType;
		public bool doorTop, doorBot, doorLeft, doorRight;

        public Tile WallTile, FloorTile;

        Door doorU, doorD, doorL, dooR;

        float tilesSize;

        Vector2 roomSizeInTiles = new Vector2(17,9);

        Tilemap Floor, Wall, Animated, Interactive;

		public RoomInstance(Vector2 _gridPos, RoomType _RoomType,  Tile _WallTile,  Tile _FloorTile){
			gridPos = _gridPos;
			RoomType = _RoomType;
            WallTile = _WallTile;
            FloorTile = _FloorTile;
            Start();
		}

        void Start() {
            Floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
            Wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
            Animated = GameObject.FindGameObjectWithTag("Animated").GetComponent<Tilemap>();
            Interactive = GameObject.FindGameObjectWithTag("Interactive").GetComponent<Tilemap>();
            
            GenerateRoomTiles();
            CreateDoors();
        }
        
        void CreateDoors() {
            Vector3 spawnPosition = transform.position + Vector3.up*(roomSizeInTiles.y/4 * tilesSize) - Vector3.up*(tilesSize/4);
            PlaceDoor(spawnPosition, doorTop, doorU);
            spawnPosition = transform.position + Vector3.down*(roomSizeInTiles.y/4 * tilesSize) - Vector3.down*(tilesSize/4);
            PlaceDoor(spawnPosition, doorBot, doorD);
            spawnPosition = transform.position + Vector3.right*(roomSizeInTiles.x * tilesSize) - Vector3.right*(tilesSize);
            PlaceDoor(spawnPosition, doorRight, dooR);
            spawnPosition = transform.position + Vector3.left*(roomSizeInTiles.x * tilesSize) - Vector3.left*(tilesSize);
            PlaceDoor(spawnPosition, doorLeft, doorL);
        }

        void PlaceDoor(Vector3 spawnPosition, bool createDoor, Door door) {
            if(createDoor) {
                Instantiate(door, spawnPosition, Quaternion.identity).transform.parent = transform;
            }
        }

        void GenerateRoomTiles() {
            Debug.Log("Generation of rooms");
            GenerateFloorTiles();
            GenerateWallTiles();
        }

        void GenerateFloorTiles() {
            Vector3Int currentCell = Floor.WorldToCell(gridPos);
            for (int x = 0; x < roomSizeInTiles.x; x++)           
            {                
                for (int y = 0; y < roomSizeInTiles.y; y++)
                {
                  Floor.SetTile(new Vector3Int(currentCell.x + x, currentCell.y + y, currentCell.z), FloorTile);
                }   
            }
        }

        void GenerateWallTiles() {
            Vector3Int currentCell = Floor.WorldToCell(gridPos);
            //Nort and South wall
            for (int x = 0; x < roomSizeInTiles.x+2; x++)           
            {               
                Wall.SetTile(new Vector3Int(currentCell.x + x - 1, currentCell.y - 1, currentCell.z), WallTile);   
                Wall.SetTile(new Vector3Int(currentCell.x + x - 1, currentCell.y + (int) roomSizeInTiles.y, currentCell.z), WallTile);       
            }

            for (int y = 0; y < roomSizeInTiles.y + 1; y++)           
            {               
                Wall.SetTile(new Vector3Int(currentCell.x - 1, currentCell.y + y, currentCell.z), WallTile);   
                Wall.SetTile(new Vector3Int(currentCell.x + (int) roomSizeInTiles.x, currentCell.y + y, currentCell.z), WallTile);       
            }
        }
	}
}
