﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environnement
{
	public class Room {
		public Vector2 gridPos;
		public RoomType RoomType;
		public bool doorTop, doorBot, doorLeft, doorRight;
		public Room roomTop, roomBot, roomLeft, roomRight;
		public Room(Vector2 _gridPos, RoomType _RoomType){
			gridPos = _gridPos;
			RoomType = _RoomType;
		}
	}

}