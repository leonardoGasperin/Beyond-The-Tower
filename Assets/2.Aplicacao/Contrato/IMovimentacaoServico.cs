using UnityEngine;

namespace btt.Aplicacao.Contratos {
    public interface IMovimentacaoServico{
        public void Movimentacao(Transform transform, float velocidade, float movimentoX);
        public void Pulo(Rigidbody2D rb, Transform transform, float forcaDoPulo);
    }
}