using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PedraColetavel : MonoBehaviour, IDanificavel,IColetavel
{
    int quantidadeDisponivel = 100;

    [SerializeField]
    Item retorno;

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
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
