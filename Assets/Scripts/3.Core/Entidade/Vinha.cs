using UnityEngine;
using btt.Core.Entidade;

public class Vinha : Personagem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        pontosVida = 6;
        tagAlvo = "Jogador";
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(pontosVida <= 0) Destroy(gameObject);
    }
}
