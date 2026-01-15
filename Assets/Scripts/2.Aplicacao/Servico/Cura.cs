using UnityEngine;
using btt.Core.Entidade;

public class Cura : MonoBehaviour
{
    public Jogador jogador;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();   
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Jogador")) {
            jogador.pontosVida += 10;
            Destroy(gameObject);
        }
    }
}
