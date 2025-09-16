using UnityEngine;
using btt.Aplicacao.Contrato;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico {

    public class CombateServico : ICombateServico {
        public bool Atacando (int ataque, Personagem personagem, int pontosEnergia){
            if (personagem == null) {
                return true;
            }
            personagem.Dano(ataque);
            return personagem.estaVivo;
        }
    }
}