using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Arvore : MonoBehaviour, IDanificavel
{

    public int quantidadeRecurso;
    public ItemColetavel recurso;
    public BarraVida barraVida;

    private void Start()
    {
        barraVida.danificavel = this;
    }

    public void Dano(int quantidade)
    {
        barraVida.Dano(quantidade);
    }

    public void Destruir()
    {
        for (int i = 0; i < quantidadeRecurso; i++)
        {
            var pos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            var recursoCriado = Instantiate(recurso, pos, transform.rotation, null);
            recursoCriado.gameObject.SetActive(true);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Dano(50);
    }
}