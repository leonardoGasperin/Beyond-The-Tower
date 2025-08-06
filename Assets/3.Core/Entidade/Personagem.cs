using UnityEngine;

public class Personagem : MonoBehaviour
{
    #region Atributos
    public string nome;
    public int pontosVida;
    public int pontosEnergia;
    public int ataque;
    public int defesa;
    public int nivel;
    public int experiencia;
    Transform posicao;
    //Animator animacao;
    public float velocidade;
    public bool estaVivo;
    public bool estaAtacando;
    public bool estaDefendendo;
    public bool estaChao;
    public bool ativo = true;
    #endregion

    #region Unity Methods
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicao = GetComponent<Transform>();
        //animacao = GetComponent<Animator>();
        estaVivo = true;
        estaAtacando = false;
        estaDefendendo = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Methods
    protected virtual void Dano(int danoRecebido)
    {
        int danoFinal = danoRecebido - defesa;
        if (danoFinal < 0)
        {
            danoFinal = 0;
        }
        pontosVida -= danoFinal;
        if (pontosVida <= 0)
        {
            pontosVida = 0;
            Morreu();
        }
    }

    private void Morreu()
    {
        estaVivo = false;
        ativo = false;
        //animacao.SetTrigger("Morreu");
        // Desativar o personagem ou iniciar a lógica de reinício
    }

    public void Atacar(Personagem alvo)
    {
        if (estaVivo && !estaAtacando)
        {
            estaAtacando = true;
            //animacao.SetTrigger("Ataca");
            alvo.Dano(ataque);
            estaAtacando = false;
        }
    }

    #endregion
}
