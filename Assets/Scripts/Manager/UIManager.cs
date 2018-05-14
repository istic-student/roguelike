using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Manager
{
    public class UIManager : MonoBehaviour
    {

        public Text Time;

        public void SetTime(int timeLeft)
        {
            var timeSpan = new TimeSpan(0, 0, timeLeft);
            var time = string.Format("{0:00}:{1:00}",
                (int)timeSpan.TotalMinutes,
                timeSpan.Seconds);
            Time.text = time;
        }

    }
}
