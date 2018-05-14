using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class AssetsManager : MonoBehaviour
    {

        [Header("UI")]
        public PlayerController[] Players;
        public PlayerUI PlayerUIPrefab;

    }
}
