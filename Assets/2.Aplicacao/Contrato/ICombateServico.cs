using UnityEngine;
using System.Collections;
using btt.Core.Entidade;

namespace btt.Aplicacao.Contrato {

    public interface ICombateServico {
        public bool Atacando(int ataque, Personagem personagem, int pontosEnergia);
    }

}