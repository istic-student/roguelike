using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Interactive.Activable;
using Assets.Scripts.Player;

namespace Assets.Scripts.Environnement
{
	public class RoomInstance : MonoBehaviour {
		public Vector2 gridPos;
		public RoomType RoomType;
        public RoomInstance roomTop, roomBot, roomLeft, roomRight;

        public Tile WallTile, FloorTile, DoorTile;

        Door doorU, doorD, doorL, dooR;

        float tilesSize;

        Vector2 roomSizeInTiles = new Vector2(17,9);

        Tilemap Floor, Wall, Animated, Interactive;

		public RoomInstance(Vector2 _gridPos, RoomType _RoomType, Tile _WallTile, Tile _FloorTile, Tile _DoorTile){
			gridPos = _gridPos;
			RoomType = _RoomType;
            WallTile = _WallTile;
            FloorTile = _FloorTile;
            DoorTile = _DoorTile;
            Start();
		}

        void Start() {
            Floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
            Wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
            Animated = GameObject.FindGameObjectWithTag("Animated").GetComponent<Tilemap>();
            Interactive = GameObject.FindGameObjectWithTag("Interactive").GetComponent<Tilemap>();
            
            GenerateRoomTiles();
        }

        public void PlayerEnteringRoom() {
            Camera MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();            
            MainCamera.transform.Translate(new Vector2(gridPos.x+roomSizeInTiles.x,gridPos.y+ roomSizeInTiles.y - 2));
            PlayerController Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Player.transform.Translate(new Vector2(gridPos.x+roomSizeInTiles.x,gridPos.y+roomSizeInTiles.y));           
        }
        
        public void CreateDoors() {
            Vector2 spawnPosition = new Vector2(gridPos.x, gridPos.y);
            PlaceDoor(spawnPosition, roomTop != null, doorU);
            spawnPosition = new Vector2(gridPos.x, gridPos.y);
            PlaceDoor(spawnPosition, roomBot != null, doorD);
            spawnPosition = new Vector2(gridPos.x, gridPos.y);
            PlaceDoor(spawnPosition, roomRight != null, dooR);
            spawnPosition = new Vector2(gridPos.x, gridPos.y);
            PlaceDoor(spawnPosition, roomLeft != null, doorL);
        }

        void PlaceDoor(Vector2 spawnPosition, bool createDoor, Door door) {
            if(createDoor) {
                Debug.Log("Install door");
                Vector3Int currentCell = Interactive.WorldToCell(spawnPosition);
                Interactive.SetTile(new Vector3Int(currentCell.x, currentCell.y, currentCell.z), DoorTile);
            }
        }

        void GenerateRoomTiles() {
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
