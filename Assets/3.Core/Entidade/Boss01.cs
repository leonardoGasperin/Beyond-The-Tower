using UnityEngine;
using btt.Aplicacao.DI.Personagem;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace btt.Core.Entidade {

    public class Boss01 : Personagem {

        private Transform barraHP;
        public Image hpVerde;
        public Jogador jogador;
        private float distancia;
        private bool cancelarBoss = false;

        public GameObject tiroMucoPrefab;
        public Transform pontoDeTiro;
        public int contadorTiro = 4;
        private bool podeAtirarMuco;

        public GameObject chuvaMucoPrefab;
        private float posicaoYChuva = 12f;
        private float posicaoXMinChuva = -9f;
        private float posicaoXMaxChuva = 12f;
        private bool podeChuva;

        protected override async void Start(){
            base.Start();
            tagAlvo = "Jogador";
            maxHP = 100;
            pontosVida = 100;
            barraHP = transform.Find("Barra de HP");
            hpVerde = barraHP.transform.Find("HP Base/HP").GetComponent<Image>();
            jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();
            podeAndar = false;
            estaVivo = true;

            _= ComportamentoBoss();
        }

        protected override void Update(){
            base.Update();
            hpVerde.fillAmount = (float)pontosVida/maxHP;

            if(jogador.pontosVida <= 0) return;
            if (pontosVida <= 0) return;

            distancia = transform.position.x - jogador.transform.position.x;
            if(distancia > 0) {
                transform.localScale = new Vector3(1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 0, 0);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
                barraHP.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }

        private void OnDestroy() {
            cancelarBoss = true;
            estaVivo = false;
        }

        private async Task ComportamentoBoss(){
            while(!cancelarBoss && estaVivo) {
                if (pontosVida <= 0) {
                    estaVivo = false;
                    break;
                }
                if(pontosVida > maxHP/2) {
                    await PrimeiraForma();
                } /*else {
                    await SegundaForma();
                }*/
            }
        }

        private async Task PrimeiraForma(){
            await RepetirTiroMuco();
            await Task.Delay(5000);
            await RepetirChuva();
        }

        private async Task RepetirTiroMuco(){
            await Task.Delay(3000);
            for (int i = 0; i < contadorTiro; i++) {
                if(cancelarBoss || !estaVivo) return;
                IntervaloTiroMuco();
                await Task.Delay(3000);
            }
        }

        private void IntervaloTiroMuco(){
            if(cancelarBoss || !estaVivo) return;
            float distanciaAtual = transform.position.x - jogador.transform.position.x;
            GameObject tiro = Instantiate(tiroMucoPrefab, pontoDeTiro.position, Quaternion.identity);
            TiroMuco tiroMuco = tiro.GetComponent<TiroMuco>();
            tiroMuco.direcao = distanciaAtual > 0 ? -1f : 1f;
        }

        private async Task RepetirChuva(){
            for (int i = 0; i < Random.Range(10, 16); i++) {
                if(cancelarBoss || !estaVivo) return;
                ChuvaDeMuco();
                await Task.Delay(500);
            }
        }

        private void ChuvaDeMuco(){
            if(cancelarBoss || !estaVivo) return;
            Instantiate(chuvaMucoPrefab, new Vector2(Random.Range(posicaoXMinChuva, posicaoXMaxChuva), transform.position.y + posicaoYChuva), Quaternion.identity);
        }
    }
}

// Boss vê o jogador e se vira para sua direção
// Boss anda na direção do jogador
// Enquanto está andando, o boss instancia o muco pelo chão com ponto de origem sua boca a intervalos aleatórios
// Se o boss toca no jogador, ele para de andar e para de instanciar o muco e usa a terceira habilidade
// Se o boss não toca no jogador, ele continua andando até chegar na parede
// Ao chegar na parede, ele para de instanciar o muco se vira para o jogador
