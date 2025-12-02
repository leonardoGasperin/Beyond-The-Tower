using UnityEngine;
using UnityEngine.InputSystem;

namespace btt.Core.Entidade {

    public class Habilidades : MonoBehaviour
    {

        private Jogador jogador;

        public bool ativaDrenar;
        private float resetaCooldownDrena;
        private Vector2 direcaoRaycast;
        public float cooldownDrenar = 60f;
        private int quantidadeDrenar;
        private bool estaNoCooldownDrenar = false;

        public bool ativaConcentracao;
        public float cooldownConcentracao = 120f;
        private float resetaCooldownConcentracao;
        private bool estaNoCooldownConcentracao = false;

        public bool ativaEspelho;
        private float resetaCooldownEspelho;
        public float cooldownEspelho = 90f;
        private int quantidadeEspelho;
        private bool estaNoCooldownEspelho = false;
        private bool podeDefender = false;

        void Start() {
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            ativaDrenar = false;
            quantidadeDrenar = 2;
            quantidadeEspelho = 3;
            ativaConcentracao = false;
            ativaEspelho = false;
        }

        void Update() {

            if(jogador.transform.localScale == new Vector3(1, 1, 1)){
                direcaoRaycast = Vector2.right;
            } else {
                direcaoRaycast = Vector2.left;
            }

            // HABILIDADE DE DRENAR

            RaycastHit2D alcanceCorrenteDrena = Physics2D.Raycast(transform.position  + new Vector3(jogador.transform.localScale.x, 0, 0), direcaoRaycast, 5f);

            if (Keyboard.current.digit1Key.wasPressedThisFrame && ativaDrenar) {
                if(alcanceCorrenteDrena.collider.CompareTag("Inimigo")) {
                    Personagem alvo = alcanceCorrenteDrena.collider.GetComponent<Personagem>();
                    quantidadeDrenar--;
                    alvo.pontosVida -= 6;
                    jogador.pontosVida += 3;
                } else {
                    quantidadeDrenar--;
                }
            }

            DrenarCooldown();

            if (quantidadeDrenar <= 0) {
                ativaDrenar = false;
                IniciarCooldownDrenar();
            }

            //HABILIDADE DE CONCENTRAÇÃO

            ConcentracaoCooldown();

            if (Keyboard.current.digit2Key.wasPressedThisFrame && ativaConcentracao) {
                jogador.pontosEnergia ++;
                ativaConcentracao = false;
                IniciarCooldownConcentracao();
            }

            // HABILIDADE DE ESPELHO REFLETOR

            if (Keyboard.current.digit3Key.wasPressedThisFrame && ativaEspelho) {
                quantidadeEspelho--;
                podeDefender = true;
            }

            if(quantidadeEspelho <= 0){
                ativaEspelho = false;
                IniciarCooldownEspelho();
            }

            EspelhoCooldown();

            RaycastHit2D detectaAtaque = Physics2D.Raycast(transform.position  + new Vector3(jogador.transform.localScale.x, 0, 0), direcaoRaycast, 0.5f);

            if (detectaAtaque.collider != null) {
                var inimigo = detectaAtaque.collider.GetComponent<Inimigo>();
                //var boss = detectaAtaque.collider.GetComponent<Boss>();

                if(inimigo != null && inimigo.estaAtacando && podeDefender && ativaEspelho) {
                    podeDefender = false;
                    inimigo.pontosVida -= inimigo.ataque;
                    jogador.pontosVida += inimigo.ataque;
                }
                /*
                if(boss != null && boss.estaAtacando && podeDefender && ativaEspelho) {
                    podeDefender = false;
                    jogador.pontosVida += boss.ataque / 2;
                }
                */
            }
        }

        private void IniciarCooldownDrenar() {
            if (estaNoCooldownDrenar) return;
            estaNoCooldownDrenar = true;
            resetaCooldownDrena = cooldownDrenar;
        }

        private void DrenarCooldown(){
            if (!estaNoCooldownDrenar) return;

            resetaCooldownDrena -= Time.deltaTime;
            if (resetaCooldownDrena > 0) return;

            quantidadeDrenar = 2;
            ativaDrenar = true;
            estaNoCooldownDrenar = false;
            resetaCooldownDrena = 0f;
        }

        private void IniciarCooldownConcentracao() {
            if (estaNoCooldownConcentracao) return;
            estaNoCooldownConcentracao = true;
            resetaCooldownConcentracao = cooldownConcentracao;
        }

        private void ConcentracaoCooldown(){
            if (!estaNoCooldownConcentracao) return;

            resetaCooldownConcentracao -= Time.deltaTime;
            if (resetaCooldownConcentracao > 0) return;

            ativaConcentracao = true;
            estaNoCooldownConcentracao = false;
            resetaCooldownConcentracao = 0f;
        }

        private void IniciarCooldownEspelho() {
            if (estaNoCooldownEspelho) return;
            estaNoCooldownEspelho = true;
            resetaCooldownEspelho = cooldownEspelho;
        }

        private void EspelhoCooldown(){
            if (!estaNoCooldownEspelho) return;

            resetaCooldownEspelho -= Time.deltaTime;
            if (resetaCooldownEspelho > 0) return;

            quantidadeEspelho = 3;
            ativaEspelho = true;
            estaNoCooldownEspelho = false;
            resetaCooldownEspelho = 0f;
        }
    }
}
