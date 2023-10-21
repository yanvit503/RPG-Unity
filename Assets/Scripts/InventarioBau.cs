using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventarioBau : MonoBehaviour
{
    [SerializeField]
    GameObject SlotHolder;

    [SerializeField]
    AudioClip somPegarItem;

    List<SlotInventario> Slots;

    [SerializeField]
    public Image imagemArrastando;

    public static Bau BauAtual;

    private void Awake()
    {
    }

    void Start()
    {
        Cursor.visible = true;
    }

    public List<SlotInventario> CarregaSlots()
    {
        List<SlotInventario> slots = new List<SlotInventario>();

        for (int i = 0; i < SlotHolder.transform.childCount; i++)
        {
            var child = SlotHolder.transform.GetChild(i);

            if (child.GetComponent<SlotInventario>() != null)
                slots.Add(child.GetComponent<SlotInventario>());
        }

        foreach(var item in BauAtual.items)
        {
            slots[item.Key].Item = item.Value;
            slots[item.Key].AtualizaSlot();
        }

        Slots = slots;
        return slots;
    }

    //limpa o conteudo anterior e salva o novo
    public void SalvaItemsBau()
    {
        BauAtual.items = new Dictionary<int, Item>();
        int i = 0;

        Slots.ToList().ForEach(x => {

            if(x.Item != null)
            {
                x.gameObject.transform.parent = BauAtual.transform;// coloca o item dentro do transform do báu
                BauAtual.items.Add(i, x.Item);
            }

            i++;
        });
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

    public void AdicionaNoSlot(SlotInventario slot, Item item)
    {
        slot.Ocupado = true;
        slot.ImageHolder.sprite = item.ItemSO.Icone;

        int qnt = 0;
        if (item.ItemSO.Estacavel)
        {
            var qntTotal = QuantiadeTotalItem(item);
            if (qntTotal > 0 && slot.Item != null)            
                qnt = slot.Item.Soma(item.GetQuantidade());
            else
            {
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

        slot.AtualizaSlot();
    }

    public int QuantiadeTotalItem(Item item)
    {
        int retorno = 0;

        foreach (var slot in Slots.Where(x => x.Item != null && x.Item.ItemSO.Nome.Equals(item.ItemSO.Nome)))
        {
            retorno += slot.Item.GetQuantidade();
        }

        return retorno;
    }
}