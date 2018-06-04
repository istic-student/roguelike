using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Envrionement
{
    public class Room : MonoBehaviour
    {

        // General room informations

        public int RoomPositionX { get; set; }
        public int RoomPositionY { get; set; }

        public int RoomSizeX { get; set; }
        public int RoomSizeY { get; set; }

        private RoomType RoomType;

        //private List<Interactive.Activable.Door> Doors = new List<Interactive.Activable.Door>();

        public bool IsVisibleOnMap;

        // -----

        public bool PlayerHasVisitedRoom { get; private set; }

        public List<Object> Enemies; // TODO: Replace Object by Ennemy

        private bool _playerIsInRoom;
        public bool PlayerIsInRoom
        {
            get { return _playerIsInRoom; }
            set
            {
                _playerIsInRoom = value;
                if (value)
                {
                    PlayerHasVisitedRoom = true;
                    IsVisibleOnMap = true;

                    // TODO: Show doors on the map
                    
                    //Enemies.ForEach(e => ...); enable/spawn enemies
                }

            }
        }

        public void OnPlayersEntersRoom()
        {
            PlayerIsInRoom = true;
            // if there is enemy
            if(Enemies.Count > 0)
            {
                //Doors.ForEach(d => d.Lock());
            } else
            {
                //Doors.ForEach(d => d.Unlock());
            }
            
        }
    }

    public enum RoomType
    {
        SpawnRoom,
        NormalRoom,
        TreasureRoom,
        BossRoom,
        SecretRoom
    }
}
