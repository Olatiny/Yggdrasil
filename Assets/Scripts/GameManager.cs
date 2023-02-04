using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        MainMenu,
        IntroCutscene,
        OutroCutscene,
        Paused,
        Playing,
        GameOver
    }

    // Game State expressed as an enum
    private GameState state;

    [Header("Enemy Objects")]
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject smallEnemyPrefab;
    [SerializeField] private GameObject largeEnemyPrefab;
    [SerializeField] private GameObject nidhogg;

    private List<GameObject> activeEnemies;

    [Header("Player References")]
    [SerializeField] private GameObject player;
    [SerializeField] private int Level2XP = 15;
    [SerializeField] private int Level3XP = 30;

    private int playerXP = 0;
    private int playerHP = 3;

    private enum PlayerStage
    {
        stage1,
        stage2,
        stage3
    }

    private PlayerStage stage;

    [Header("UI Objects")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playingCanvas;
    [SerializeField] private GameObject pausedCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Sound Manager")]
    [SerializeField] private GameObject soundManager;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode);
    }

    private void initiateLevel()
    {
        foreach (GameObject g in spawnPoints)
        {
            if (g.CompareTag("littleSpawn"))
            {
                activeEnemies.Add(Instantiate(smallEnemyPrefab, g.transform.position, g.transform.rotation));
            } else if (g.CompareTag("bigSpawn"))
            {
                activeEnemies.Add(Instantiate(largeEnemyPrefab, g.transform.position, g.transform.rotation));
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance != this) return;

        if (scene.name == "MainGame")
        {
            playerHP = 3;
            playerXP = 0;

            initiateLevel();

            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);

            state = GameState.Playing;
        }

        if (scene.name == "MainMenu")
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            state = GameState.MainMenu;
        }

        if (scene.name == "IntroCutscene")
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            state = GameState.IntroCutscene;
        }

        if (scene.name == "OutroCutscene")
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            state = GameState.OutroCutscene;
        }
    }

    public void addXP(int xp)
    {
        playerXP += xp;

        if (playerXP > Level3XP)
        {
            stage = PlayerStage.stage3;
        } else if (playerXP > Level2XP)
        {
            stage = PlayerStage.stage2;
        } else
        {
            stage = PlayerStage.stage1;
        }
    }

    public void addHP(int hp)
    {
        playerHP += hp;
    }

    public void loseHP(int hp)
    {
        playerHP -= hp;

        if (playerHP <= 0)
        {
            state = GameState.GameOver;

            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        // Don't do gameplay junk when in these states
        if (state == GameState.Paused || state == GameState.GameOver || state == GameState.OutroCutscene 
            || state == GameState.IntroCutscene || state == GameState.MainMenu)
        {
            return;
        }

        // Dividing up stages
        switch (stage)
        {
            case PlayerStage.stage1:
                break;
            case PlayerStage.stage2:
                break;
            case PlayerStage.stage3:
                break;
            default:
                break;
        }
    }
}
