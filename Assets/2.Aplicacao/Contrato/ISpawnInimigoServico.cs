using UnityEngine;
using System.Collections;

namespace btt.Aplicacao.Contrato {

    public interface ISpawnInimigoServico {

        public void Spawn(GameObject prefab, Transform posicao);

    }
}