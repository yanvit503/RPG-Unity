using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaCrafting : Interagivel
{
    bool usando = false;

    private void Start()
    {
    }

    public override void Interagir()
    {
        usando = !usando;
        InventarioUIManager.Instance.AbrirCraftingTable(usando);
    }
}