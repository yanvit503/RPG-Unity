using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemColetavel : Item
{
    [SerializeField]
    AudioSource SomPegarItem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
            Inventario.Instance.AdicionarItem(this);
    }
}