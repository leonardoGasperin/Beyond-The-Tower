using UnityEngine;
using System.Collections;

namespace btt.Aplicacao.Contrato {

    public interface ISpawnServico {

        public void Spawn(GameObject prefab, Transform posicao);

    }
}