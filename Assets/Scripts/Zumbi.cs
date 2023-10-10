using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zumbi : MonoBehaviour, IDanificavel, IInimigo
{
    
    public BarraVida BarraVida;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F)))
            Atacar();
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
    }
}
