using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceitaBotaoUI : MonoBehaviour
{
    public ReceitaScriptableObject Receita;
    public CraftingUIManager UICraftManager;

    Image icone;
    TextMeshProUGUI texto;

    void Awake()
    {
        icone = transform.GetChild(0).GetComponentInChildren<Image>();
        texto = transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();

        icone.sprite = Receita.Saida.Icone;
        texto.text = Receita.Saida.Nome;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        UICraftManager.SelecionarReceita(Receita);
    }
}