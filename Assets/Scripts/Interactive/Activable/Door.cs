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

        private Orientation _Orientation;
        public void Start()
        {
            _Orientation = Orientation.North;
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }    
        protected override void Unlock()
        {
            Debug.Log("Open door");

            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.transform.Translate(LinkRoom.gridPos);
            _mainCamera.transform.Translate(LinkRoom.gridPos);
            // todo : remove collision and play animation
        }

    }
}
