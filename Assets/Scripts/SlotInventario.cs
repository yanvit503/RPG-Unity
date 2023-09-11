using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (Item != null)
        {
            ImageHolder.sprite = SpriteInicial;
            TextoQuantidade.text = string.Empty;
            Ocupado = false;
            Item = null;
        }
    }

    public void AtualizaQuantidade(Item item)
    {
        Item = item;
        TextoQuantidade.text = item.Quantidade.ToString();
    }
}