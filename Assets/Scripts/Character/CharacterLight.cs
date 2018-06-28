using Assets.Scripts.Abstract;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterLight : MonoBehaviour
    {
        public GameObject _lightLine;
        void Start() {
            _lightLine = GameObject.Find("LightLine");
        }

        public void RotateLight(int orientation) {
            switch(orientation) {
                case 0:
                    _lightLine.transform.eulerAngles =  new Vector3Int(0, 0, 0);
                    break;
                case 1:
                    _lightLine.transform.eulerAngles =  new Vector3Int(0, 0, 270);
                    break;
                case 2:
                    _lightLine.transform.eulerAngles =  new Vector3Int(0, 0, 180);
                    break;
                case 3:
                    _lightLine.transform.eulerAngles =  new Vector3Int(0, 0, 90);
                    break;
            }
        }

    }
}