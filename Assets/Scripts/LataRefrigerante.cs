using UnityEngine;

namespace Assets.Scripts
{
    public class LataRefrigerante : Interagivel, IBebida
    {
        [SerializeField]
        float quantidadeBeber;

        public void Beber(float quantidade)
        {
            Player.instance.Status.Beber(quantidadeBeber);
        }

        public override void Interagir()
        {
            Beber(quantidadeBeber);
        }
    }
}