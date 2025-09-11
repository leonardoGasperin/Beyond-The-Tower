using UnityEngine;
using btt.Aplicacao.Contratos;

namespace btt.Aplicacao.Servico {
    public class MovimentacaoServico : IMovimentacaoServico {
        public void Movimentacao(Transform transform, float velocidade, float movimentoX){
            transform.Translate(Vector3.right * movimentoX * velocidade * Time.deltaTime);
        }

        public void Pulo(Rigidbody2D rb, Transform transform, float forcaDoPulo){
            rb.AddForce(transform.up * forcaDoPulo, ForceMode2D.Impulse);
        }
    }
}