using Assets.Scripts;
using UnityEngine;

public class Arvore : MonoBehaviour, IDanificavel, IColetavel
{
    [SerializeField]
    int quantidadeDisponivel = 100;

    [SerializeField]
    Item recurso;

    [SerializeField]
    TipoColetavelEnum Tipo;

    TipoColetavelEnum IColetavel.Tipo { get { return Tipo; } set { Tipo = value; } }

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
}