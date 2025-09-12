using UnityEngine;
using btt.Aplicacao.Contrato;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico {

    public class CombateServico : ICombateServico {
        public void Atacando (int ataque, Collision2D col, int pontosEnergia){
            Inimigo inimigo = col.gameObject.GetComponent<Inimigo>();
            if (inimigo != null) {
                inimigo.pontosVida -= ataque;
                if (inimigo.pontosVida <= 0){
                    inimigo.pontosVida = 0;
                    pontosEnergia++;
                }
            }
            Debug.Log("Vida do inimigo: " + inimigo.pontosVida);
        }
    }
}