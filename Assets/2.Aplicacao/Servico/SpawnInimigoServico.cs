using UnityEngine;
using System.Collections;
using btt.Aplicacao.Contrato;

namespace btt.Aplicacao.Servico {

    public class SpawnInimigoServico : ISpawnInimigoServico {

        public void SpawnInimigo(GameObject inimigoPrefab, Transform posicao) {
            Object.Instantiate(inimigoPrefab, posicao);
        }

    }
}