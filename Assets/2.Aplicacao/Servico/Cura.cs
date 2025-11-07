using UnityEngine;
using btt.Core.Entidade;

public class Cura : MonoBehaviour
{
    public Jogador jogador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador").GetComponent<Jogador>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Jogador")) {
            jogador.pontosVida += 10;
            Destroy(gameObject);
        }
    }
}
