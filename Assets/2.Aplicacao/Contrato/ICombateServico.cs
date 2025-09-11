using UnityEngine;
using System.Collections;

namespace btt.Aplicacao.Contrato {

    public interface ICombateServico {
        public void Atacando(int ataque, Collision2D col, int pontosEnergia);
    }

}