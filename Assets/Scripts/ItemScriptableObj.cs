using UnityEngine;

[CreateAssetMenu(menuName = "Inventario/Item")]
public class ItemScriptableObj : ScriptableObject
{
    public string Nome;
    public int Quantidade;
    public Sprite Icone;
    public bool Estacavel;
}