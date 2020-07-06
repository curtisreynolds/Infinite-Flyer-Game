using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public PlayerController playerController;

    public static bool hasLost;
    

    //public EnemyCollision enemyCollision;
    public GameObject enemyBlock;
    public GameObject playerShip;
    public GameObject startGameController;
    public GameObject gameovercontroller;
    public GameObject enemyCollision;
    public Text playerScore;
    public static int Score;
    public int OpenScore;

    private float enemyDelay = 1.5f;
    private int gameDifficulty = 0;
    private int prevScore = 0;
    private int spawnWalkWay = 0;

    public bool resetDifficulty = true;


    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        playerScore.GetComponentInParent<CanvasGroup>().alpha = 0f;
        Score = 0;
        Instantiate(gameovercontroller);
        Instantiate(enemyCollision);

    }

    // Update is called once per frame
    void Update()
    {
        playerScore.text = Score.ToString();
        SetGameDifficulty();
        OpenScore = Score;
    }


    public void StartGame()
    {
        playerScore.GetComponentInParent<CanvasGroup>().alpha = 1f;
        coroutine = SpawnEnemy();
        StartCoroutine(coroutine);
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {

            yield return new WaitForSeconds(enemyDelay);
            if (spawnWalkWay < 2)
            {
                var obj = (GameObject)Instantiate(enemyBlock, new Vector3(Random.Range(-12f, 12f), 5f, 200), Quaternion.identity);
                obj.GetComponent<FallingBlocks>().isBuilding = true;
                obj.GetComponent<FallingBlocks>().enemyStage = gameDifficulty;
                spawnWalkWay += 1;

            }
            else if (spawnWalkWay == 2)
            {
                var obj = (GameObject)Instantiate(enemyBlock, new Vector3(0, Random.Range(1f, 10f), 200), Quaternion.identity);
                obj.GetComponent<FallingBlocks>().isBuilding = false;
                obj.GetComponent<FallingBlocks>().enemyStage = gameDifficulty;
                spawnWalkWay = 0;
            }



        }
    }

    public void HasLost()
    {
        GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<CanvasGroup>().alpha = 0f;
        hasLost = true;
        int tempscore;
        tempscore = PlayerPrefs.GetInt("highscore");
        if (Score > tempscore)
        {
            PlayerPrefs.SetInt("highscore", Score);
            Debug.Log(PlayerPrefs.GetInt("highscore"));

        }
        GameObject.FindGameObjectWithTag("GameOverUi").GetComponent<gameOverController>().SetUi(Score);
        GameObject.FindGameObjectWithTag("GameOverUi").GetComponent<gameOverController>().ShowUi();
    }

    public void ReloadGame()
    {
        
        Score = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        hasLost = false;
        GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraFollow>().ReloadCamera();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ReloadPlayer();
        GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<CanvasGroup>().alpha = 1;


    }
    public void SetGameDifficulty() 
    {

        if (hasLost)
        {
            prevScore = 0;
            enemyDelay = 1.5f;
            gameDifficulty = 0;
            

        }
        else if (Score == prevScore + 10 && !hasLost)
        {

            gameDifficulty += 1;
            if (gameDifficulty < 5 && enemyDelay >= .3f)
            {
                enemyDelay -= .1f;
            }
            else if (gameDifficulty > 5 && gameDifficulty < 10 && enemyDelay >= 0.2f)
            {
                enemyDelay -= .2f;
            }
            else if (enemyDelay > .3f)
            {
                enemyDelay -= .05f;
            }
            else
            {
                enemyDelay = .3f;

            }

            prevScore = Score;
        }


    }
}
