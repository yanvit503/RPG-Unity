using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUIManager : MonoBehaviour
{
    public static InventarioUIManager Instance;

    [SerializeField]
    public GameObject PainelCraftingTable;

    public bool ArrastandoItem = false;
    public bool ArrastandoMetade = false;
    public SlotInventario SlotAnterior;
    public Item ItemArrastando;

    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirInventario(bool abrir)
    {

        Inventario.Instance.gameObject.SetActive(abrir);
    }

    public void AbrirCraftingTable(bool abrir)
    {
        AbrirInventario(abrir);
        PainelCraftingTable.gameObject.SetActive(abrir);
    }
}
