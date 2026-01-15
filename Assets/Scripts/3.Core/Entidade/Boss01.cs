using btt.Apresentacao.Gerenciadores.GerenciadorSpawn;
using System.Threading.Tasks;
using UnityEngine;

namespace btt.Core.Entidade
{
    public class Boss01 : Inimigo
    {
        #region Atributos
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

        #endregion

        #region Herdados Metodos
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
            ObjetoOrientacao();
        }

        protected override void Morreu()
        {
            ativo = false;
            podeAtacar = false;
            estaDefendendo = false;
            estaChao = true;
            estaAtacando = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            jogador.pontosEnergia += EnergiaRecompensa;
            GerenciadorSpawn.Instance.DestruirInimigo(this);
        }
        #endregion

        #region Entidade Metodos
        private async Task ComportamentoBoss()
        {
            while (!cancelarBoss && estaVivo)
            {
                switch(pontosVida > maxHP / 2)
                {
                    case true:
                        await PrimeiraForma();
                        break;
                    case false:
                        // await SegundaForma();
                        break;
                    default:
                }
                
            }
        }

        private async Task PrimeiraForma()
        {
            await RepetirTiroMuco();
            await Task.Delay(5000);
            await RepetirChuva();
        }

        private void IntervaloTiroMuco()
        {
            if (cancelarBoss || !estaVivo) return;
            GameObject tiro = Instantiate(tiroMucoPrefab, pontoDeTiro.position, Quaternion.identity);
            TiroMuco tiroMuco = tiro.GetComponent<TiroMuco>();
            tiroMuco.direcao = distancia > 0 ? -1f : 1f;
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

        private void ChuvaDeMuco()
        {
            if (cancelarBoss || !estaVivo) return;
            Instantiate(chuvaMucoPrefab, new Vector2(Random.Range(posicaoXMinChuva, posicaoXMaxChuva), transform.position.y + posicaoYChuva), Quaternion.identity);
        }
        #endregion
    }

}
