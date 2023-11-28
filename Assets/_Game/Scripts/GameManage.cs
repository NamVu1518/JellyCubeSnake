using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public enum GameState
{
    onInit = 0,
    onPlaying = 1,
    onPaused = 2,
    onWin = 3,
    onLose = 4
}

public class GameManage : Singleton<GameManage>
{
    private GameState _state;

    public GameState State
    {
        get { return _state; }
    }

    private void Start()
    {
        Mainmenu();

        SpawnCubeForInitGame(Constant.VOLUMM_CUBE_IN_MAP, 0.01f);
        SpawnEnemyForInitGame(Constant.VOLUMN_ENEMY_IN_GAME);
    }

    public void ChangGameState(GameState gameState)
    {
        _state = gameState;
    }

    private void SpawnCubeForInitGame(int volume, float delay)
    {
        StartCoroutine(SpawnCubesCoroutine(volume, delay));
    }

    private void SpawnEnemyForInitGame(int volume)
    {
        for (int i = 0; i < volume; i++)
        {
            CubeManage.Ins.SpawnEnemy();
        }
    }
    private IEnumerator SpawnCubesCoroutine(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            CubeManage.Ins.SpawnCubeOnMap();
            yield return new WaitForSeconds(delay);
        }
    }

    //For Game 
    public void StartGame()
    {
        ChangGameState(GameState.onPlaying);
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UI_Gamemenu>();
    }

    public void Mainmenu()
    {
        ChangGameState(GameState.onInit);
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UI_Mainmenu>();
    }
}
