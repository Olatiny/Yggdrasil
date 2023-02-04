using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        MainMenu,
        Cutscene,
        Paused,
        Playing,
        GameOver
    }

    [Header("Enemy Objects")]
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject smallEnemyPrefab;
    [SerializeField] private GameObject largeEnemyPrefab;
    [SerializeField] private GameObject nidhogg;

    private GameObject[] activeEnemies;

    [Header("Player Objects")]
    [SerializeField] private GameObject player;

    private int playerLevel;
    private int playerXP;

    [Header("UI Objects")]
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas playingCanvas;
    [SerializeField] private Canvas pausedCanvas;
    [SerializeField] private Canvas gameOverCanvas;

    [Header("Sound Manager")]
    [SerializeField] private GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
