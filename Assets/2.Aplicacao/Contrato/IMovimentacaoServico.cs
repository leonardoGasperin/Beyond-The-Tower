using UnityEngine;

namespace btt.Aplicacao.Contrato
{
    public interface IMovimentacaoServico
    {
        public void MovimentacaoLinear(int velocidade, int direção, Rigidbody2D rb);
        void Pulo(int forcaDoPulo, Rigidbody2D rb);
    }
}