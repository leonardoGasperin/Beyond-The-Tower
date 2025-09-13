using UnityEngine;
using btt.Aplicacao.Contrato;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico {

    public class CombateServico : ICombateServico {
        public bool Atacando (int ataque, Inimigo inimigo, int pontosEnergia){
            if (inimigo == null) {
                return true;
            }
            inimigo.Dano(ataque);
            Debug.Log("Vida do inimigo: " + inimigo.pontosVida);
            return inimigo.estaVivo;
        }
    }
}