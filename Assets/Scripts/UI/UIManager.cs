using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject InventarioPanel, CraftingPanel, BauPanel;

    bool craftingAberto, inventarioAberto, bauAberto;

    [HideInInspector]
    public static UIManager Instancia;

    void Start()
    {
        Instancia = this;

        AtualizaUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!bauAberto)
            {
                craftingAberto = craftingAberto ? false : true;
                inventarioAberto = inventarioAberto ? false : true;

                AtualizaUI(); 
            }
            else
            {
                FechaBau();
            }
        }

        if(bauAberto)
            BauPanel.transform.GetChild(0).gameObject.SetActive(true);

    }

    public void AbrirBau()
    {
        if (bauAberto)
            return;

        inventarioAberto = true;
        craftingAberto = false;
        bauAberto = true;

        AtualizaUI();
        BauPanel.transform.GetChild(0).GetComponent<InventarioBau>().CarregaSlots(InventarioBau.BauAtual);        
    }
    
    public void FechaBau()
    {
        if (!bauAberto)
            return;

        inventarioAberto = false;
        craftingAberto = false;
        bauAberto = false;

        BauPanel.transform.GetChild(0).GetComponent<InventarioBau>().SalvaItemsBau();
        InventarioBau.BauAtual.FecharBau();

        Destroy(BauPanel.transform.GetChild(0).gameObject);

        AtualizaUI();
    }

    private void AtualizaUI()
    {
        InventarioPanel.SetActive(inventarioAberto);
        CraftingPanel.SetActive(craftingAberto);
        BauPanel.SetActive(bauAberto);
    }
}