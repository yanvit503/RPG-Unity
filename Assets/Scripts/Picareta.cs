using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Picareta : MonoBehaviour, IArma
    {
        [SerializeField]
        ArmaScriptableObject ArmaSO;

        [SerializeField]
        AudioClip somBater;

        [SerializeField]
        GameObject ParticulaImpacto;

        [SerializeField]
        List<TipoColetavelEnum> TiposPermitidos;
        
        [SerializeField]
        float CameraShake;

        float cooldown = .5f;
        AudioSource AudioSource;
        Animator animator;

        public ArmaScriptableObject ScriptableObject { get { return ArmaSO; } set { ArmaSO = value; } }

        public bool Equipada { get; set; }
        GameObject IArma.gameObject { get { return gameObject; } set { } }

        private void Start()
        {
            animator = GetComponent<Animator>();
            AudioSource = GetComponent<AudioSource>();
        }

        public void Atirar()
        {
            var camPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

            RaycastHit hit;
            if (Physics.Raycast(camPos, Camera.main.transform.TransformDirection(Vector3.forward), out hit, ArmaSO.Alcance, LayerMask.GetMask("Coletavel")))
            {
                if (hit.collider != null)
                {
                    var danificavel = hit.collider.GetComponent<IDanificavel>();
                    var coletavel = hit.collider.GetComponent<IColetavel>();

                    if (danificavel != null && coletavel != null && TiposPermitidos.Any(x => x.Equals(coletavel.Tipo)))
                    {
                        danificavel.Dano(ArmaSO.QuantidadeDano);
                        AudioSource.PlayOneShot(somBater);

                        Camera.main.GetComponent<StressReceiver>().InduceStress(CameraShake);

                        Instantiate(ParticulaImpacto,hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal),hit.collider.transform);
                    }
                }
            }

            animator.SetTrigger("Bater");
            
            cooldown = .5f;
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