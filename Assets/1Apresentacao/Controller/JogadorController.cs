using UnityEngine;
using UnityEngine.InputSystem;

public class JogadorController {

    public bool BotaoPulo(bool estaChao){
        //Se apertou espaço, pula
        return Keyboard.current.spaceKey.wasPressedThisFrame && estaChao;
    }

    public int BotoesDirecao(){
        //Seta esquerda pressionada, movimento para a esquerda
        if (Keyboard.current.leftArrowKey.isPressed){
            return -1;
        }
        //Seta direita pressionada, movimento para a direita
        else if (Keyboard.current.rightArrowKey.isPressed){
            return 1;
        }
        return 0;
    }

    public bool BotaoAtaque(){
        return Keyboard.current.zKey.wasPressedThisFrame;
    }
}