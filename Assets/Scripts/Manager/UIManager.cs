using System;
using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Manager
{
    public class UIManager : MonoBehaviour
    {

        public Text Time;
        public GameObject PlayerPanel;
        public Dictionary<PlayerController, PlayerUI> playersUI;

        private void Start()
        {
            playersUI = new Dictionary<PlayerController, PlayerUI>();
            InitPlayersUI();
        }

        private void InitPlayersUI()
        {
            foreach (var player in GameManager.AssetsManager.Players)
            {
                var pUI = CreatePlayerUI();
                if (pUI == null)
                    continue;
                player.PlayerChange += OnPlayerChange;
                playersUI.Add(player, pUI);
                pUI.SetData(player);
            }
        }

        private PlayerUI CreatePlayerUI()
        {
            var ui = Instantiate(GameManager.AssetsManager.PlayerUIPrefab);
            var pUI = ui.GetComponentInChildren<PlayerUI>();
            if (pUI == null)
                return null;
            pUI.transform.SetParent(PlayerPanel.transform, false);
            return pUI;
        }

        public void SetTime(int timeLeft)
        {
            var timeSpan = new TimeSpan(0, 0, timeLeft);
            var time = string.Format("{0:00}:{1:00}",
                (int)timeSpan.TotalMinutes,
                timeSpan.Seconds);
            Time.text = time;
        }

        private void OnPlayerChange(PlayerController playerController)
        {
            PlayerUI playerUI;
            if (playersUI.TryGetValue(playerController, out playerUI))
                playerUI.SetData(playerController);
        }

    }
}
