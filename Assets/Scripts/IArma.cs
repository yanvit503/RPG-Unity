using UnityEngine;

namespace Assets.Scripts
{
    public interface IArma
    {
        ArmaScriptableObject ScriptableObject { get; set; }
        GameObject gameObject { get; set; }
        bool Equipada { get; set; }
        void Atirar();
    }
}
