using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObj ItemSO;

    [HideInInspector]
    public int Quantidade;

    void Awake()
    {
        Quantidade = ItemSO.Quantidade;
    }
}