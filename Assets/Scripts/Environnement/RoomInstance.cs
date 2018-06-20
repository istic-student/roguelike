using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interactive.Activable;

namespace Assets.Scripts.Environnement
{
	public class RoomInstance : MonoBehaviour {
		public Vector2 gridPos;
		public RoomType RoomType;
		public bool doorTop, doorBot, doorLeft, doorRight;

        Door doorU, doorD, doorL, dooR;
		public RoomInstance(Vector2 _gridPos, RoomType _RoomType){
			gridPos = _gridPos;
			RoomType = _RoomType;
		}

        void start() {
            CreateDoors();
            GenerateRoomTiles();
        }
        
        void CreateDoors() {

        }

        void GenerateRoomTiles() {
            
        }
	}
}
