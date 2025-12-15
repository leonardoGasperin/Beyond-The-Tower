using UnityEngine;
using UnityEngine.InputSystem;

namespace btt.Core.Entidade
{
    public class HabilidadeDrenar : Habilidades
    {
        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            quantidade = 2;
            quantidadeMax = 2;
            cooldown = 0;
            resetaCooldown = 60f;
        }

        protected override void Update()
        {
            base.Update();
            if (Keyboard.current.digit1Key.wasPressedThisFrame && ativo && quantidade > 0)
                Drenar(new(), new(), raycast);
        }

        #endregion

        #region Entidade Metodos
        private void Drenar(Personagem alvo, Personagem jogador, RaycastHit2D raycast)
        {
            AtivarHabilidade();
            
            if (!raycast.collider.CompareTag("Inimigo"))
            {
                quantidade--;
                return;
            }
            
            quantidade--;
            alvo.pontosVida -= 6;
            jogador.pontosVida += 3;
        }

        #endregion

    }
}