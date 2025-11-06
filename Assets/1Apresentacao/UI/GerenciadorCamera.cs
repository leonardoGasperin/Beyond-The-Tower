using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {
    public class GerenciadorCamera : MonoBehaviour
    {

        public PersonagemConfiguracaoDI.ServiceLocator fachada;
        public bool cameraPodeSubir = true;
        public float cameraVelocidade;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            fachada = GetComponent<PersonagemConfiguracaoDI>().Services;
        }

        // Update is called once per frame
        void Update()
        {
            if (cameraPodeSubir) {
                fachada.cameraServico.CameraSobe(transform, cameraVelocidade);
            }
            if(transform.position.y >= 41) {
                cameraPodeSubir = false;
            }
        }
    }
}
