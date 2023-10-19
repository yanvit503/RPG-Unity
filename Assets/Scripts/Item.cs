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
    
    /// <summary>
    /// Remove quantidade do item, caso a quantidade a remover seja maior ou igual a zero, retorna a quantidade total e exclui o item
    /// </summary>
    /// <param name="qnt"></param>
    /// <returns></returns>
    public int Subtrai(int qnt)
    {
        var qntAnterior = Quantidade;
        var result = Quantidade - qnt;
        Quantidade -= qnt;

        if (result <= 0)
        {
            Destroy(gameObject);
            return qntAnterior;
        }

        return result;
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