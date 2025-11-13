using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace btt.Core.Entidade {
    public class GerenciadorUI : MonoBehaviour
    {
        [SerializeField] private GameObject painelGameOver;
        private TMP_Text numeroEnergias;
        private Jogador jogador;
        private bool gameOver = false;
        private GerenciadorCamera mainCamera;
        private CanvasGroup energiasCanvasGroup;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            energiasCanvasGroup = GameObject.Find("Energias").GetComponent<CanvasGroup>();
            energiasCanvasGroup.alpha = 0f;
            numeroEnergias = GameObject.Find("Quantidade").GetComponent<TMP_Text>();
            jogador = GameObject.Find("Jogador").GetComponent<Jogador>();
            mainCamera = GameObject.Find("Main Camera").GetComponent<GerenciadorCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            numeroEnergias.text = jogador.pontosEnergia.ToString();

            if (!gameOver && jogador.transform.position.y < mainCamera.transform.position.y - 10) {
                jogador.pontosVida = 0;
                gameOver = true;
                GameOver();
            }
        }

        public void GameOver(){
            painelGameOver.SetActive(true);
            mainCamera.cameraPodeSubir = false;
        }

        public void Reiniciar(){
            SceneManager.LoadScene("Beyond the Tower");
        }
    }
}
