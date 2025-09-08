using UnityEngine;
using btt.Aplicacao.Contratos;

namespace btt.Aplicacao.Servico {
    public class MovimentacaoServico : IMovimentacaoServico {
        public void Movimentacao(Rigidbody2D rb, float velocidade, float movimentoX){
            rb.linearVelocity = new Vector2(movimentoX * velocidade, 0f);
        }

        public void Pulo(Rigidbody2D rb, Transform transform, float forcaDoPulo){
            rb.AddForce(transform.up * forcaDoPulo, ForceMode2D.Impulse);
        }
    }
}