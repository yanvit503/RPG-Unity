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

    AudioSource audioSource;

    public static Bau BauAtual;

    private void Awake()
    {
    }

    void Start()
    {
        audioSource = Player.instance.GetComponent<AudioSource>();
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

    public void AdicionaNoSlot(SlotInventario slot, Item item, bool tocaSom = false,bool notifica = false)
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

        if(tocaSom) audioSource.PlayOneShot(somPegarItem);

        if(notifica) NotificacaoInventarioManager.Instancia.NotificacaoPegarItem(item.ItemSO.Nome, qntStr);

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

    public List<Item> GetItem(Item item)
    {
        try
        {
            return Slots.Where(x => x.Item != null && x.Item.Equals(item)).Select(x => x.Item).ToList();
        }
        catch
        {
            return new List<Item>();
        }
    }

    public List<Item> GetItemBySO(ItemScriptableObj item)
    {
        List<Item> items = new List<Item>();

        try
        {
            foreach (Item i in Slots.Where(x => x.Item != null && x.Item.ItemSO.Nome.Equals(item.Nome)).Select(x => x.Item))
            {
                items.Add(i);
            }

            return items;
        }
        catch
        {
            return items;
        }
    }

    public int RemoveQuantidade(ItemScriptableObj item, int quantidade)
    {
        int quantidadeRemovida = 0;

        try
        {
            foreach (SlotInventario slot in Slots.Where(x => x.Item != null && x.Item.ItemSO.Nome.Equals(item.Nome)))
            {
                Item i = slot.Item;

                if (quantidadeRemovida < quantidade)
                {
                    var qntItem = i.GetQuantidade();
                    var qntRemovidaAgora = i.Subtrai(quantidade);
                    
                    quantidadeRemovida += qntRemovidaAgora;

                    if (qntRemovidaAgora >= qntItem)
                        slot.Item = null;
                }

                slot.AtualizaSlot();
            }

            return quantidadeRemovida;
        }
        catch
        {
            return quantidadeRemovida;
        }
    }

    public Item InstanciaItem(ReceitaScriptableObject receita)
    {
        // Crie um novo objeto vazio
        GameObject novoObjeto = new GameObject(receita.Saida.Nome);

        novoObjeto.AddComponent<Item>().ItemSO = receita.Saida;
        return Instantiate(novoObjeto).GetComponent<Item>();
    }
}