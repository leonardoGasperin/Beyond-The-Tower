using UnityEngine.InputSystem;

namespace btt.Core.Entidade
{
    public class HabilidadeEspelho : Habilidades
    {
        #region Herdados Metodos
        protected override void Start()
        {
            base.Start();
            quantidade = 3;
            quantidadeMax = 3;
            cooldown = 0;
            resetaCooldown = 90f;
        }
        protected override void Update()
        {
            base.Update();
            if (Keyboard.current.digit3Key.wasPressedThisFrame && ativo && quantidade > 0)
                Espelho(new());
        }

        #endregion

        #region Entidade Metodos
        private void Espelho(Jogador jogador)
        {
            if (!ativo) return;

            AtivarHabilidade();
            quantidade--;
            jogador.estaDefendendo = true;

            if (raycast.collider == null) return;

            var inimigo = raycast.collider.GetComponent<Inimigo>();
            
            if (!inimigo.estaAtacando || !jogador.estaDefendendo) return;

            jogador.estaDefendendo = false;
            inimigo.pontosVida -= /*se for boss ? inimigo.ataque/2 : */ inimigo.ataque;
            //jogador.pontosVida += inimigo.ataque; //sugiro que seja removido HP do jogador levando metade do dano refletido se não ficar muito OP
            //a habilidade ja tira dano do inimigo, não precisa curar o jogador tbm
            //pois se não tira o sentido da habilidade drenar e deixa a habilidade Concentração mais fraca 
        }

        #endregion

    }
}