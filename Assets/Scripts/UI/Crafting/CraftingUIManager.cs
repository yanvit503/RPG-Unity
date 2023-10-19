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

    [SerializeField]
    GameObject ReceitaSelecionadaPanel;

    [Header("Lista de Receitas")]
    [SerializeField]
    GameObject ContentHolder;

    [SerializeField]
    ReceitaBotaoUI ItemReceitaPrefab;

    void Start()
    {
        ReceitaSelecionadaPanel.SetActive(false);
        LimparListaIngredientes();
        SelecionarReceita(Receitas.First());
        MostraReceitas();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelecionarReceita(ReceitaScriptableObject receita)
    {
        if (receita != null)
        {
            LimparListaIngredientes();
            ReceitaSelecionadaPanel.SetActive(true);

            ReceitaSelecionada = receita;
            IconeItem.sprite = ReceitaSelecionada.Saida.Icone;
            TituloNomeItem.text = ReceitaSelecionada.Saida.Nome;

            for (int i = 0; i < ReceitaSelecionada.Ingredientes.Count(); i++)
            {
                if (ReceitaSelecionada.Ingredientes[i].Item != null)
                {
                    ImagensIngredientes[i].gameObject.SetActive(true);
                    ImagensIngredientes[i].sprite = ReceitaSelecionada.Ingredientes[i].Item.Icone;
                    TextosQuantidadeIngrediente[i].text = ReceitaSelecionada.Ingredientes[i].QuantidadeNecessaria.ToString();
                }
            }
        }
    }

    void LimparListaIngredientes()
    {
        int i = 0;

        ImagensIngredientes.ForEach(x =>
        {
            x.gameObject.SetActive(false);
            TextosQuantidadeIngrediente[i].text = string.Empty;
            i++;
        });
    }

    void MostraReceitas()
    {
        Receitas.ForEach(x =>
        {
            var btn = Instantiate(ItemReceitaPrefab, ContentHolder.transform);
            btn.GetComponent<ReceitaBotaoUI>().Receita = x;
            btn.GetComponent<ReceitaBotaoUI>().UICraftManager = this;
            btn.GetComponent<ReceitaBotaoUI>().AtualizaUI();
        });
    }
}