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

        public DoorType DoorType;
        public Vector2 gridPos;

        public GameObject Prefab;

        private Orientation _Orientation;

        public Door(Vector2 _GridPos,  RoomInstance _LinkRoom) {
            gridPos = _GridPos;
            LinkRoom =  _LinkRoom;            
            Start();
        }
        public void Start()
        {
            _Orientation = Orientation.North;
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();            
        }    
        protected override void Unlock()
        {
            Debug.Log("Open door");

            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.transform.SetPositionAndRotation(new Vector3(LinkRoom.gridPos.x + LinkRoom.roomSizeInTiles.x, LinkRoom.gridPos.y + LinkRoom.roomSizeInTiles.y ,1), Quaternion.identity);
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
