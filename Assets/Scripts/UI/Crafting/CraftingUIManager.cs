using System;
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

    [Header("Botão Fabricar")]
    [SerializeField]
    TextMeshProUGUI TextoBotaoFabricar;

    [SerializeField]
    Image BtnFabricar;

    bool fabricando;
    float tempoFabricando = 0.0f;

    void Start()
    {
        ReceitaSelecionadaPanel.SetActive(false);
        LimparListaIngredientes();
        SelecionarReceita(Receitas.First());
        MostraReceitas();
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

    public void FabricarReceitaSelecionada()
    {
        if(CraftingManager.VerificaReceita(ReceitaSelecionada) && !fabricando)
        {
            fabricando = true;
            tempoFabricando = 0;
            BtnFabricar.fillAmount = 0;
        }
    }

    private void Update()
    {
        if(fabricando)
        {
            tempoFabricando += Time.deltaTime;
            BtnFabricar.fillAmount = tempoFabricando / ReceitaSelecionada.TempoFabricacao;

            var tempoRestante = Convert.ToInt32(ReceitaSelecionada.TempoFabricacao - tempoFabricando) + 1;
            TextoBotaoFabricar.text = $"Fabricando ({tempoRestante})";

            if (tempoFabricando >= ReceitaSelecionada.TempoFabricacao)
            {
                CraftingManager.FabricarItem(ReceitaSelecionada);
                fabricando = false;
                tempoFabricando = 0;
                BtnFabricar.fillAmount = 1;
                TextoBotaoFabricar.text = "Fabricar";
            }
        }
    }
}