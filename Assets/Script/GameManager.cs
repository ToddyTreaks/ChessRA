using System;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public SetupBoard setupBoard;

        private void Awake()
        {
            setupBoard = GetComponent<SetupBoard>();
        }

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
