using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField]
    GameObject SlotHolder;

    [SerializeField]
    AudioClip somPegarItem;

    List<SlotInventario> Slots;
    [HideInInspector]
    public static Inventario Instance;

    [SerializeField]
    public Image imagemArrastando;

    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Slots = GetSlots();
        audioSource = Player.instance.GetComponent<AudioSource>();
        Cursor.visible = true;
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
            var slotComItem = Slots.Where(x => x.Item != null && x.Item.ItemSO.Nome.Equals(item.ItemSO.Nome) && x.Item.GetQuantidade() < x.QuantidadeMaxima).ToList();

            //ja tem
            foreach (var slot in slotComItem)
            {
                //adiciona quantidade no slot que ja tem o item
                AdicionaNoSlot(slot, item, "adiciona quantidade no slot que ja tem o item");

                return;
            }

            //nao tem
            //adiciona no slot vazio
            foreach (var slot in slotsVazios)
            {
                AdicionaNoSlot(slot, item, "adiciona no slot vazio");
                break;
            }

        }
        else
            foreach (var slot in slotsVazios)
            {
                AdicionaNoSlot(slot, item,"não estacavel");
                break;
            }

    }

    public void AdicionaNoSlot(SlotInventario slot, Item item,string chamada)
    {
        slot.Ocupado = true;
        slot.ImageHolder.sprite = item.ItemSO.Icone;

        string qntStr = "";

        int qnt = 0;
        if (item.ItemSO.Estacavel)
        {
            var qntTotal = QuantiadeTotalItem(item);
            if (qntTotal > 0 && slot.Item != null)
            {
                qnt = slot.Item.Soma(item.GetQuantidade());
                qntStr = qntTotal > 0 ? $"+{item.GetQuantidade()}({qntTotal + item.GetQuantidade()})" : $"+{item.GetQuantidade()}";
            }
            else
            {
                qntStr = $"+{item.GetQuantidade()}";
                slot.TextoQuantidade.text = qnt.ToString();
                qnt = item.GetQuantidade();
            }

            slot.TextoQuantidade.text = qnt.ToString();
        }
        else
        {
            slot.TextoQuantidade.text = string.Empty;
            qnt = 1;
        }

        item.transform.SetParent(slot.transform);
        item.gameObject.SetActive(false);

        slot.ImageHolder.color = new Color(1, 1, 1, 1);

        slot.Item = item;
        slot.Item.AtualizaQuantidade(qnt);

        audioSource.PlayOneShot(somPegarItem);

        NotificacaoInventarioManager.Instancia.NotificacaoPegarItem(item.ItemSO.Nome, qntStr);
    }

    int QuantiadeTotalItem(Item item)
    {
        int retorno = 0;

        foreach (var slot in Slots.Where(x => x.Item != null && x.Item.ItemSO.Nome.Equals(item.ItemSO.Nome)))
        {
            retorno += slot.Item.GetQuantidade();
        }

        return retorno;
    }
}