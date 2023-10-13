using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class InimigoAI : MonoBehaviour
{
    NavMeshAgent agent;

    IInimigo inimigo;
    Player player;

    public float DistanciaAtaque;
    public float DistanciaPlayer = 0;
    public float IntervaloAtaque = 2;
    public bool Atacando;
    public bool Perseguindo;
    public bool Vivo = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player.instance;
        inimigo = agent.gameObject.GetComponent<IInimigo>();
        DistanciaAtaque = agent.stoppingDistance;
    }

    private void Update()
    {
        if (Vivo)
        {
            if (Perseguindo && !Atacando)
                agent.SetDestination(player.transform.position);

            if (DistanciaPlayer > 50 && Perseguindo)
            {
                PararPerseguir();
            }

            if (Perseguindo && DistanciaPlayer <= DistanciaAtaque)
                Atacar();
            else
                if (Atacando)
            {
                Atacando = false;
                inimigo.PararAtacar();
            }

            IntervaloAtaque -= Time.deltaTime; 
        }
    }

    public void PararPerseguir()
    {
        Perseguindo = false;
        inimigo.PararPerseguir();
        agent.SetDestination(gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !Perseguindo && Vivo)
        {            
            inimigo.Perseguir();
            CalculaDistancia();
            InvokeRepeating(nameof(CalculaDistancia), 0, 1);
            Perseguindo = true;
        }
    }

    void CalculaDistancia()
    {
        var dist = (transform.position - player.transform.position).magnitude;
        DistanciaPlayer = dist > 100 ? DistanciaAtaque + 1: dist;
    }

    void Atacar()
    {
        if(IntervaloAtaque <= 0)
        {
            Debug.Log("atacando");
            Atacando = true;
            inimigo.Atacar();
            IntervaloAtaque = 2;
        }
    }
}