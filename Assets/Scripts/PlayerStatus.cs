using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStatus : MonoBehaviour
    {
        public float Vida { get; set; }
        public float Fome { get; set; }
        public float Sede { get; set; }

        internal void Beber(float quantidadeBeber)
        {
            Sede -= quantidadeBeber;

            if (Sede < 0)
                Sede = 0;

            Debug.Log(Sede);
        }

        private void Awake()
        {
            Vida = 100;
            Sede = 20;
            Fome = 0;
        }
    }
}