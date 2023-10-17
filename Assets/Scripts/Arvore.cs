using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Arvore : MonoBehaviour, IDanificavel
{

    public int quantidadeDisponivel;
    public Item recurso;

    private void Start()
    {
    }

    public void Dano(int quantidade)
    {
        int qntAdicionar;
        if (quantidadeDisponivel < quantidade)
        {
            qntAdicionar = quantidadeDisponivel;
            Destruir();
        }
        else
        {
            quantidadeDisponivel -= quantidade;
            qntAdicionar = quantidade;
        }

        var itemAdd = Instantiate(recurso);

        itemAdd.AtualizaQuantidade(qntAdicionar);
        Inventario.Instance.AdicionarItem(itemAdd);
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Dano(12);
    }
}