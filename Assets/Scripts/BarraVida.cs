using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public int QuantidadeVida;
    public int QuantidadeVidaMaxima;
    public Image Slider;
    public GameObject UIobj;
    public IDanificavel danificavel;

    private void Start()
    {
        gameObject.SetActive(false);
        QuantidadeVida = QuantidadeVidaMaxima;
        gameObject.SetActive(true);
    }

    public void Dano(int qnt)
    {
        QuantidadeVida -= qnt;

        if (QuantidadeVida <= 0)
            danificavel.Destruir();
    }

    public void Curar(int qnt)
    {
        if (qnt >= QuantidadeVidaMaxima)
            QuantidadeVida = qnt;

        QuantidadeVida += qnt;
    }

    void Update()
    {
        UIobj.transform.rotation = Quaternion.LookRotation(UIobj.transform.position - Camera.main.transform.position);

        var qnt = (float)QuantidadeVida / (float)QuantidadeVidaMaxima;
        Slider.fillAmount = Mathf.MoveTowards(Slider.fillAmount, qnt, 2 * Time.deltaTime);
    }
}