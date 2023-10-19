using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CraftingManager
{
    //um item fabricável não poderá haver mais de uma receita
    //uma receita não poderá haver mais de 4 ingredientes
    public static bool VerificaReceita(ReceitaScriptableObject receita)
    {
        bool receitaCompleta = false;

        foreach (var ingrediente in receita.Ingredientes)
        {
            var qntInventario = Inventario.Instance.GetItemBySO(ingrediente.Item)?.Sum(x => x.GetQuantidade());

            if (qntInventario.HasValue && qntInventario >= ingrediente.QuantidadeNecessaria)
                receitaCompleta = true;
        }

        return receitaCompleta;
    }

    public static bool FabricarItem(ReceitaScriptableObject receita)
    {
        bool fabricou = false;

        if (VerificaReceita(receita))
        {
            //retira a quantidade usada na fabricação
            foreach (var ingrediente in receita.Ingredientes)
            {
                Inventario.Instance.RemoveQuantidade(ingrediente.Item, ingrediente.QuantidadeNecessaria);
            }

            //adiciona o item fabricado no inventario
            var itemCriado = Inventario.Instance.InstanciaItem(receita);
            itemCriado.AtualizaQuantidade(receita.QuantidadeSaida);

            Inventario.Instance.AdicionarItem(itemCriado);

            fabricou = true;
            Debug.Log("Fabricou " + receita.Saida.Nome);
        }

        Debug.Log("Sem itens suficientes");
        return fabricou;
    }
}