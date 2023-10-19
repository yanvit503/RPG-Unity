using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftingManager : MonoBehaviour
{
    [SerializeField]
    List<ReceitaScriptableObject> Receitas;

    [SerializeField]
    ItemScriptableObj itemTeste;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FabricarItem(itemTeste);
        }
    }

    //um item fabricável não poderá haver mais de uma receita
    public bool VerificaReceita(ItemScriptableObj item)
    {
        bool receitaCompleta = false;
        Debug.Log("Verificando receita " + item.Nome);

        var receita = Receitas.Where(x => x.Saida.Equals(item)).First();

        foreach (var ingrediente in receita.Ingredientes)
        {
            var qntInventario = Inventario.Instance.GetItemBySO(ingrediente.Item)?.Sum(x => x.GetQuantidade());

            if (qntInventario.HasValue && qntInventario >= ingrediente.QuantidadeNecessaria)
                receitaCompleta = true;
        }

        if (receitaCompleta)
            //mostra o item que vai ser craftado
            Debug.Log(receita.Saida.Nome);

        return receitaCompleta;
    }

    public bool FabricarItem(ItemScriptableObj saida)
    {
        bool fabricou = false;

        if(VerificaReceita(saida))
        {
            //retira a quantidade usada na fabricação
            var receita = Receitas.Where(x => x.Saida.Equals(saida)).First();

            foreach (var ingrediente in receita.Ingredientes)
            {
                Inventario.Instance.RemoveQuantidade(ingrediente.Item,ingrediente.QuantidadeNecessaria);
            }

            //adiciona o item fabricado no inventario

            fabricou = true;
        }

        return fabricou;
    }
}