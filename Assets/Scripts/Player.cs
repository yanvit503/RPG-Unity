using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IDanificavel
    {
        public static Player instance;
        public PlayerStatus Status { get; set; }
        public PlayerUI PlayerUI { get; set; }

        public void Dano(int quantidade)
        {
            GetComponent<BarraVida>().Dano(quantidade);
        }

        public void Destruir()
        {
            Debug.Log("Morreu");
        }

        private void Awake()
        {
            Status = GetComponent<PlayerStatus>();
            PlayerUI = GetComponent<PlayerUI>();
            instance = this;
        }
    }
}