using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.AI;

public class InimigoAI : MonoBehaviour
{
    NavMeshAgent agent;

    IInimigo inimigo;
    Player player;

    public float DistanciaAtaque;
    public float DistanciaPlayer = 0;
    public bool Atacando;
    public bool Perseguindo;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player.instance;
        inimigo = agent.gameObject.GetComponent<IInimigo>();
        DistanciaAtaque = agent.stoppingDistance;
    }

    private void Update()
    {
        if (Perseguindo)
        { 
            agent.SetDestination(player.transform.position);
            //OlharAlvo();
        }

        DistanciaPlayer = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(DistanciaPlayer);

        if (Perseguindo && DistanciaPlayer <= DistanciaAtaque)
            Atacar();
        else
        {
            if (Atacando)
            {
                Atacando = false;
                inimigo.PararAtacar();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Perseguindo = true;
            inimigo.Perseguir();
        }
    }

    void OlharAlvo()
    {
        Vector3 direcao = (transform.position - player.transform.position).normalized;
        Quaternion rotacao = Quaternion.LookRotation(new Vector3(direcao.x,0,direcao.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,rotacao,Time.deltaTime * 5);
    }

    void Atacar()
    {
        Atacando = true;
        inimigo.Atacar();
    }
}