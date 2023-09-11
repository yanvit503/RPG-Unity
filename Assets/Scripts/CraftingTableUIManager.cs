using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTableUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject SlotHolder;

    [SerializeField]
    SlotInventario SlotSaida;
    
    [SerializeField]
    List<ReceitaScriptableObject> Receitas;

    SlotCraftingTable[] _slots;

    public static CraftingTableUIManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _slots = GetSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    SlotCraftingTable[] GetSlots()
    {
        SlotCraftingTable[] slots = new SlotCraftingTable[9];

        for (int i = 0; i < SlotHolder.transform.childCount; i++)
        {
            var child = SlotHolder.transform.GetChild(i);

            if (child.GetComponent<SlotCraftingTable>() != null)
                slots[i] = child.GetComponent<SlotCraftingTable>();
        }

        return slots;
    }

    public void VerificaReceita()
    {
        bool receitaCompleta = true;
        Debug.Log("Verificando receitas... ");

        foreach (var receita in Receitas)
        {   
            for(int i  = 0; i < _slots.Length; i++)
            {
                if (receita.Ingredientes[i] != null)//receita tem item nesse slot
                    if (!_slots[i].Ocupado || _slots[i].Item.ItemSO.Nome != receita.Ingredientes[i].Nome)//o slot está vazio ou com o item errado
                        receitaCompleta = false;
            }

            if (receitaCompleta)
                //mostra o item que vai ser craftado
                Debug.Log(receita.Saida.Nome);
        }       
    }
}