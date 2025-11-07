using UnityEngine;
using System.Collections;
using btt.Core.Entidade;

namespace btt.Aplicacao.Contrato {

    public interface ICameraServico {
        public void CameraSobe(Transform transform, float cameraVelocidade);
    }

}