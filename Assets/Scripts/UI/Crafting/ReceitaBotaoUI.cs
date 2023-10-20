using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceitaBotaoUI : MonoBehaviour
{
    [HideInInspector]
    public ReceitaScriptableObject Receita;

    [HideInInspector]
    public CraftingUIManager UICraftManager;

    Image icone;
    TextMeshProUGUI texto;

    void Awake()
    {
        icone = transform.GetChild(0).GetComponentInChildren<Image>();
        texto = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        //deseleciona os outros botões
        var outrosBtn = transform.parent.GetComponentsInChildren<ReceitaBotaoUI>().Where(x => !x.Receita.Equals(Receita)).ToList();

        outrosBtn.ForEach(x =>
        {
            x.GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 0);//define sprite de fundo do botão
        });

        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 255);//define sprite de fundo do botão

        UICraftManager.SelecionarReceita(Receita);
    }

    public void AtualizaUI()
    {
        icone.sprite = Receita.Saida.Icone;
        texto.text = Receita.Saida.Nome;
    }
}