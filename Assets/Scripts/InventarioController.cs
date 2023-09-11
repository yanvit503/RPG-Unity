using UnityEngine;

public class InventarioController : MonoBehaviour
{
    bool aberto;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!aberto)
                Inventario.Instance.gameObject.SetActive(true);
            else
                Inventario.Instance.gameObject.SetActive(false);
            aberto = !aberto;
        }
    }
}