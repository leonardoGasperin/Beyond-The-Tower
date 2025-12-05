using UnityEngine;

namespace btt.Core.Entidade
{
    // TODO: Adequar para que a Entidade MiniBoss seja genérica e reutilizável para todos os tipos de inimigos MiniBoss
    public class MiniBoss : Inimigo
    {
        private bool atirouLanca;
        private float diferencaLanca;
        private float direcaoLanca;
        private Projetil lancaAtual;
        public float velocidadeSaidaColisao = 3f;
        private bool colidindo = false;
        bool podeAlcancarJogador;

        protected override void Start()
        {
            base.Start();
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
            base.Update();

            hpVerde.fillAmount = (float)pontosVida/maxHP;

            podeAlcancarJogador = fachada.lancaServico.PodeAtirar(jogador, transform);

            if(jogador.pontosVida <= 0) return;
            
            if (colidindo) {
                distancia = transform.position.x - jogador.transform.position.x;
            }
            else if (lancaAtual) {
                distancia = transform.position.x - lancaAtual.transform.position.x;
            } else {
                lancaAtual = null;
            }
            
            if(distancia > 0) {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (pontosVida <= 0) {
                return;
            }

            if (jogador.pontosVida <= 0) {
                return;
            }
            
            if(podeAtacar && alvo != null && alvo.pontosVida > 0) IntervaloAtaqueInimigo();

            if(colidindo) return;
            
            if(atirouLanca == false && podeAtirar && podeAlcancarJogador){
                podeAndar = true;
                podeAtirar = false;
                lancaAtual = fachada.lancaServico.AtiraLanca(jogador, projetil, transform);
                atirouLanca = true;
            }

            if(atirouLanca && lancaAtual != null && lancaAtual.chao && podeAndar) {
                diferencaLanca = transform.position.x - lancaAtual.transform.position.x;
                direcaoLanca = Mathf.Sign(diferencaLanca);
                fachada.movimentacaoServico.Movimentacao(transform, velocidade, -direcaoLanca);
            }

            if (!podeAlcancarJogador && lancaAtual == null) {
                AndaAteJogador();
            }
        }

        private void AndaAteJogador(){
            podeAndar = true;
            float direcao = Mathf.Sign(jogador.transform.position.x - transform.position.x);
            fachada.movimentacaoServico.Movimentacao(transform, velocidade, direcao);
            if(direcao > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            } else {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D col){
            
            if(col.gameObject.CompareTag("Projetil") && lancaAtual.chao){
                Destroy(col.gameObject);
                lancaAtual = null;
                podeAndar = false;
                atirouLanca = false;
                podeAtirar = true;
            }
        }

        ///Inimigos
        private void IntervaloAtaqueInimigo(){
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0) {
                timerCooldown = ataqueCooldown;
                fachada.combateServico.Atacando(ataque, alvo);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D col){
            base.OnCollisionEnter2D(col);
            colidindo = true;
            podeAndar = false;
        }

        protected override void OnCollisionExit2D(Collision2D col){
            base.OnCollisionExit2D(col);
            colidindo = false;
            podeAndar = true;
        }
    }
}
