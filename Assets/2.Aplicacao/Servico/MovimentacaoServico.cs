using btt.Aplicacao.Contrato;
using UnityEngine;

namespace btt.Aplicacao.Servico
{
    public class MovimentacaoServico : IMovimentacaoServico
    {
        public void MovimentacaoLinear(int velocidade, int direção, Rigidbody2D rb)
        {
            rb.linearVelocity = new Vector2(1 * velocidade * direção * Time.deltaTime, 0f);
        }

        public void Pulo(int forcaDoPulo, Rigidbody2D rb)
        {
            rb.AddForce(Vector2.up * forcaDoPulo * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}