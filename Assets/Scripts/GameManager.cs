using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public float turnDelay = 0.05f;
    public float levelStartDelay = 2f;
    public bool doingSetup;

    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private List<Enemy> enemies = new List<Enemy>();
    private bool enemiesMoving; //si no ponemos nada se inicializa falso

    private int level = 0;
    private GameObject levelImage;
    private Text levelText;

    private GameObject input;


    private void Awake(){

        if(GameManager.instance == null){

            GameManager.instance = this;
        }else if(GameManager.instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
    }


    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        input = GameObject.Find("InputNombre");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text =  level + " floor";
        levelImage.SetActive(true);

        enemies.Clear();
        boardScript.SetupScene(level);

        Invoke("HideLevelImage", levelStartDelay); //para programar la llamada de un metodo para que se ejecute 
                                                    //dentro de cierto tiempo usamos Invoke
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;

    }

    public void GameOver()
    {
        levelText.text = level + " is your dead place";
        levelImage.SetActive(true);
        enabled = false;

        

    }


    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }


    private void Update()
    {
        if (playersTurn || enemiesMoving || doingSetup) return;  //si no es el turno del jugador y los enemigos no se estan 
                                                                 // moviendo entonces iniciamos el movimiento de los enemigos
        StartCoroutine(MoveEnemies());                              // o se esta cargando el Setup
    }


    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        level++;
        InitGame();
    }
}
