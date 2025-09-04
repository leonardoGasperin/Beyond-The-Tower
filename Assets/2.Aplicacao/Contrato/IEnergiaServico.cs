using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

namespace btt.Aplicacao.Contrato {

    public interface IEnergiaServico {

        public void ReduzirEnergia(int energiaPulo);
        public void RecuperarEnergia(int energiaPulo, int energiaMaximaPulo);
        public Task RecuperarEnergiaAutomaticamente(int energiaPulo);
    }
}