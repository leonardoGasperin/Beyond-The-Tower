using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class AtravessaPlataforma : MonoBehaviour
{
    private float tempoDesativada = 400; //milissegundos
    private Collider2D colliderJogador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliderJogador = gameObject.GetComponent<Collider2D>();
    }

    private async void Atravessa(Collider2D colliderPlataforma) {
        Physics2D.IgnoreCollision(colliderJogador, colliderPlataforma, true);
        await Task.Delay((int)(tempoDesativada));
        Physics2D.IgnoreCollision(colliderJogador, colliderPlataforma, false);
    }

    // Update is called once per frame
    void Update() {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Plataforma"));
        if(Keyboard.current.downArrowKey.wasPressedThisFrame && hit.collider != null) {
            Atravessa(hit.collider);
        }
    }
}
