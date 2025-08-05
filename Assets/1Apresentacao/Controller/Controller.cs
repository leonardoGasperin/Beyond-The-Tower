using UnityEngine;
using UnityEngine.InputSystem;

public class Movimentacao : MonoBehaviour {

    [SerializeField] private float forcaDoPulo = 200f;
    [SerializeField] private float velocidade = 10f;
    bool estaNoChao;
    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(Keyboard.current == null) return;

        float movimentoX = 0f;
        
        //Se apertou espaço, pula
        if(Keyboard.current.spaceKey.wasPressedThisFrame){
            if(estaNoChao) {
                Pulo();
            }
            else return;
        }

        //Seta esquerda pressionada, movimento para a esquerda
        if(Keyboard.current.leftArrowKey.isPressed){
            movimentoX = -1f;
        }
        //Seta direita pressionada, movimento para a direita
        else if(Keyboard.current.rightArrowKey.isPressed){
            movimentoX = 1f;
        }

        //transform.position += new Vector3(movimentoX, 0f, 0f) * velocidade * Time.deltaTime;
    }

    //Checar se o jogador está tocando o chão
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Plataforma")) {
            estaNoChao = true;
        }
    }

    //Checar se o jogador não está tocando o chão
    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.CompareTag("Plataforma")){
            estaNoChao = false;
        }
    }

    void Pulo(){
    }