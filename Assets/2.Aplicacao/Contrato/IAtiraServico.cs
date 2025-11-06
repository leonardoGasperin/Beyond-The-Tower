using UnityEngine;
using System.Collections;
using btt.Core.Entidade;

namespace btt.Aplicacao.Contratos {

    public interface IAtiraServico {
        public void Atira(Vector2 velocidadeTiro, Transform transform);
    }
}