using System;
using System.Collections;
using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {

        [Header("Manager")]
        public static GameManager Instance;
        public static AssetsManager AssetsManager;
        public UIManager UIManager;

        [Header("Game")]
        private float _time;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            AssetsManager = GetComponent<AssetsManager>();
        }

        private void Start()
        {
            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            yield return new WaitForSeconds(1);
            foreach (var player in AssetsManager.Players)
            {
                var h = player.GetComponent<Health>();
                if (h != null)
                    h.TakeDamage(10);
            }
        }

        private void Update()
        {
            _time += Time.deltaTime;
            UIManager.SetTime((int)Math.Ceiling(_time));
        }

        private void End()
        {
        }

    }
}
