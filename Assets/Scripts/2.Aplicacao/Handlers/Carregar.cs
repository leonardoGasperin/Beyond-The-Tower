using UnityEngine;
using System.IO;

namespace btt.Aplicacao.Handler.CarregarHandler
{
    public static class Carregar
    {
        public static SalvarDados CarregarDados()
        {
            try
            {
                string caminhoArquivo = Application.persistentDataPath + Save.SAVE;
                string conteudoArquivo = File.ReadAllText(caminhoArquivo);
                SalvarDados salvarDados = JsonUtility.FromJson<SalvarDados>(conteudoArquivo);
                return salvarDados;
            }
            catch
            {
                return null;
            }
        }
    }
}
