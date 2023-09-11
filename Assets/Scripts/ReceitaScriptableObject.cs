using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Receita")]
public class ReceitaScriptableObject : ScriptableObject
{
    public ItemScriptableObj Saida;

    public ItemScriptableObj[] Ingredientes;
}
