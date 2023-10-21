using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventario : MonoBehaviour
{
    [SerializeField]
    public Image ImageHolder;
    [SerializeField]
    public TMP_Text TextoQuantidade;
    public Item Item;
    public bool Ocupado;
    public int QuantidadeMaxima;

    [SerializeField]
    public Sprite SpriteInicial;

    void Start()
    {
        SpriteInicial = ImageHolder.sprite;
    }

    public void RemoveItem()
    {
        ImageHolder.sprite = SpriteInicial;
        TextoQuantidade.text = string.Empty;
        Ocupado = false;
        Item = null;
    }

    public void AtualizaSlot()
    {
        if (Item != null)
        {
            ImageHolder.sprite = Item.ItemSO.Icone;
            TextoQuantidade.text = Item.GetQuantidade().ToString();
        }
        else
            RemoveItem();
    }

    public void AtualizaQuantidade(Item item)
    {
        Item = item;
        TextoQuantidade.text = item.GetQuantidade().ToString();
    }
}