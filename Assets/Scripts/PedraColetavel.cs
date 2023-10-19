using Assets.Scripts;
using UnityEngine;

public class PedraColetavel : MonoBehaviour, IDanificavel, IColetavel
{
    [SerializeField]
    int quantidadeDisponivel = 100;

    [SerializeField]
    Item retorno;
    
    [SerializeField]
    GameObject ParticulaDestruir;

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

        var itemAdd = Instantiate(retorno);

        itemAdd.AtualizaQuantidade(qntAdicionar);
        Inventario.Instance.AdicionarItem(itemAdd);
    }

    public void Destruir()
    {
        Instantiate(ParticulaDestruir,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
