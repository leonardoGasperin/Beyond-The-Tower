using btt.Core.Entidade;

public class Vinha : Personagem
{
    protected override void Start()
    {
        pontosVida = 6;
        tagAlvo = "Jogador";
    }

    protected override void Update()
    {
        if(pontosVida <= 0) Destroy(gameObject);
    }
}
