using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotCraftingTable : SlotInventario
{
    Image imagemArrastando;

    public void CliqueSlot(SlotCraftingTable slot)
    {
        //imagemArrastando = Inventario.Instance.imagemArrastando;

        //if (InventarioUIManager.Instance.ArrastandoItem)
        //{
        //    //verifica se o item � igual
        //    if (slot.Item != null && slot.Item.ItemSO.Nome.Equals(InventarioUIManager.Instance.ItemArrastando.ItemSO.Nome))
        //    {
        //        if (!InventarioUIManager.Instance.ArrastandoMetade)
        //            InventarioUIManager.Instance.SlotAnterior.RemoveItem();
        //        InventarioUIManager.Instance.ArrastandoItem = false;
        //        Inventario.Instance.AdicionaNoSlot(slot, InventarioUIManager.Instance.ItemArrastando);
        //        imagemArrastando.gameObject.SetActive(InventarioUIManager.Instance.ArrastandoItem);
        //        InventarioUIManager.Instance.SlotAnterior.ImageHolder.color = new Color(1, 1, 1, 1);

        //    }
        //    else if (slot.Item != null && !slot.Item.ItemSO.Nome.Equals(InventarioUIManager.Instance.ItemArrastando.ItemSO.Nome))
        //    {
        //        //troca item
        //        var itemAnterior = slot.Item;
        //        if (!InventarioUIManager.Instance.ArrastandoMetade)
        //            InventarioUIManager.Instance.SlotAnterior.RemoveItem();

        //        Inventario.Instance.AdicionaNoSlot(slot, InventarioUIManager.Instance.ItemArrastando);

        //        InventarioUIManager.Instance.ArrastandoItem = true;
        //        InventarioUIManager.Instance.ItemArrastando = itemAnterior;
        //        imagemArrastando.sprite = itemAnterior.ItemSO.Icone;
        //        imagemArrastando.gameObject.SetActive(InventarioUIManager.Instance.ArrastandoItem);
        //        InventarioUIManager.Instance.SlotAnterior.ImageHolder.color = new Color(1, 1, 1, 1);
        //    }
        //    else
        //    {
        //        if (!InventarioUIManager.Instance.ArrastandoMetade)
        //            InventarioUIManager.Instance.SlotAnterior.RemoveItem();
        //        InventarioUIManager.Instance.ArrastandoItem = false;
        //        Inventario.Instance.AdicionaNoSlot(slot, InventarioUIManager.Instance.ItemArrastando);
        //        imagemArrastando.gameObject.SetActive(InventarioUIManager.Instance.ArrastandoItem);
        //        InventarioUIManager.Instance.SlotAnterior.ImageHolder.color = new Color(1, 1, 1, 1);
        //    }
        //    CraftingTableUIManager.Instance.VerificaReceita();
        //}
        //else
        //{
        //    if (slot.Item != null)
        //    {
        //        if (Input.GetKey(KeyCode.LeftShift))
        //        {
        //            ComecaArrastarMetade(slot);
        //        }
        //        else
        //            ComecaArrastarItem(slot);
        //    }
        //}



    }

    private void ComecaArrastarItem(SlotCraftingTable slot)
    {
        slot.ImageHolder.color = new Color(1, 1, 1, 0.2f);
        InventarioUIManager.Instance.ItemArrastando = slot.Item;
        InventarioUIManager.Instance.ArrastandoItem = true;
        InventarioUIManager.Instance.SlotAnterior = slot;
        imagemArrastando.gameObject.SetActive(InventarioUIManager.Instance.ArrastandoItem);
        imagemArrastando.sprite = slot.ImageHolder.sprite;
        InventarioUIManager.Instance.ArrastandoMetade = false;
    }

    private void ComecaArrastarMetade(SlotCraftingTable slot)
    {
        if (slot.Item.GetQuantidade() > 1)
        {
            slot.ImageHolder.color = new Color(1, 1, 1, 0.2f);

            var itemDividido = Instantiate(slot.Item);
            var divisao = slot.Item.GetQuantidade() / 2;
            var resto = slot.Item.GetQuantidade() % 2;

            itemDividido.AtualizaQuantidade(divisao + resto);
            slot.Item.AtualizaQuantidade(slot.Item.GetQuantidade() / 2);

            InventarioUIManager.Instance.ItemArrastando = itemDividido;
            InventarioUIManager.Instance.ArrastandoItem = true;
            InventarioUIManager.Instance.SlotAnterior = slot;
            imagemArrastando.gameObject.SetActive(InventarioUIManager.Instance.ArrastandoItem);
            imagemArrastando.sprite = slot.ImageHolder.sprite;
            InventarioUIManager.Instance.ArrastandoMetade = true;

            slot.AtualizaQuantidade(slot.Item);
        }
        else
            ComecaArrastarItem(slot);
    }

    public void CliqueInventario()
    {
        //if (InventarioUIManager.Instance.ArrastandoItem)
        //{
        //    InventarioUIManager.Instance.SlotAnterior.RemoveItem();
        //    InventarioUIManager.Instance.ArrastandoItem = false;
        //    Inventario.Instance.AdicionaNoSlot(InventarioUIManager.Instance.SlotAnterior, InventarioUIManager.Instance.ItemArrastando);
        //    imagemArrastando.gameObject.SetActive(InventarioUIManager.Instance.ArrastandoItem);
        //}
    }

    void Update()
    {
        if (InventarioUIManager.Instance.ArrastandoItem && imagemArrastando != null)
            imagemArrastando.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
}