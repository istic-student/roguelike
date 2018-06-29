using Assets.Scripts.Abstract;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterLight : MonoBehaviour
    {
        public GameObject _lightLine;

        private CharacterController _characterController;
        void Start() {
            _lightLine = GameObject.Find("LightLine");
            _characterController = GetComponent<CharacterController>();
        }
        public void Update() {
            RotateLightPrecise();
        }
        public void RotateLightPrecise() {
            _lightLine.transform.eulerAngles =  new Vector3Int(0, 0, (int) getAngle(_characterController.preciseDirection.x, _characterController.preciseDirection.y));
        }
        
        public float getAngle(float x, float y) {
            if (x == 0 && y == 0)
                return 180;
            else if (x >= 0 && y > 0)
                return (270 + (Mathf.Atan2(y, x) * 180 / Mathf.PI));
            else if (x > 0 && y <= 0)
                return (270 + (Mathf.Atan(y / x) * 180 / Mathf.PI));
            else if (x <= 0 && y < 0)
                return (90 + (Mathf.Atan(y / x) * 180 / Mathf.PI));
            else if (x < 0 && y >= 0)
                return ( 90 + (Mathf.Atan(y / x) * 180 / Mathf.PI));
            else
                return 180;
        }
    }
}