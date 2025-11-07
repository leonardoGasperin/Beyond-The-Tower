using UnityEngine;
using btt.Aplicacao.Contratos;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico {
    public class AtiraServico : IAtiraServico {

        public void Atira(Vector2 velocidadeTiro, Transform transform){
            transform.position += (Vector3)(velocidadeTiro * Time.deltaTime);
        }

    }
}