using UnityEngine;
using Assets.Scripts;

public class PlayerInteragir : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    LayerMask msacara;
    PlayerUI playerUI;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        playerUI = GetComponentInChildren<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.SetMiraNormal();
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3, msacara))
        {
            if (hit.collider.GetComponent<Interagivel>() != null)
            {
                var interagivel = hit.collider.gameObject.GetComponent<Interagivel>();

                playerUI.AtualizaTexto(interagivel.Texto);
                Player.instance.PlayerUI.SetMiraInteragivel();

                if (Input.GetKeyDown(KeyCode.E))
                    interagivel.BaseInteragir();
            }
        }
        else
            playerUI.AtualizaTexto("");
    }
}