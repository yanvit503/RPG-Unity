using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Item/Ingrediente")]
    public class IngredienteReceita : ScriptableObject
    {
        public ItemScriptableObj Item;
        public int QuantidadeNecessaria;
    }
}
