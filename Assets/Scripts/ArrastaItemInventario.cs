using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArrastaItemInventario : MonoBehaviour
{
    bool arrastandoItem = false;
    bool arrastandoMetade = false;
    Item itemArrasatado;
    Image imagemArrastando;
    SlotInventario _slotAnterior;

    public void CliqueSlot(SlotInventario slot)
    {
        imagemArrastando = Inventario.Instance.imagemArrastando;

        if (arrastandoItem)
        {
            //verifica se o item é igual
            if(slot.Item != null && slot.Item.ItemSO.Nome.Equals(itemArrasatado.ItemSO.Nome))
            {
                if (!arrastandoMetade)
                    _slotAnterior.RemoveItem();
                arrastandoItem = false;
                Inventario.Instance.AdicionaNoSlot(slot, itemArrasatado);
                imagemArrastando.gameObject.SetActive(arrastandoItem);
                _slotAnterior.ImageHolder.color = new Color(1, 1, 1, 1);

            }
            else if (slot.Item != null && !slot.Item.ItemSO.Nome.Equals(itemArrasatado.ItemSO.Nome))
            {
                //troca item
                var itemAnterior = slot.Item;
                if(!arrastandoMetade)
                    _slotAnterior.RemoveItem();
                //_slotAnterior.ImageHolder.sprite = _slotAnterior.SpriteInicial;

                Inventario.Instance.AdicionaNoSlot(slot, itemArrasatado,false);

                arrastandoItem = true;
                itemArrasatado = itemAnterior;
                imagemArrastando.sprite = itemAnterior.ItemSO.Icone;
                imagemArrastando.gameObject.SetActive(arrastandoItem);
                _slotAnterior.ImageHolder.color = new Color(1, 1, 1, 1);
            }
            else
            {
                if (!arrastandoMetade)
                    _slotAnterior.RemoveItem();
                arrastandoItem = false;
                Inventario.Instance.AdicionaNoSlot(slot, itemArrasatado);
                imagemArrastando.gameObject.SetActive(arrastandoItem);
                _slotAnterior.ImageHolder.color = new Color(1, 1, 1, 1);
            }

        }
        else
        {
            if (slot.Item != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ComecaArrastarMetade(slot);
                }
                else
                    ComecaArrastarItem(slot);
            }
        }

        Debug.Log(_slotAnterior.gameObject);

    }

    private void ComecaArrastarItem(SlotInventario slot)
    {
        slot.ImageHolder.color = new Color(1, 1, 1, 0.2f);
        itemArrasatado = slot.Item;
        arrastandoItem = true;
        _slotAnterior = slot;
        imagemArrastando.gameObject.SetActive(arrastandoItem);
        imagemArrastando.sprite = slot.ImageHolder.sprite;
        arrastandoMetade = false;
    }

    private void ComecaArrastarMetade(SlotInventario slot)
    {
        if (slot.Item.Quantidade > 1)
        {
            slot.ImageHolder.color = new Color(1, 1, 1, 0.2f);

            var itemDividido = Instantiate(slot.Item);
            var divisao = slot.Item.Quantidade / 2;
            var resto  = slot.Item.Quantidade % 2;

            itemDividido.Quantidade = divisao + resto;
            slot.Item.Quantidade = slot.Item.Quantidade / 2;

            itemArrasatado = itemDividido;
            arrastandoItem = true;
            _slotAnterior = slot;
            imagemArrastando.gameObject.SetActive(arrastandoItem);
            imagemArrastando.sprite = slot.ImageHolder.sprite;
            arrastandoMetade = true;

            slot.AtualizaQuantidade(slot.Item);
        }
        else
            ComecaArrastarItem(slot);
    }

    public void CliqueInventario()
    {
        if (arrastandoItem)
        {
            _slotAnterior.RemoveItem();
            arrastandoItem = false;
            Inventario.Instance.AdicionaNoSlot(_slotAnterior, itemArrasatado);
            imagemArrastando.gameObject.SetActive(arrastandoItem);
        }
    }

    void Update()
    {
        if (arrastandoItem)
            imagemArrastando.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
}