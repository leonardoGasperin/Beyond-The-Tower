using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

namespace btt.Aplicacao.Contrato {

    public interface IEnergiaServico {

        public int ReduzirEnergia(int pontosEnergia);
        public void RecuperarEnergia(int pontosEnergia, int energiaMaximaPulo);
        public Task RecuperarEnergiaAutomaticamente(int pontosEnergia);
    }
}