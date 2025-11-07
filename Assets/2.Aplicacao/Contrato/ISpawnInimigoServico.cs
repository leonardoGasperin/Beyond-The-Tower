using UnityEngine;
using System.Collections;

namespace btt.Aplicacao.Contrato {

    public interface ISpawnInimigoServico {

        public void SpawnInimigo(GameObject inimigoPrefab, Transform posicao);

    }
}