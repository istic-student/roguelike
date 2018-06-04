using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Envrionement
{
    public class Map : MonoBehaviour

    {
        public int NumberOfRooms;
        public int NumberOfTreasureRooms;
        public int NumberOfSecretRooms;

        public int roomBlockXSize = 8;
        public int roomBlockYSize = 8;

        private List<Room> Rooms = new List<Room>();

    }
}


