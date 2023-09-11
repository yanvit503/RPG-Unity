using UnityEngine;

public class Porta : Interagivel
{
    bool aberta = false;
    Animator animator;

    public override void Interagir()
    {
        aberta = !aberta;
        animator.SetBool("Aberta", aberta);

        if (aberta)
            Texto = "Fechar porta";
        else
            Texto = "Abrir porta";
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
}