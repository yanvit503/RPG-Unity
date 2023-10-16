using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificacaoInventario : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI txtNome, txtQuatidade;
    
    [SerializeField]
    float Velocidade;

    CanvasGroup canvasGroup;

    float tempo = 0;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        tempo += Time.deltaTime;
        if (tempo > 1)
            FadeOut();

        if (canvasGroup.alpha <= 0)
            RemoveNotificacao();
    }

    private void FadeOut()
    {
        canvasGroup.alpha = (canvasGroup.alpha - .5f * Time.deltaTime);
    }
    
    private void RemoveNotificacao()
    {
        Destroy(gameObject);
        NotificacaoInventarioManager.Instancia.AtualizaGrid();
    }
}