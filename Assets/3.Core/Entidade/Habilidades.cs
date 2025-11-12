using UnityEngine;
using UnityEngine.InputSystem;

namespace btt.Core.Entidade {

    public class Habilidades : MonoBehaviour
    {

        private float resetaCooldown = 0f;
        private Jogador jogador;

        public bool ativaDrenar;
        private Vector2 direcaoCorrenteDrena;
        public float cooldownDrenar = 2f;
        private int quantidadeDrenar;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            ativaDrenar = true;
            quantidadeDrenar = 2;
        }

        // Update is called once per frame
        void Update()
        {
            if(jogador.transform.localScale == new Vector3(1, 1, 1)){
                direcaoCorrenteDrena = Vector2.right;
            } else {
                direcaoCorrenteDrena = Vector2.left;
            }

            RaycastHit2D alcanceCorrenteDrena = Physics2D.Raycast(transform.position  + new Vector3(jogador.transform.localScale.x, 0, 0), direcaoCorrenteDrena, 5f);

            if(Keyboard.current.digit1Key.wasPressedThisFrame && ativaDrenar) {
                quantidadeDrenar --;
                // Animação da corrente
            }

            if (Keyboard.current.digit1Key.wasPressedThisFrame && alcanceCorrenteDrena.collider.CompareTag("Inimigo") && ativaDrenar) {
                Personagem alvo = alcanceCorrenteDrena.collider.GetComponent<Personagem>();
                quantidadeDrenar--;
                alvo.pontosVida -= 2;
                jogador.pontosVida += 3;
            }

            if(quantidadeDrenar <= 0) {
                quantidadeDrenar = 0;
                ativaDrenar = false;
                DrenarCooldown();
            }
        }

        private void DrenarCooldown(){
            resetaCooldown -= Time.deltaTime;
            if (resetaCooldown <= 0) {
                resetaCooldown = cooldownDrenar;
                quantidadeDrenar = 2;
            }
        }
    }
}

// Drenar
// Ativado ao apertar alguma tecla
// Alcance à distância na direção que o jogador está olhando
// Checa colisão ou trigger com inimigo
// Tira vida do inimigo e aumenta vida do jogador
// Contador de vezes que pode usar (ver contador de pulos para referência)
// Após dois usos, tem cooldown de 60 segundos