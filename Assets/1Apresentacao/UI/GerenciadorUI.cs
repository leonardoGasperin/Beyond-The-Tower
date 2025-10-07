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
        private GerenciadorCamera camera;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            numeroEnergias = GameObject.Find("Quantidade").GetComponent<TMP_Text>();
            jogador = GameObject.Find("Jogador").GetComponent<Jogador>();
            camera = GameObject.Find("Main Camera").GetComponent<GerenciadorCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            numeroEnergias.text = jogador.pontosEnergia.ToString();

            if (!gameOver && jogador.transform.position.y < camera.transform.position.y - 10) {
                gameOver = true;
                GameOver();
            }
        }

        public void GameOver(){
            painelGameOver.SetActive(true);
            camera.cameraPodeSubir = false;
        }

        public void Reiniciar(){
            SceneManager.LoadScene("Beyond the Tower");
        }
    }
}
