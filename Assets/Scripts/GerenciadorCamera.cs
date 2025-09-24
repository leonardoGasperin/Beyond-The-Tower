using UnityEngine;

namespace btt.Core.Entidade {
    public class GerenciadorCamera : MonoBehaviour
    {

        public bool cameraPodeSubir = true;
        public float cameraVelocidade;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (cameraPodeSubir) {
                CameraSobe();
            }
        }

        private void CameraSobe(){
            transform.Translate(Vector3.up * cameraVelocidade * Time.deltaTime);
        }
    }
}
