using UnityEngine;
using btt.Aplicacao.Contratos;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico
{
    public class LancaServico : ILancaServico
    {

        public void MovimentoLanca(Vector2 velocidadeTiro, Transform transform)
        {
            transform.position += (Vector3)(velocidadeTiro * Time.deltaTime);
            float anguloRotacao = Mathf.Atan2(velocidadeTiro.y, velocidadeTiro.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(anguloRotacao, Vector3.forward);
        }

        public bool PodeAtirar(Jogador jogador, Transform transform)
        {
            Vector2 origemLanca = transform.position;
            Vector2 alvoLanca = jogador.transform.position;
            Vector2 diferenca = alvoLanca - origemLanca;

            float dx = Mathf.Abs(diferenca.x);
            float dy = diferenca.y;

            float velocidadeInicialLanca = 12f;
            float gravidade = 9.8f;

            float dentroRaizQuadrada = velocidadeInicialLanca * velocidadeInicialLanca * velocidadeInicialLanca * velocidadeInicialLanca - gravidade * (gravidade * dx * dx + 2 * dy * velocidadeInicialLanca * velocidadeInicialLanca);

            return dentroRaizQuadrada >= 0;
        }

        public Projetil AtiraLanca(Jogador jogador, GameObject projetil, Transform transform)
        {
            Vector2 origemLanca = transform.position;
            Vector2 alvoLanca = jogador.transform.position;
            Vector2 diferenca = alvoLanca - origemLanca;

            float direcaoLanca = Mathf.Sign(diferenca.x);
            float dx = Mathf.Abs(diferenca.x);
            float dy = diferenca.y;

            float velocidadeInicialLanca = 12f;
            float gravidade = 9.8f;

            float dentroRaizQuadrada = velocidadeInicialLanca * velocidadeInicialLanca * velocidadeInicialLanca * velocidadeInicialLanca - gravidade * (gravidade * dx * dx + 2 * dy * velocidadeInicialLanca * velocidadeInicialLanca);
            float raizQuadrada = Mathf.Sqrt(dentroRaizQuadrada);

            if (dentroRaizQuadrada < 0)
            {
                return null;
            }

            float angulo = Mathf.Atan((velocidadeInicialLanca * velocidadeInicialLanca - raizQuadrada) / (gravidade * dx));

            Vector2 velocidadeLanca = new Vector2(direcaoLanca * velocidadeInicialLanca * Mathf.Cos(angulo), velocidadeInicialLanca * Mathf.Sin(angulo));

            GameObject instanciar = Object.Instantiate(projetil, origemLanca, Quaternion.identity);
            Projetil lanca = instanciar.GetComponent<Projetil>();
            lanca.velocidadeTiro = velocidadeLanca;
            lanca.gravidade = -gravidade;
            return lanca;
        }

    }
}