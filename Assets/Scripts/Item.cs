using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObj ItemSO;

    [HideInInspector]
    private int Quantidade;

    void Awake()
    {
        Quantidade = ItemSO.Quantidade;
    }

    public int Soma(int qnt)
    {
        return Quantidade + qnt;
    }
    
    public int AtualizaQuantidade(int qnt)
    {
        Quantidade = qnt;
        return Quantidade;
    } 
    
    public int GetQuantidade()
    {
        return Quantidade;
    }
}