using UnityEngine;
using UnityEngine.InputSystem;

public class JogadorController : MonoBehaviour {
    void Update(){
        if(Keyboard.current == null) return;
        BotaoPulo();
        BotoesDirecao();
    }

    private void BotaoPulo(){
        //Se apertou espaço, pula
        if (Keyboard.current.spaceKey.wasPressedThisFrame && estaChao)
    }

    private void BotoesDirecao(){
        //Seta esquerda pressionada, movimento para a esquerda
        if (Keyboard.current.leftArrowKey.isPressed){
            //movimentoX = -1f;
        }
        //Seta direita pressionada, movimento para a direita
        else if (Keyboard.current.rightArrowKey.isPressed){
            //movimentoX = 1f;
        }
    }
}