using UnityEngine;

public class Movimentacao : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] private float velocidade = 10f;
    [SerializeField] private float forcaDoPulo = 200f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        rb.linearVelocity = new Vector2(movimentoX * velocidade, 0f);
    }

    void Pulo(){
        rb.AddForce(Vector2.up * forcaDoPulo, ForceMode2D.Impulse);
    }
}