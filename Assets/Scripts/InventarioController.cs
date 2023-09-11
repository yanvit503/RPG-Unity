using UnityEngine;

public class InventarioController : MonoBehaviour
{
    bool aberto = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventarioUIManager.Instance.AbrirInventario(aberto);
            aberto = !aberto;
        }
    }
}