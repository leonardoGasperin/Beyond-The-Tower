using UnityEngine;
using UnityEngine.InputSystem;

public class Movimentacao : MonoBehaviour {

    [SerializeField] private float velocidadeMovimento = 10f;
    Vector2 inputMovimento;
    Rigidbody2D rb;
    Inputs inputs;

    void Awake(){
        inputs = new Inputs();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable(){
        inputs.Gameplay.Enable();
        inputs.Gameplay.Movimento.performed += Movimento;
        inputs.Gameplay.Movimento.canceled += Movimento;
    }

    void OnDisable(){
        inputs.Gameplay.Movimento.performed -= Movimento;
        inputs.Gameplay.Movimento.canceled -= Movimento;
        inputs.Gameplay.Disable();
    }

    void Movimento(InputAction.CallbackContext context){
        inputMovimento = context.ReadValue<Vector2>();
    }

    void FixedUpdate(){
        
    }

}