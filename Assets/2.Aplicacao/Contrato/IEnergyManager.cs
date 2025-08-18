using UnityEngine;

namespace btt.Aplicacao.Contrato {

    public interface IEnergyManager {

        public void ReduzirEnergia(int energiaPulo);
        public void RecuperarEnergia(int energiaPulo, int energiaMaximaPulo);
        public void RecuperarEnergiaAutomaticamente(int energiaPulo);

    }
}