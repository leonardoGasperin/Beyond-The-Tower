using btt.Aplicacao.Handler.CarregarHandler;
using btt.Core.Entidade;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace btt.Apresentacao.Gerenciadores.GerenciadorCheckpoint
{
    public class GerenciadorCheckpoint : MonoBehaviour
    {
        private Jogador jogador;
        private GerenciadorCamera mainCamera;
        private CanvasGroup energiasCanvasGroup;
        private bool salvarResetado = false;

        private async void Start()
        {
            jogador = FindFirstObjectByType<Jogador>();
            mainCamera = FindFirstObjectByType<GerenciadorCamera>();
            energiasCanvasGroup = GameObject.Find("Energias").GetComponent<CanvasGroup>();

            if (!File.Exists(Application.persistentDataPath + Save.SAVE))
                return;

            await CarregarEstadoAsync();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Save.ResetarSave();
                salvarResetado = true;
            }
        }

        private async Task CarregarEstadoAsync()
        {
            await Task.Yield();

            SalvarDados salvarDados = Carregar.CarregarDados();
            if (salvarDados == null) return;

            jogador.transform.position = salvarDados.dadosJogador.posicaoCheckpoint;
            jogador.pontosEnergia = salvarDados.dadosJogador.energia;
            jogador.pontosVida = salvarDados.dadosJogador.hp;

            Vector3 pos = mainCamera.transform.position;
            pos.y = jogador.transform.position.y + 3f;
            mainCamera.transform.position = pos;

            mainCamera.cameraPodeSubir = salvarDados.dadosCamera.cameraPodeSubir;
            energiasCanvasGroup.alpha = salvarDados.dadosUIEnergias.alpha;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause && !salvarResetado)
                SalvarEstado();
        }

        private void OnApplicationQuit()
        {
            if (!salvarResetado)
                SalvarEstado();
        }

        private void SalvarEstado()
        {
            Save.SalvarEstadoJogo(jogador.gameObject, mainCamera, energiasCanvasGroup);
        }
    }

}