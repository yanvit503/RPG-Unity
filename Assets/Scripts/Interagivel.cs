using UnityEngine;

public abstract class Interagivel : MonoBehaviour
{
    public string Texto;

    public void BaseInteragir()
    {
        Interagir();
    }

    public virtual void Interagir() { }
}