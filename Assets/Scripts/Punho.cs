using UnityEngine;

namespace Assets.Scripts
{
    public class Punho : MonoBehaviour, IArma
    {
        [SerializeField]
        ArmaScriptableObject ArmaSO;

        [SerializeField]
        AudioClip SomSoco;

        float cooldown = .5f;
        AudioSource AudioSource;

        public ArmaScriptableObject ScriptableObject { get { return ArmaSO; } set { ArmaSO = value; } }

        public bool Equipada { get; set; }
        GameObject IArma.gameObject { get { return gameObject; } set { } }

        public void Atirar()
        {
            var camPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

            RaycastHit hit;
            if (Physics.Raycast(camPos, Camera.main.transform.TransformDirection(Vector3.forward), out hit, ArmaSO.Alcance, LayerMask.GetMask("Inimigo")))
            {
                if (hit.collider != null && !hit.collider.isTrigger)
                {
                    if (hit.collider.GetComponent<IDanificavel>() != null)
                    {
                        hit.collider.GetComponent<IDanificavel>().Dano(ArmaSO.QuantidadeDano);
                    }
                }
            }

            cooldown = .5f;

            AudioSource.PlayOneShot(SomSoco);

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * ArmaSO.Alcance, Color.red, Mathf.Infinity);

            GetComponent<Animator>().SetTrigger("Soco");
        }

        void Awake()
        {
            ArmaController.Instancia.Armas.Add(this);
            ArmaController.Instancia.EquiparArma(this);
            AudioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            cooldown -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (cooldown <= 0)
                    Atirar();
            }
        }
    }
}