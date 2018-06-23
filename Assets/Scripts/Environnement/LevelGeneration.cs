﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environnement
{
	public class LevelGeneration : MonoBehaviour {
		Vector2 worldSize = new Vector2(4,4);
		Room[,] rooms;
		List<Vector2> takenPositions = new List<Vector2>();

		List<Room> roomList = new List<Room>();
		int gridSizeX, gridSizeY, numberOfRooms = 20;
		public GameObject roomWhiteObj;

		// specials rooms
		private bool _BossRoomGenerated;
		private int _NumberOfSecretRooms, _NumberOfTreasuresRooms;

		void Start () {
			// If there is too many rooms for the map size
			if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2)){ 
				numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
			}

			gridSizeX = Mathf.RoundToInt(worldSize.x);
			gridSizeY = Mathf.RoundToInt(worldSize.y);

			_NumberOfSecretRooms = Mathf.RoundToInt(numberOfRooms * 0.05f);
			_NumberOfTreasuresRooms = Mathf.RoundToInt(numberOfRooms * 0.10f);

			CreateRooms(); //lays out the actual map			
			SetRoomDoors(); //assigns the doors where rooms would connect
			ChangeSomeNormalRoomToSpecialRoom(); //Change normal rooms to special ones
			DrawMap(); //instantiates objects to make up a map
		}
		void CreateRooms(){
			//setup - Creating spawn room
			rooms = new Room[gridSizeX * 2,gridSizeY * 2];
			Room _Room = new Room(Vector2.zero, RoomType.SpawnRoom); 
			roomList.Add(_Room);
			rooms[gridSizeX,gridSizeY] = _Room;
			takenPositions.Insert(0,Vector2.zero);
			Vector2 checkPos = Vector2.zero;
			//magic numbers
			float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
			//add rooms
			for (int i =0; i < numberOfRooms -1; i++){
				float randomPerc = ((float) i) / (((float)numberOfRooms - 1));
				randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
				//grab new position
				checkPos = NewPosition();
				//test new position
				if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare){
					int iterations = 0;
					do {
						checkPos = SelectiveNewPosition();
						iterations++;
					}while(NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
					if (iterations >= 50)
						print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
				}
				//finalize position
				_Room = new Room(checkPos, RoomType.NormalRoom);
				roomList.Add(_Room);
				rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = _Room;
				takenPositions.Insert(0,checkPos);
			}	
		}

		void ChangeSomeNormalRoomToSpecialRoom() {
			//boss room
			Debug.Log("Searching location for a boss room");		
				for (int i = 1; i < roomList.Count; i++)
				{
					if(roomList[i].RoomType == RoomType.NormalRoom && (
					(roomList[i].doorBot && !roomList[i].doorLeft && !roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && roomList[i].doorLeft && !roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && !roomList[i].doorLeft && roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && !roomList[i].doorLeft && !roomList[i].doorRight && roomList[i].doorTop))) {
						roomList[i].RoomType = RoomType.BossRoom;
						break;
					}

				}

			//secret room
			if(_NumberOfSecretRooms >= 1) {		
				 Debug.Log("Searching location for a secret room");		
				for (int i = 1; i < roomList.Count; i++)
				{
					if(roomList[i].RoomType == RoomType.NormalRoom && (
					(roomList[i].doorBot && !roomList[i].doorLeft && !roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && roomList[i].doorLeft && !roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && !roomList[i].doorLeft && roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && !roomList[i].doorLeft && !roomList[i].doorRight && roomList[i].doorTop))) {
						if(_NumberOfSecretRooms == 1) {
							_NumberOfSecretRooms = 0;
							break;
						}
						_NumberOfSecretRooms--;
						roomList[i].RoomType = RoomType.SecretRoom;
						break;
					}

				}
			}

			//treasure room
			if(_NumberOfTreasuresRooms >= 1) {
				Debug.Log("Searching location for a treasure room");			
				for (int i = 1; i < roomList.Count; i++)
				{
					if(roomList[i].RoomType == RoomType.NormalRoom && (
					(roomList[i].doorBot && !roomList[i].doorLeft && !roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && roomList[i].doorLeft && !roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && !roomList[i].doorLeft && roomList[i].doorRight && !roomList[i].doorTop)
					|| (!roomList[i].doorBot && !roomList[i].doorLeft && !roomList[i].doorRight && roomList[i].doorTop))) {
						roomList[i].RoomType = RoomType.TreasureRoom;
						if(_NumberOfTreasuresRooms == 1) {
							_NumberOfTreasuresRooms = 0;
							break;
						}
						_NumberOfTreasuresRooms--;
					}

				}
			}

		}
		Vector2 NewPosition(){
			int x = 0, y = 0;
			Vector2 checkingPos = Vector2.zero;
			do{
				int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
				x = (int) takenPositions[index].x;//capture its x, y position
				y = (int) takenPositions[index].y;
				bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
				bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
				if (UpDown){ //find the position bnased on the above bools
					if (positive){
						y += 1;
					}else{
						y -= 1;
					}
				}else{
					if (positive){
						x += 1;
					}else{
						x -= 1;
					}
				}
				checkingPos = new Vector2(x,y);
			}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
			return checkingPos;
		}
		Vector2 SelectiveNewPosition(){ // method differs from the above in the two commented ways
			int index = 0, inc = 0;
			int x =0, y =0;
			Vector2 checkingPos = Vector2.zero;
			do{
				inc = 0;
				do{ 
					//instead of getting a room to find an adject empty space, we start with one that only 
					//as one neighbor. This will make it more likely that it returns a room that branches out
					index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
					inc ++;
				}while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
				x = (int) takenPositions[index].x;
				y = (int) takenPositions[index].y;
				bool UpDown = (Random.value < 0.5f);
				bool positive = (Random.value < 0.5f);
				if (UpDown){
					if (positive){
						y += 1;
					}else{
						y -= 1;
					}
				}else{
					if (positive){
						x += 1;
					}else{
						x -= 1;
					}
				}
				checkingPos = new Vector2(x,y);
			}while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
			if (inc >= 100){ // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
				print("Error: could not find position with only one neighbor");
			}
			return checkingPos;
		}
		int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions){
			int ret = 0; // start at zero, add 1 for each side there is already a room
			if (usedPositions.Contains(checkingPos + Vector2.right)){ //using Vector.[direction] as short hands, for simplicity
				ret++;
			}
			if (usedPositions.Contains(checkingPos + Vector2.left)){
				ret++;
			}
			if (usedPositions.Contains(checkingPos + Vector2.up)){
				ret++;
			}
			if (usedPositions.Contains(checkingPos + Vector2.down)){
				ret++;
			}
			return ret;
		}
		void DrawMap(){
			foreach (Room room in rooms){
				if (room == null){
					continue; //skip where there is no room
				}
				Vector2 drawPos = room.gridPos;
				drawPos.x *= 16;//aspect ratio of map sprite
				drawPos.y *= 8;
				//create map obj and assign its variables
				MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
				mapper.RoomType = room.RoomType;
				mapper.up = room.doorTop;
				mapper.down = room.doorBot;
				mapper.right = room.doorRight;
				mapper.left = room.doorLeft;
			}
		}
		void SetRoomDoors(){
			for (int x = 0; x < ((gridSizeX * 2)); x++){
				for (int y = 0; y < ((gridSizeY * 2)); y++){
					if (rooms[x,y] == null){
						continue;
					}
					Vector2 gridPosition = new Vector2(x,y);
					if (y - 1 < 0){ //check above
						rooms[x,y].doorBot = false;
					}else{
						rooms[x,y].doorBot = (rooms[x,y-1] != null);
					}
					if (y + 1 >= gridSizeY * 2){ //check bellow
						rooms[x,y].doorTop = false;
					}else{
						rooms[x,y].doorTop = (rooms[x,y+1] != null);
					}
					if (x - 1 < 0){ //check left
						rooms[x,y].doorLeft = false;
					}else{
						rooms[x,y].doorLeft = (rooms[x - 1,y] != null);
					}
					if (x + 1 >= gridSizeX * 2){ //check right
						rooms[x,y].doorRight = false;
					}else{
						rooms[x,y].doorRight = (rooms[x+1,y] != null);
					}
				}
			}
		}
	}

}