using UnityEngine;
using btt.Aplicacao.Contrato;

namespace btt.Aplicacao.Servico
{

    public class CameraServico : ICameraServico
    {

        public void CameraSobe(Transform transform, float cameraVelocidade)
        {
            transform.Translate(Vector3.up * cameraVelocidade * Time.deltaTime);
        }
    }
}