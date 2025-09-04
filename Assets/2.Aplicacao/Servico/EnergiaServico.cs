using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using btt.Aplicacao.Contrato;

namespace btt.Aplicacao.Servico {

    public class EnergiaServico:IEnergiaServico {

        public void ReduzirEnergia(int energiaPulo){
            energiaPulo --;
            if(energiaPulo < 0) energiaPulo = 0;
        }

        public void RecuperarEnergia(int energiaPulo, int energiaMaximaPulo){
            energiaPulo ++;
            if (energiaPulo > energiaMaximaPulo) energiaPulo = energiaMaximaPulo;
        }

        public async Task RecuperarEnergiaAutomaticamente(int energiaPulo){
            energiaPulo ++;
            await Task.Delay(2000);
        }
    }
}