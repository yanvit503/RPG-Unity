using System.Collections.Generic;
using UnityEngine;

public class InterruptorLuz : Interagivel
{
    [SerializeField]
    List<Light> luzes;

    bool _ligado = false;

    public override void Interagir()
    {
        if (!_ligado)
        {
            _ligado = true;

            foreach(var luz in luzes)
            {
                luz.intensity = 1;
            }            
        }
        else
        {
            _ligado = false;

            foreach (var luz in luzes)
            {
                luz.intensity = 0;
            }
        }            
    }
}