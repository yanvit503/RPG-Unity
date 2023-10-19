using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIManager : MonoBehaviour
{
    ReceitaScriptableObject ReceitaSelecionada;

    [SerializeField]
    List<ReceitaScriptableObject> Receitas;
    
    [SerializeField]
    List<Image> ImagensIngredientes;

    [SerializeField]
    List<TextMeshProUGUI> TextosQuantidadeIngrediente;

    [SerializeField]
    TextMeshProUGUI TituloNomeItem;

    [SerializeField]
    Image IconeItem;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelecionarReceita(ReceitaScriptableObject receita)
    {
        ReceitaSelecionada = receita;
        IconeItem.sprite = ReceitaSelecionada.Saida.Icone;
        TituloNomeItem.text = ReceitaSelecionada.Saida.Nome;

        for(int i = 0; i < ReceitaSelecionada.Ingredientes.Count(); i++)
        {
            ImagensIngredientes[i].sprite = ReceitaSelecionada.Ingredientes[i].Item.Icone;
            TextosQuantidadeIngrediente[i].text = ReceitaSelecionada.Ingredientes[i].QuantidadeNecessaria.ToString();
        }
    }

    void LimparListaIngredientes()
    {

    }

}
