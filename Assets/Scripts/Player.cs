using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player instance;
        public PlayerStatus Status { get; set; }
        public PlayerUI PlayerUI { get; set; }

        private void Awake()
        {
            Status = GetComponent<PlayerStatus>();
            PlayerUI = GetComponent<PlayerUI>();
            instance = this;
        }
    }
}