using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class NotificacaoInventarioManager : MonoBehaviour
    {
        public static NotificacaoInventarioManager Instancia;

        [SerializeField]
        GameObject notificacaoHolder;
        
        [SerializeField]
        GameObject notificacaoPegarPrefab;

        private GridLayoutGroup gridLayout;

        private void Start()
        {
            Instancia = this;
            gridLayout = notificacaoHolder.GetComponent<GridLayoutGroup>();
        }

        public void NotificacaoPegarItem(string nome, string quantidade)
        {
            var obj = Instantiate(notificacaoPegarPrefab,notificacaoHolder.transform);
            obj.GetComponent<NotificacaoInventario>().txtNome.text = nome;
            obj.GetComponent<NotificacaoInventario>().txtQuatidade.text = quantidade;
            AtualizaGrid();
        }

        public void AtualizaGrid()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayout.GetComponent<RectTransform>());
        }
    }
}