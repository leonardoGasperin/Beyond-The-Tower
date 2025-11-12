using UnityEngine;
using System.Collections;
using btt.Aplicacao.Contrato;

namespace btt.Aplicacao.Servico {

    public class SpawnServico : ISpawnServico {

        public void Spawn(GameObject prefab, Transform posicao) {
            Object.Instantiate(prefab, posicao.position, posicao.rotation, posicao);
        }

    }
}