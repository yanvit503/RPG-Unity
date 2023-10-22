using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventarioBau))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Bau : Interagivel
{
    [HideInInspector]
    public InventarioBau InventarioBau;

    [SerializeField]
    GameObject BauUIPrefab;
        
    public GameObject BauUIHolder;
        
    [SerializeField]
    AudioClip somAbrir,somFechar;

    [HideInInspector]
    public Dictionary<int, Item> items = new Dictionary<int, Item>();

    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        InventarioBau = GetComponent<InventarioBau>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public override void Interagir()
    {
        InventarioBau.BauAtual = this;

        animator.SetTrigger("Abrir");
        audioSource.PlayOneShot(somAbrir);

        var ui = Instantiate(BauUIPrefab, BauUIHolder.transform);
        ui.SetActive(true);

        UIManager.Instancia.AbrirBau();
    }

    public void FecharBau()
    {
        audioSource.PlayOneShot(somFechar);
        animator.SetTrigger("Fechar");
    }
}