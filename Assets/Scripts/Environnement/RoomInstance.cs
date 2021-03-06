using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Interactive.Activable;
using Assets.Scripts.Player;

namespace Assets.Scripts.Environnement
{
    public class RoomInstance : MonoBehaviour {
		public Vector2 gridPos;
		public RoomType RoomType;
        public RoomInstance roomTop, roomBot, roomLeft, roomRight ;

        public Tile WallTile, FloorTile, DoorTile;

        public GameObject doorU, doorD, doorL, dooR;

        float tilesSize;

        public MapSpriteSelector Mapper;

        public Vector2 roomSizeInTiles = new Vector2(17,9);

        Tilemap Floor, Wall, Animated, Interactive;

        public bool isRoomVisited;
		        
        public void SetRoomLeft(RoomInstance _RoomLeft) {
            roomLeft = _RoomLeft;
        }

        void Start() {
            Floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Tilemap>();
            Wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
            Animated = GameObject.FindGameObjectWithTag("Animated").GetComponent<Tilemap>();
            Interactive = GameObject.FindGameObjectWithTag("Interactive").GetComponent<Tilemap>();
            isRoomVisited = false;        
        }

        public void PlayerEnteringRoom() {
            /*Camera MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();            
            MainCamera.transform.Translate(new Vector2(gridPos.x+roomSizeInTiles.x,gridPos.y+ roomSizeInTiles.y - 2));
            PlayerController Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Player.transform.Translate(new Vector2(gridPos.x+roomSizeInTiles.x,gridPos.y+roomSizeInTiles.y));*/    

            Mapper.PlayerEnterInRoom();
            if(!isRoomVisited) {
                switch(RoomType) {					
                        case RoomType.NormalRoom:
                            for (int i = 0; i < Random.Range(0,3); i++)
                            {
                                 GameObject ennemy;
                                if(Random.Range(0,100) > 50)
                                    ennemy = Instantiate(Resources.Load("Prefabs/Enemy Goblin"), new Vector3(gridPos.x+roomSizeInTiles.x, gridPos.y + roomSizeInTiles.y, 0), Quaternion.identity) as GameObject;
                                else 
                                    ennemy = Instantiate(Resources.Load("Prefabs/Enemy Skeleton"), new Vector3(gridPos.x+roomSizeInTiles.x, gridPos.y + roomSizeInTiles.y, 0), Quaternion.identity)  as GameObject;
                                ennemy.transform.parent = GameObject.Find("Room "+gridPos.x/100+", "+gridPos.y/100 + " - " + RoomType).transform;
                            }                            
                            break;
                        case RoomType.BossRoom:
                            Instantiate(Resources.Load("Prefabs/Boss Necromancer"), new Vector3(gridPos.x+roomSizeInTiles.x, gridPos.y + roomSizeInTiles.y, 0), Quaternion.identity);
                            break;
                        case RoomType.TreasureRoom:
                            Instantiate(Resources.Load("Prefabs/Interactive/Activable/Chest"), new Vector3(gridPos.x+roomSizeInTiles.x, gridPos.y + roomSizeInTiles.y, 0), Quaternion.identity);
                            GameObject lightObject = Instantiate(GameObject.Find("TreasureLight"), new Vector3((float) (gridPos.x + roomSizeInTiles.x + 1), (float) (gridPos.y + roomSizeInTiles.y +1), 0), Quaternion.identity);
                            lightObject.name = "Light-Treasure";
                            lightObject.transform.parent = GameObject.Find("Room "+gridPos.x/100+", "+gridPos.y/100 + " - " + RoomType).transform;
                            break;
                        case RoomType.SecretRoom:
                            Instantiate(Resources.Load("Prefabs/Interactive/Activable/Shop"), new Vector3(gridPos.x+roomSizeInTiles.x, gridPos.y + roomSizeInTiles.y, 0), Quaternion.identity);
                            break;
                        default:						
                            break;
                    }
            }
            isRoomVisited= true;
        }       

        void GenerateRoomTiles() {
            GenerateFloorTiles();
            GenerateWallTiles();
            if(RoomType.NormalRoom == RoomType) {
                GenerateObstacles();
            }            
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

        void GenerateObstacles() {
            int nbObstacles = Random.Range(0, 8);
            for (int i = 0; i < nbObstacles; i++)
            {
                Vector3Int currentCell = Wall.WorldToCell(gridPos);
                int randomX = 2 + Random.Range(0, 13);
                int randomY = 2 + Random.Range(0 ,5);
                Wall.SetTile(new Vector3Int(currentCell.x + randomX, currentCell.y + randomY, currentCell.z), WallTile);
                GameObject shaderObject = Instantiate(GameObject.Find("BlockShader"), new Vector3((float) (gridPos.x + randomX*2 + 1), (float) (gridPos.y + randomY*2 +1), currentCell.z), Quaternion.identity);
                shaderObject.name = "BlockShader-"+i;
                shaderObject.transform.parent = GameObject.Find("Room "+gridPos.x/100+", "+gridPos.y/100 + " - " + RoomType).transform;
            }
        }

        public override string ToString() {
            return "Salle: Pos = "+gridPos+", roomTop="+(roomTop != null)+", roomBot="+(roomBot  != null)+", roomLeft="+(roomLeft  != null)+", roomRight="+(roomRight  != null); 
        }
	}
}
