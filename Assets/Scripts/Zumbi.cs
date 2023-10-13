using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Zumbi : MonoBehaviour, IDanificavel, IInimigo
{

    [SerializeField]
    int QuantidadeDano;

    public BarraVida BarraVida;
    Animator animator;
    InimigoAI AI;
    AudioSource audioSource;
    Player player;

    [Header("Som")]
    [SerializeField]
    AudioClip somAgrar;
    [SerializeField]
    AudioClip somAtacar;

    [SerializeField]
    List<AudioClip> somPerseguindo;
    Queue<AudioClip> clipQueue = new Queue<AudioClip>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        AI = GetComponent<InimigoAI>();
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("DistanciaPlayer",AI.DistanciaPlayer);

        if (audioSource.isPlaying == false && clipQueue.Count > 0 && AI.Vivo)
        {
            audioSource.clip = clipQueue.Dequeue();
            audioSource.Play();
            clipQueue.Enqueue(audioSource.clip);
        }
    }

    public void Dano(int quantidade)
    {
        BarraVida.Dano(quantidade);
    }

    public void Destruir()
    {
        if (AI.Vivo)
        {
            audioSource.Stop();
            AI.PararPerseguir();
            AI.Vivo = false;
            animator.SetTrigger("Morrer"); 
        }
    }

    public void Atacar()
    {
        audioSource.PlayOneShot(somAtacar);
        animator.SetTrigger("Atacar");
        animator.SetBool("Atacando", true);

        player.gameObject.GetComponent<IDanificavel>().Dano(QuantidadeDano);
    }

    public void Perseguir()
    {
        if(!AI.Perseguindo)
            audioSource.PlayOneShot(somAgrar);

        animator.SetBool("Perseguindo", true);
        animator.SetTrigger("Perseguir");

        foreach(var clip in somPerseguindo)
        {
            clipQueue.Enqueue(clip);
        }
    }

    public void PararAtacar()
    {
        animator.SetTrigger("PararAtacar");
        animator.SetBool("Atacando", false);
    }

    public void PararPerseguir()
    {
        animator.SetBool("Perseguindo", false);
    }
}