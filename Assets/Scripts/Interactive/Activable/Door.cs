using UnityEngine;
using Assets.Scripts.Utils;
using Assets.Scripts.Player;

namespace Assets.Scripts.Interactive.Activable
{
    public class Door : Abstract.Activable
    {
        private Camera _mainCamera;

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
            player.transform.Translate(34, 0, 0);
            _mainCamera.transform.Translate(34, 0, 0);
            // todo : remove collision and play animation
        }

    }
}
