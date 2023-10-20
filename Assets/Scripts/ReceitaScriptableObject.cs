using Assets.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Receita")]
public class ReceitaScriptableObject : ScriptableObject
{
    public ItemScriptableObj Saida;

    public int QuantidadeSaida;
    public float TempoFabricacao;

    public IngredienteReceita[] Ingredientes;
}