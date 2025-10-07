using UnityEngine;
using System.Collections;
using btt.Core.Entidade;

namespace btt.Aplicacao.Contratos {

    public interface IAtiraServico {
        public void Atira(Vector3 direcaoTiro, float velocidadeTiro, Transform transform);
    }

}