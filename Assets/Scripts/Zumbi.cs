using Assets.Scripts;
using UnityEngine;

public class Zumbi : MonoBehaviour, IDanificavel, IInimigo
{

    public BarraVida BarraVida;
    Animator animator;
    InimigoAI AI;

    void Start()
    {
        animator = GetComponent<Animator>();
        AI = GetComponent<InimigoAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F)))
            Atacar();

        animator.SetFloat("DistanciaPlayer",AI.DistanciaPlayer);
    }

    public void Dano(int quantidade)
    {
        throw new System.NotImplementedException();
    }

    public void Destruir()
    {
        throw new System.NotImplementedException();
    }

    public void Atacar()
    {
        animator.SetTrigger("Atacar");
        animator.SetBool("Atacando", true);
    }

    public void Perseguir()
    {
        animator.SetBool("Perseguindo", true);
        animator.SetTrigger("Perseguir");
    }

    public void PararAtacar()
    {
        animator.SetTrigger("PararAtacar");
        animator.SetBool("Atacando", false);
    }
}
