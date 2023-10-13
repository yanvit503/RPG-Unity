using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public int QuantidadeVida;
    public int QuantidadeVidaMaxima;
    public bool UIWorldSpace;
    public Image Slider;
    public GameObject UIobj;
    public IDanificavel danificavel;

    private void Start()
    {
        gameObject.SetActive(false);
        QuantidadeVida = QuantidadeVidaMaxima;
        gameObject.SetActive(true);
        danificavel = GetComponent<IDanificavel>();
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

        ExibeBarra();
    }

    void ExibeBarra()
    {
        if(UIWorldSpace)
        {
            if (QuantidadeVida == QuantidadeVidaMaxima || QuantidadeVida <= 0 && UIWorldSpace)
            {
                UIobj.transform.GetChild(0).gameObject.SetActive(false);
                return;
            }

            var dist = (transform.position - Player.instance.transform.position).magnitude;
            UIobj.transform.GetChild(0).gameObject.SetActive(dist < 15 ? true : false);
        }
    }
}