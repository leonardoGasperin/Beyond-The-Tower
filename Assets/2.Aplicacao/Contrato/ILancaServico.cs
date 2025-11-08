using UnityEngine;
using System.Collections;
using btt.Core.Entidade;

namespace btt.Aplicacao.Contratos {

    public interface ILancaServico {
        public void MovimentoLanca(Vector2 velocidadeTiro, Transform transform);
        public Projetil AtiraLanca(Jogador jogador, GameObject projetil, Transform transform);
    }
}