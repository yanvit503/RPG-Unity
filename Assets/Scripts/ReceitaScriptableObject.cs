using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Receita")]
public class ReceitaScriptableObject : ScriptableObject
{
    public ItemScriptableObj Saida;

    public int QuantidadeSaida;

    public IngredienteReceita[] Ingredientes;
}