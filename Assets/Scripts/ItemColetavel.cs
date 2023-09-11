using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemColetavel : Item
{
    private void OnTriggerEnter(Collider other)
    {
        Inventario.Instance.AdicionarItem(this);
    }
}