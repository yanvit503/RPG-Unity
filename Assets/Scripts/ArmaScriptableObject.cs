using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Arma")]
    public class ArmaScriptableObject : ScriptableObject
    {
        public string Nome;
        public int QuantidadeDano;
        public float Alcance;
        public bool ArmaDeFogo;
    }
}