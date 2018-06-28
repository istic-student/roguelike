using UnityEngine;
using Assets.Scripts.Utils;
using Assets.Scripts.Player;
using Assets.Scripts.Environnement;

namespace Assets.Scripts.Interactive.Activable
{
    public class Door : Abstract.Activable
    {
        private Camera _mainCamera;

        public RoomInstance LinkRoom;

        public RoomInstance OwnerRoom;

        public DoorType DoorType;
        public Vector2 gridPos;

        public GameObject Prefab;

        public Orientation Orientation;

        public Door(Vector2 _GridPos,  RoomInstance _LinkRoom,  RoomInstance _OwnerRoom) {
            gridPos = _GridPos;
            LinkRoom =  _LinkRoom;   
            OwnerRoom = _OwnerRoom;        
            Start();
        }
        public void Start()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();            
        }    
        protected override void Unlock()
        {
            Debug.Log("Open door");

            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            Vector3 posPlayer = new Vector3(0,0,0);
            switch(Orientation) {
					case Orientation.North:
						posPlayer = new Vector3(LinkRoom.gridPos.x + LinkRoom.roomSizeInTiles.x, LinkRoom.gridPos.y ,1);
						break;
					case Orientation.South:
						posPlayer = new Vector3(LinkRoom.gridPos.x + LinkRoom.roomSizeInTiles.x, LinkRoom.gridPos.y + LinkRoom.roomSizeInTiles.y*2 - 1, 1);
						break;
					case Orientation.East:
						posPlayer = new Vector3(LinkRoom.gridPos.x, LinkRoom.gridPos.y + LinkRoom.roomSizeInTiles.y ,1);
						break;
					case Orientation.West:
						posPlayer = new Vector3(LinkRoom.gridPos.x + LinkRoom.roomSizeInTiles.x * 2 - 1, LinkRoom.gridPos.y + LinkRoom.roomSizeInTiles.y ,1);
						break;
				}
            OwnerRoom.Mapper.PlayerLeftRoom();
            LinkRoom.PlayerEnteringRoom();
            player.transform.SetPositionAndRotation(posPlayer, Quaternion.identity);
            _mainCamera.transform.SetPositionAndRotation(new Vector3(LinkRoom.gridPos.x + LinkRoom.roomSizeInTiles.x, LinkRoom.gridPos.y + LinkRoom.roomSizeInTiles.y ,-15), Quaternion.identity);
            // todo : remove collision and play animation
        }      

    }

    public enum DoorType
        {
            normalDoor,
            bossDoor,
            secretDoor,
        }
}
