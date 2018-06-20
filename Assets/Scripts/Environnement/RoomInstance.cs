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

        public Tile wallTile, floorTile;

        Door doorU, doorD, doorL, dooR;

        float tilesSize = 32;
        int roomSizeX = 9, roomSizeY = 17;
        Vector2 roomSizeInTiles = new Vector2(9,17);

        Tilemap Floor, Wall, Animated, Interactive;

		public RoomInstance(Vector2 _gridPos, RoomType _RoomType){
			gridPos = _gridPos;
			RoomType = _RoomType;
		}

        void start() {
            Floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
            Wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
            Animated = GameObject.FindGameObjectWithTag("Animated").GetComponent<Tilemap>();
            Interactive = GameObject.FindGameObjectWithTag("Interactive").GetComponent<Tilemap>();
            CreateDoors();
            GenerateRoomTiles();
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
            Vector3Int currentCell = Floor.WorldToCell(gridPos);
            for (int x = 0; x < roomSizeX; x++)           
            {                
                for (int y = 0; y < roomSizeY; y++)
                {
                    GenerateRoomTiles(new Vector3Int(currentCell.x, currentCell.y, currentCell.z), floorTile, Floor);
                }   
            }
        }
        
        void GenerateRoomTiles(Vector3Int spawnPos, Tile tile, Tilemap tileMap) {
            tileMap.SetTile(spawnPos, tile);
        }
	}
}
