using btt.Aplicacao.Contrato;
using btt.Core.Entidade;

namespace btt.Aplicacao.Servico
{

    public class CombateServico : ICombateServico
    {
        public void Atacando(int ataque, Personagem personagem)
        {
            if (personagem == null)
                return;
            personagem.Dano(ataque);
        }
    }
}