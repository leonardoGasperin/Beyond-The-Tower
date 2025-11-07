using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using btt.Aplicacao.Contrato;

namespace btt.Aplicacao.Servico {

    public class EnergiaServico:IEnergiaServico {

        public int ReduzirEnergia(int pontosEnergia){
            pontosEnergia --;
            if(pontosEnergia < 0) pontosEnergia = 0;
            return pontosEnergia;
        }

        public void RecuperarEnergia(int pontosEnergia, int energiaMaximaPulo){
            pontosEnergia ++;
            if (pontosEnergia > energiaMaximaPulo) pontosEnergia = energiaMaximaPulo;
        }

        public async Task RecuperarEnergiaAutomaticamente(int pontosEnergia){
            pontosEnergia ++;
            await Task.Delay(2000);
        }
    }
}