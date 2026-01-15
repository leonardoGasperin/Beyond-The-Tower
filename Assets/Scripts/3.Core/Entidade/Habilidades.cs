using UnityEngine;
using UnityEngine.InputSystem;

namespace btt.Core.Entidade
{
    public class Habilidades : MonoBehaviour
    {
        #region Atributos
        private Vector2 direcaoRaycast;

        protected bool ativo;
        protected bool estaCooldown;
        protected int quantidade;
        protected int quantidadeMax;
        protected float cooldown;
        protected float resetaCooldown;
        protected RaycastHit2D raycast;

        #endregion

        #region Unity Metodos
        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
            raycast = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), direcaoRaycast, 0.5f);

            if (!ativo && cooldown > 0)
                cooldown -= Time.deltaTime;

            if (quantidade <= 0)
                CooldownHabilidade();
        }

        #endregion

        #region Entidade Metodos
        protected virtual void AtivarHabilidade()
        {
            if (estaCooldown) return;
            ativo = false;
            estaCooldown = true;
            cooldown = resetaCooldown;
        }

        protected virtual void CooldownHabilidade()
        {
            if (!estaCooldown || cooldown > 0) return;
            ativo = true;
            estaCooldown = false;
            quantidade = quantidadeMax;
        }

        #endregion

    }
}
