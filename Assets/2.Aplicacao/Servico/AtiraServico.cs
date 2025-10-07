using UnityEngine;
using btt.Aplicacao.Contratos;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico {
    public class AtiraServico : IAtiraServico {

        public void Atira(Vector3 direcaoTiro, float velocidadeTiro, Transform transform){
            transform.Translate(direcaoTiro * velocidadeTiro * Time.deltaTime);
        }

    }
}