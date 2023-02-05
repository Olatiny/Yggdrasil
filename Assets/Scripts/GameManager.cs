using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public enum PlayerStage
    {
        stage1,
        stage2,
        stage3
    }

    // Game State expressed as an enum
    [Header("Game States & World Objects")]
    [SerializeField] private GameState state;
    public PlayerStage stage;

    private Camera cam;

    [Header("Enemy Objects")]
    [SerializeField] private GameObject BigSpawnPoints;
    [SerializeField] private GameObject LittleSpawnPoints;
    [SerializeField] private GameObject smallEnemyPrefab;
    //[SerializeField] private GameObject dragonRef;
    [SerializeField] private GameObject nidhogg;

    private List<GameObject> activeEnemies;

    [Header("Player References")]
    [SerializeField] private GameObject player;
    [SerializeField] private int Level2XP = 15;
    [SerializeField] private int Level3XP = 30;

    private int playerXP = 0;
    private int playerHP = 30;

    [Header("UI Objects")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playingCanvas;
    [SerializeField] private GameObject pausedCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Sound Manager")]
    public GameObject soundManager;

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
        Debug.Log("instantiating level");

        Transform[] allBigSpawns = BigSpawnPoints.GetComponentsInChildren<Transform>();
        Transform[] allLittleSpawns = LittleSpawnPoints.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in allBigSpawns)
        {
            //Debug.Log("Big: " + spawnPoint.position);
        }

        foreach (Transform spawnPoint in allLittleSpawns)
        {
            //Debug.Log("Small: " + spawnPoint.position);
            if (spawnPoint.position.x == 0 && spawnPoint.position.y == 0) continue;

            Instantiate(smallEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        //foreach (GameObject g in spawnPoints)
        //{
        //    if (g.CompareTag("littleSpawn"))
        //    {
        //        activeEnemies.Add(Instantiate(smallEnemyPrefab, g.transform.position, g.transform.rotation));
        //    } else if (g.CompareTag("bigSpawn"))
        //    {
        //        activeEnemies.Add(Instantiate(largeEnemyPrefab, g.transform.position, g.transform.rotation));
        //    }
        //}
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance != this) return;

        if (scene.name == "ThomasTestScene" || scene.name == "HaleyTest")
        {
            playerHP = 30;
            playerXP = 0;

            stage = PlayerStage.stage1;

            BigSpawnPoints = GameObject.FindGameObjectWithTag("BigObjects");
            LittleSpawnPoints = GameObject.FindGameObjectWithTag("LittleObjects");
            player = GameObject.FindGameObjectWithTag("Player");
            cam = Camera.main;
            healthText.text = "Health: " + playerHP;
            cam.orthographicSize = 30;
            nidhogg = GameObject.FindGameObjectWithTag("DragonBody");
            nidhogg.SetActive(false);
            Physics2D.IgnoreLayerCollision(0, 9);

            initiateLevel();

            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);

            state = GameState.Playing;
        }

        if (scene.name == "MainMenu")
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
            SettingsCanvas.SetActive(false);
            state = GameState.MainMenu;
        }

        if (scene.name == "IntroCutscene")
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
            state = GameState.IntroCutscene;
        }

        if (scene.name == "OutroCutscene")
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
            state = GameState.OutroCutscene;
        }
    }

    public void addXP(int xp)
    {
        playerXP += xp;

        //if (playerXP >= Level3XP)
        //{
        //    //stage = PlayerStage.stage3;
        //}
        if (playerXP >= Level2XP && stage != PlayerStage.stage3)
        {
            stage = PlayerStage.stage3;
            player.GetComponent<Animator>().Play("BecomeBigger");

            Physics2D.IgnoreLayerCollision(0, 3);

            //GameObject[] objs = GameObject.FindGameObjectsWithTag("Bushman");

            //foreach (GameObject o in objs)
            //{
            //    Physics2D.IgnoreCollision(o.GetComponent<CircleCollider2D>(), player.GetComponent<BoxCollider2D>());
            //}
            playerHP = 30;

            player.GetComponent<BoxCollider2D>().offset += new Vector2(0, -2);
            player.GetComponent<BoxCollider2D>().size += new Vector2(0, 7);
            cam.orthographicSize += 40;
            nidhogg.SetActive(true);
            //cam.fieldOfView += 12;
        }
        else if (playerXP < Level2XP)
        {
            stage = PlayerStage.stage1;
        }
    }

    public void addHP(int hp)
    {
        playerHP += hp;
        healthText.text = "Health: " + playerHP;
    }

    public void loseHP(int hp)
    {
        playerHP -= hp;
        healthText.text = "Health: " + playerHP;

        player.GetComponent<PlayerScript>().TakeDamage();

        soundManager.GetComponent<MusicScript>().DamageSFX();

        if (playerHP <= 0)
        {
            state = GameState.GameOver;

            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            playingCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
        }
    }

    public void StartIntroCutscene()
    {
        SceneManager.LoadScene("IntroCutscene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ThomasTestScene");
    }
    
    public void StartOutroCutscene()
    {
        SceneManager.LoadScene("OutroCutscene");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        state = GameState.Paused;

        soundManager.GetComponent<MusicScript>().PauseAdjust();

        pausedCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        playingCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        state = GameState.Playing;

        soundManager.GetComponent<MusicScript>().UnpauseAdjust();

        pausedCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        playingCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(false);
    }

    public void EnableSettings()
    {
        pausedCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        playingCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void SaveSettings()
    {
        if (state == GameState.MainMenu)
        {
            pausedCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
        else
        {
            pausedCanvas.SetActive(true);
            gameOverCanvas.SetActive(false);
            playingCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
            mainMenuCanvas.SetActive(false);
        }
    }

    public bool CanMove()
    {
        return state == GameState.Playing;
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
