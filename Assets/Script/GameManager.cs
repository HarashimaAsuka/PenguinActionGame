using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameBlockControlloer[] blocks;
    private GameCoinController[] coins;
    private EnemyController[] enemies;

    private void Awake(){
        this.blocks = (GameBlockControlloer[])GameObject.FindObjectsOfType(typeof(GameBlockControlloer));
        this.coins = (GameCoinController[])GameObject.FindObjectsOfType(typeof(GameCoinController));
        this.enemies = (EnemyController[])GameObject.FindObjectsOfType(typeof(EnemyController));
    }

    public void GameInitialize(){
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerController player = playerObj.GetComponent<PlayerController>();

        if(player)
            player.Initialize();

        Camera mainCamera = Camera.main;
        GameCameraControlller cameraController = mainCamera.GetComponent<GameCameraControlller>();

        if(cameraController)
            cameraController.Initialize();

        foreach(GameBlockControlloer block in this.blocks)
            block.Initialize();

        foreach(GameCoinController coin in this.coins)
            coin.Initialize();

        foreach(EnemyController enemy in this.enemies)
            enemy.Initialize();        

    }
}