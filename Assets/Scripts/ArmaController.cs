using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class ArmaController : MonoBehaviour
    {
        public static ArmaController Instancia;

        public IArma ArmaEquipada { get; set; }

        public List<IArma> Armas;
        Dictionary<KeyCode, int> teclaParaIndice = new Dictionary<KeyCode, int>();

        private void Awake()
        {
            Instancia = this;
            Armas = new List<IArma>();

            teclaParaIndice[KeyCode.Alpha1] = 0;
            teclaParaIndice[KeyCode.Alpha2] = 1;
        }

        private void Update()
        {
            foreach (var tecla in teclaParaIndice)
            {
                if (Input.GetKeyDown(tecla.Key) && tecla.Value >= 0 && tecla.Value < Armas.Count)
                {
                    EquiparArma(Armas[tecla.Value]);
                }
            }
        }

        public void EquiparArma(IArma arma)
        {
            var armaInventario = Armas.Where(x => x.Equals(arma)).FirstOrDefault();

            if (armaInventario != null)
            {
                ArmaEquipada = arma;
                arma.Equipada = true;
                armaInventario.gameObject.SetActive(true);

                foreach (var outras in Armas.Where(x => !x.Equals(arma)))
                {
                    outras.gameObject.SetActive(false);
                }
            }
            else
                Debug.Log("Arma não está no inventário");
        }

    }
}