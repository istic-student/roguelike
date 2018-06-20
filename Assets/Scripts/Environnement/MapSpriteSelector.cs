using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environnement
{
	public class MapSpriteSelector : MonoBehaviour {
	
		public Sprite 	spU, spD, spR, spL,
				spUD, spRL, spUR, spUL, spDR, spDL,
				spULD, spRUL, spDRU, spLDR, spUDRL;
		public bool up, down, left, right;
		public RoomType RoomType = RoomType.NormalRoom;
		public Color normalRoomColor, spawnRoomColor, BossRoomColor, TreasureRoomColor, SecretRoomColor ;
		Color mainColor;
		SpriteRenderer rend;
		void Start () {
			rend = GetComponent<SpriteRenderer>();
			mainColor = normalRoomColor;
			PickSprite();
			PickColor();
		}

		void PickSprite(){ //picks correct sprite based on the four door bools
			if (up){
				if (down){
					if (right){
						if (left){
							rend.sprite = spUDRL;
						}else{
							rend.sprite = spDRU;
						}
					}else if (left){
						rend.sprite = spULD;
					}else{
						rend.sprite = spUD;
					}
				}else{
					if (right){
						if (left){
							rend.sprite = spRUL;
						}else{
							rend.sprite = spUR;
						}
					}else if (left){
						rend.sprite = spUL;
					}else{
						rend.sprite = spU;
					}
				}
				return;
			}
			if (down){
				if (right){
					if(left){
						rend.sprite = spLDR;
					}else{
						rend.sprite = spDR;
					}
				}else if (left){
					rend.sprite = spDL;
				}else{
					rend.sprite = spD;
				}
				return;
			}
			if (right){
				if (left){
					rend.sprite = spRL;
				}else{
					rend.sprite = spR;
				}
			}else{
				rend.sprite = spL;
			}
		}

		void PickColor(){
			switch (RoomType)
			{
				case RoomType.NormalRoom:
					mainColor = normalRoomColor;
					break;
				case RoomType.SpawnRoom:
					mainColor = spawnRoomColor;
					break;
				case RoomType.BossRoom:
					mainColor = BossRoomColor;
					break;
				case RoomType.TreasureRoom:
					mainColor = TreasureRoomColor;					
					break;	
				case RoomType.SecretRoom:
					mainColor = SecretRoomColor;
					break;
				default:
					rend.color = mainColor;
					break;
			}
			rend.color = mainColor;
		}
	}
}