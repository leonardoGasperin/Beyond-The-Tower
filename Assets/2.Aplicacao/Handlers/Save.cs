using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using btt.Core.Entidade;
using System.IO;

public static class Save {

    public const string SAVE = "/salvarDados.json";

    public static void SalvarEstadoJogo(GameObject jogador, GerenciadorCamera mainCamera, CanvasGroup energiasCanvasGroup){
        string filePathSaveData = Application.persistentDataPath + SAVE;
        DadosJogador dadosJogador = ProcessarDadosJogador(jogador);
        DadosCamera dadosCamera = new DadosCamera(mainCamera);
        DadosUIEnergias dadosUIEnergias = new DadosUIEnergias(energiasCanvasGroup);
        SalvarDados salvarDados = new SalvarDados(dadosJogador, dadosCamera, dadosUIEnergias);
        string txt = JsonUtility.ToJson(salvarDados);
        File.WriteAllText(filePathSaveData, txt);
    }

    private static DadosJogador ProcessarDadosJogador(GameObject jogador){
        return new DadosJogador(jogador.GetComponent<Jogador>());
    }

    public static void ResetarSave() {
        string filePath = Application.persistentDataPath + SAVE;

        if (File.Exists(filePath)) {
            File.Delete(filePath);
            Debug.Log("Save resetado com sucesso!");
        }
        else {
            Debug.Log("Nenhum save encontrado para resetar.");
        }
    }
}

[Serializable]
public class SalvarDados {
    public DadosJogador dadosJogador;
    public DadosCamera dadosCamera;
    public DadosUIEnergias dadosUIEnergias;

    public SalvarDados(DadosJogador dadosJogador, DadosCamera dadosCamera, DadosUIEnergias dadosUIEnergias) {
        this.dadosJogador = dadosJogador;
        this.dadosCamera = dadosCamera;
        this.dadosUIEnergias = dadosUIEnergias;
    }
}

[Serializable]
public class DadosJogador {
    
    [SerializeField] public Vector2 posicaoCheckpoint;
    public int energia;
    public int hp;
    /* TODO: adicionar mais dados do jogador aqui
    * Add quests completadas do jogador
    * Add HP do jogador
    * Add skills do jogador
    * Add energia de pulo do jogador
    * Add moralidade do jogador
    */

    public DadosJogador(Jogador jogador) {
        posicaoCheckpoint = jogador.transform.position;
        energia = jogador.pontosEnergia;
        hp = jogador.pontosVida;
    }

}

[Serializable]
public class DadosCamera {
    public bool cameraPodeSubir;

    public DadosCamera(GerenciadorCamera mainCamera) {
        cameraPodeSubir = mainCamera.cameraPodeSubir;
    }
}

[Serializable]
public class DadosUIEnergias {
    public float alpha;

    public DadosUIEnergias(CanvasGroup energiasCanvasGroup) {
        alpha = energiasCanvasGroup.alpha;
    }
}