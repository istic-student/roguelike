using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Interactive.Activable;
using Assets.Scripts.Player;

namespace Assets.Scripts.Environnement
{
    public class RoomInstance {
		public Vector2 gridPos;
		public RoomType RoomType;
        public RoomInstance roomTop, roomBot, roomLeft, roomRight ;

        public Tile WallTile, FloorTile, DoorTile;

        public GameObject doorU, doorD, doorL, dooR;

        float tilesSize;

        public Vector2 roomSizeInTiles = new Vector2(17,9);

        Tilemap Floor, Wall, Animated, Interactive;

		public RoomInstance(Vector2 _gridPos, RoomType _RoomType, Tile _WallTile, Tile _FloorTile, Tile _DoorTile){
			gridPos = _gridPos;
			RoomType = _RoomType;
            WallTile = _WallTile;
            FloorTile = _FloorTile;
            DoorTile = _DoorTile;
            Start();
		}
        
        public void SetRoomLeft(RoomInstance _RoomLeft) {
            roomLeft = _RoomLeft;
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

        public string printRoom() {
            return "Salle: Pos = "+gridPos+", roomTop="+(roomTop != null)+", roomBot="+(roomBot  != null)+", roomLeft="+(roomLeft  != null)+", roomRight="+(roomRight  != null); 
        }
	}
}
