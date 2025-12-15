using UnityEngine.InputSystem;

namespace btt.Core.Entidade
{
    public class HabilidadeConcentracao : Habilidades
    {
        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            quantidade = 0;
            quantidadeMax = 0;
            cooldown = 0;
            resetaCooldown = 60f;
        }

        protected override void Update()
        {
            base.Update();
            if (Keyboard.current.digit2Key.wasPressedThisFrame && ativo)
                Concentracao(new());
        }

        #endregion

        #region Entidade metodos
        private void Concentracao(Jogador jogador)
        {
            AtivarHabilidade();
            jogador.pontosEnergia++;
        }

        #endregion

    }
}