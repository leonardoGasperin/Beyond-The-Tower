using UnityEngine.InputSystem;

public class JogadorController
{
    public int BotoesDirecao()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
            return -1;
        else if (Keyboard.current.rightArrowKey.isPressed)
            return 1;
        return 0;
    }

    public bool BotaoPulo()
        => Keyboard.current.spaceKey.wasPressedThisFrame;

    public bool BotaoAtaque()
        => Keyboard.current.zKey.wasPressedThisFrame;

}