using btt.Aplicacao.DI.Inimigo;
using btt.Core.Enumeradores;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class MiniBoss : Inimigo
    {
        private Projetil lancaAtual;
        private float diferencaLanca;
        private float direcaoLanca;
        private bool atirouLanca;
        private bool colidindo = false;
        private bool podeAlcancarJogador;
        private EEstadoMiniBoss maquinaEstado;

        public float velocidadeSaidaColisao = 3f;
        
        #region Unity Methods
        protected override void Start()
        {
            base.Start();
            maquinaEstado = EEstadoMiniBoss.Idle;
            InimigoFachada = GetComponent<InimigoConfiguracaoDI>().Services;
            velocidade = 5f;
            maxHP = 100;
            ataque = 2;
            pontosVida = 100;
            atirouLanca = false;
            podeAndar = false;
        }

        // TODO: Refatorar em metodos especificos
        protected override void Update()
        {
            if(jogador.pontosVida == 0) return;
            
            base.Update();
            AtualizarEstado();
            ExecutarEstado();
            AtualizaAlvo();
            Orientacao();

        }

        private void OnTriggerEnter2D(Collider2D col)
        {

            if (col.gameObject.CompareTag("Projetil") && lancaAtual.chao)
            {
                Destroy(col.gameObject);
                lancaAtual = null;
                podeAndar = false;
                atirouLanca = false;
                podeAtirar = true;
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col)
        {
            base.OnCollisionEnter2D(col);
            colidindo = true;
            podeAndar = false;
        }

        protected override void OnCollisionExit2D(Collision2D col)
        {
            base.OnCollisionExit2D(col);
            colidindo = false;
            podeAndar = true;
        }

        #endregion

        #region Methods
        protected override void IntervaloAtaqueInimigo()
        {
            if (timerCooldown > 0) return;

            timerCooldown = ataqueCooldown;
            fachada.combateServico.Atacando(ataque, alvo);
        }

        private void Orientacao()
        {

            if (distancia > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void AtualizaAlvo()
        {
            if (colidindo)
                distancia = transform.position.x - jogador.transform.position.x;
            else if (lancaAtual)
                distancia = transform.position.x - lancaAtual.transform.position.x;
            else
                lancaAtual = null;
        }

        private void AndaAteJogador(){
            podeAndar = true;
            float direcao = Mathf.Sign(jogador.transform.position.x - transform.position.x);
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
        }

        private void AtualizarEstado()
        {
            podeAlcancarJogador = InimigoFachada.lancaServico.PodeAtirar(jogador, transform);

            if (!atirouLanca && podeAtirar && podeAlcancarJogador)
                maquinaEstado = EEstadoMiniBoss.PrepararAtaque;
            else if (atirouLanca && lancaAtual != null && lancaAtual.chao && podeAndar)
                maquinaEstado = EEstadoMiniBoss.SeguirLanca;
            else if (!podeAlcancarJogador && lancaAtual == null)
                maquinaEstado = EEstadoMiniBoss.SeguirJogador;
            else
                maquinaEstado = EEstadoMiniBoss.Idle;
        }

        private void ExecutarEstado()
        {
            _ = maquinaEstado switch
            {
                EEstadoMiniBoss.PrepararAtaque => ExecutarPrepararAtaque(),
                EEstadoMiniBoss.SeguirLanca => ExecutarSeguirLanca(),
                EEstadoMiniBoss.SeguirJogador => ExecutarSeguirJogador(),
                EEstadoMiniBoss.Idle => ExecutarIdle(),
                _ => null!,
            };
        }

        private object ExecutarPrepararAtaque()
        {
            podeAndar = true;
            podeAtirar = false;
            lancaAtual = InimigoFachada.lancaServico.AtiraLanca(jogador, projetil, transform);
            atirouLanca = true;
            return null!;
        }

        private object ExecutarSeguirLanca()
        {
            diferencaLanca = transform.position.x - lancaAtual.transform.position.x;
            direcaoLanca = Mathf.Sign(diferencaLanca);
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, -direcaoLanca);
            return null!;
        }

        private object ExecutarSeguirJogador()
        {
            AndaAteJogador();
            return null!;
        }

        private object ExecutarIdle() => null!;

        #endregion

    }
}
