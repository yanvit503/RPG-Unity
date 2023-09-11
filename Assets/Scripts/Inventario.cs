using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField]
    GameObject SlotHolder;

    List<SlotInventario> Slots;
    [HideInInspector]
    public static Inventario Instance;

    [SerializeField]
    public Image imagemArrastando;

    void Start()
    {
        Instance = this;
        Slots = GetSlots();
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    List<SlotInventario> GetSlots()
    {
        List<SlotInventario> slots = new List<SlotInventario>();

        for (int i = 0; i < SlotHolder.transform.childCount; i++)
        {
            var child = SlotHolder.transform.GetChild(i);

            if (child.GetComponent<SlotInventario>() != null)
                slots.Add(child.GetComponent<SlotInventario>());
        }

        return slots;
    }

    public void AdicionarItem(Item item)
    {
        var slotsVazios = Slots.Where(x => x.Ocupado == false).ToList();

        if (item.ItemSO.Estacavel)
        {
            //procura se já tem no invetario
            var slotComItem = Slots.Where(x => x.Item != null && x.Item.ItemSO.Nome.Equals(item.ItemSO.Nome) && x.Item.Quantidade < x.QuantidadeMaxima).ToList();

            //ja tem
            foreach (var slot in slotComItem)
            {
                //adiciona quantidade no slot que ja tem o item
                AdicionaNoSlot(slot, item);

                return;
            }

            //nao tem
            //adiciona no slot vazio
            foreach (var slot in slotsVazios)
            {
                AdicionaNoSlot(slot, item);
                break;
            }

        }
        else
            foreach (var slot in slotsVazios)
            {
                AdicionaNoSlot(slot, item);
                break;
            }

    }

    public void AdicionaNoSlot(SlotInventario slot, Item item, bool mesmoItem = true)
    {
        slot.Ocupado = true;
        slot.ImageHolder.sprite = item.ItemSO.Icone;

        int qnt;
        if (item.ItemSO.Estacavel)
        {
            if (mesmoItem)
                qnt = slot.Item == null ? _ = item.Quantidade : _ = slot.Item.Quantidade += item.Quantidade;
            else
                qnt = item.Quantidade;

            slot.TextoQuantidade.text = qnt.ToString();
        }
        else
        {
            slot.TextoQuantidade.text = string.Empty;
            qnt = 1;
        }

        item.transform.parent = slot.transform;
        item.gameObject.SetActive(false);

        slot.ImageHolder.color = new Color(1, 1, 1, 1);

        slot.Item = item;
        slot.Item.Quantidade = qnt;
    }
}