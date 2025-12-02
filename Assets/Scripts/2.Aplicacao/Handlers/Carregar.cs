using UnityEngine;
using System.IO;

public static class Carregar {

    public static SalvarDados CarregarDados(){
        try {
            string caminhoArquivo = Application.persistentDataPath + Save.SAVE;
            string conteudoArquivo = File.ReadAllText(caminhoArquivo);
            SalvarDados salvarDados = JsonUtility.FromJson<SalvarDados>(conteudoArquivo);
            return salvarDados;
        }
        catch {
            return null;
        }
    }
}