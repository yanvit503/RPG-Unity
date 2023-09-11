using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI texto;
    [SerializeField]
    Image mira;
    [SerializeField]
    Sprite interagivelSprite;
    Sprite miraNormal;

    private void Start()
    {
        miraNormal = mira.sprite;
    }

    public void AtualizaTexto(string txt)
    {
        texto.text = txt;
    }

    public string GetTexto()
    {
        return texto.text;
    }

    public void SetMiraInteragivel()
    {
        mira.rectTransform.localScale = new Vector3(5, 5, 1);
        mira.sprite = interagivelSprite;
    }
    
    public void SetMiraNormal()
    {
        mira.rectTransform.localScale = new Vector3(1,1,1);
        mira.sprite = miraNormal;
    }
}