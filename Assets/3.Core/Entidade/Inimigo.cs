using UnityEngine;
using btt.Aplicacao.DI.Personagem;

namespace btt.Core.Entidade {

    public class Inimigo : Personagem {
    
        protected override void Start(){
            base.Start();
            pontosVida = 10;
        }
    
    }

}