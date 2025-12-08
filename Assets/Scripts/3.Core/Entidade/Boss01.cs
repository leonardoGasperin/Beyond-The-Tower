using btt.Apresentacao.Gerenciadores.InimigoGerenciador;
using System.Threading.Tasks;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class Boss01 : Inimigo
    {
        public GameObject tiroMucoPrefab;
        public GameObject chuvaMucoPrefab;
        public Transform pontoDeTiro;
        public int contadorTiro = 4;

        private float posicaoYChuva = 12f;
        private float posicaoXMinChuva = -9f;
        private float posicaoXMaxChuva = 12f;
        private bool cancelarBoss = false;
        private bool podeAtirarMuco;
        private bool podeChuva;

        protected override void Start()
        {
            base.Start();
            maxHP = 100;
            pontosVida = 100;
            podeAndar = false;
            estaVivo = true;

            _ = ComportamentoBoss();
        }

        protected override void Update()
        {
            if (jogador.pontosVida <= 0) return;
            base.Update();

            distancia = transform.position.x - jogador.transform.position.x;
            Orientacao();
        }

        public override void Morreu()
        {
            base.Morreu();
            cancelarBoss = true;
        }

        private async Task ComportamentoBoss()
        {
            while (!cancelarBoss && estaVivo)
            {
                if (pontosVida <= 0)
                {
                    estaVivo = false;
                    break;
                }
                if (pontosVida > maxHP / 2)
                {
                    await PrimeiraForma();
                } /*else {
                    await SegundaForma();
                }*/
            }
        }

        private async Task PrimeiraForma()
        {
            await RepetirTiroMuco();
            await Task.Delay(5000);
            await RepetirChuva();
        }

        /// TODO: refatorar para remover overengineering
        private void IntervaloTiroMuco()
        {
            if (cancelarBoss || !estaVivo) return;
            float distanciaAtual = transform.position.x - jogador.transform.position.x;
            GameObject tiro = Instantiate(tiroMucoPrefab, pontoDeTiro.position, Quaternion.identity);
            TiroMuco tiroMuco = tiro.GetComponent<TiroMuco>();
            tiroMuco.direcao = distanciaAtual > 0 ? -1f : 1f;
        }

        private async Task RepetirChuva()
        {
            for (int i = 0; i < Random.Range(10, 16); i++)
            {
                if (cancelarBoss || !estaVivo) return;
                ChuvaDeMuco();
                await Task.Delay(500);
            }
        }

        /// TODO: abstrair para Serviço de Ataque de Boss
        /// TODO: refatorar para remover overengineering
        private async Task RepetirTiroMuco()
        {
            await Task.Delay(3000);
            for (int i = 0; i < contadorTiro; i++)
            {
                if (cancelarBoss || !estaVivo) return;
                IntervaloTiroMuco();
                await Task.Delay(3000);
            }
        }

        /// TODO: abstrair para Serviço de Ataque de Boss
        private void ChuvaDeMuco()
        {
            if (cancelarBoss || !estaVivo) return;
            Instantiate(chuvaMucoPrefab, new Vector2(Random.Range(posicaoXMinChuva, posicaoXMaxChuva), transform.position.y + posicaoYChuva), Quaternion.identity);
        }
    }

}
