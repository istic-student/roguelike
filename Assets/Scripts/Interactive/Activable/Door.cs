using Assets.Scripts.Envrionement;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Interactive.Activable
{
    public class Door
    {

        // General informations

        public Room ownerRoom;
        public Room connectingRoom;

        public DoorType doorType;

        // ---

        public bool isOpen = false;
               
        public BoxCollider2D Wall { get; set; }

        private Animator _animator;

        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && isOpen)
            {
                // Change player room
                connectingRoom.PlayerIsInRoom = true;
                ownerRoom.PlayerIsInRoom = false;
            }
        }

        public void Unlock()
        {
            Debug.Log("Open door");
            isOpen = true;
            // todo : remove collision and play animation
        }

        public void Lock()
        {
            Debug.Log("close door");
            isOpen = false;
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
