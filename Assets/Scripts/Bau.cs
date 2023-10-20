using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventarioBau))]
public class Bau : Interagivel
{
    [HideInInspector]
    public InventarioBau InventarioBau;

    [SerializeField]
    GameObject BauUIPrefab;
    
    [SerializeField]
    GameObject BauUIHolder;
 
    void Start()
    {
        InventarioBau = GetComponent<InventarioBau>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interagir()
    {
        InventarioBau.BauAtual = this;

        var ui = Instantiate(BauUIPrefab,BauUIHolder.transform);
        ui.SetActive(true);

        UIManager.Instancia.AbrirBau();
    }
}
