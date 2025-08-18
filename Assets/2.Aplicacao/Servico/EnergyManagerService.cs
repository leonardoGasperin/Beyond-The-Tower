using UnityEngine;

namespace btt.aplicacao.servico {

    public class EnergyManagerService:IEnergyManager {

        public void ReduzirEnergia(int energiaPulo){
            energiaPulo --;
            if(energiaPulo < 0) energiaPulo = 0;
        }

        public void RecuperarEnergia(int energiaPulo, int energiaMaximaPulo){
            energiaPulo ++;
            if (energiaPulo > energiaMaximaPulo) energiaPulo = energiaMaximaPulo;
        }

        public void RecuperarEnergiaAutomaticamente(int energiaPulo){
            energiaPulo ++;
            yield return new WaitForSeconds(2);
        }
    }
}