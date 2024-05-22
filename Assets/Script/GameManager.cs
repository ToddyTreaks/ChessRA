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
