using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private Pathfinding pathfinding;
    
    public VisionCone cone;
    public Transform tileMap;

    public int gridHeight = 31;
    public int gridWidth = 31;
    public int cellSize = 1;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject otherPlayerPrefab;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject crossbow;
    [SerializeField] private GameObject gameCanvas;
    
    private int killCount;
    private Text killCountText;

    public GameObject thePlayer;

    private List<GameObject> enemyList = new List<GameObject>();


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this) {
            Destroy(gameObject);
            return;
        }
        
        InitGame();
    }

    private void Start() {
    }

    void InitGame()
    {
        Transform tilemap = GameObject.Find("EpicArenaMap").transform;
        cone = Instantiate(cone);
        pathfinding = new Pathfinding(gridHeight, gridWidth, cellSize);

        Instantiate(gameCanvas, new Vector3(0, 0), Quaternion.identity);
        enemyList.Add(GameObject.Find("Enemy"));
        thePlayer = GameObject.Find("Player");


        killCountText = GameObject.Find("KillCount").GetComponent<Text>();
        print("killCountText");
        print(killCountText);
        killCount = 0;
        killCountText.text = killCount.ToString();

        GameObject.Find("Instructions").GetComponent<Text>().text = ("Walk with WASD \nAttack with left moust button \nPick up items with F \nCycle weapons with Q and E");

    }
    
    void Update()
    {   
        if (enemyList.Count == 0)
            enemyList.Add(Instantiate(enemyPrefab, new Vector3(20, 20f), Quaternion.identity).gameObject);
    }

    public void HandleKilledPlayer() {
        foreach (GameObject enemyGameObject in enemyList) {
            enemyGameObject.GetComponent<Enemy>().RemovePlayerFromTarget();
            }
    }

    public void HandleKilledEnemy(GameObject enemy) {
        enemyList.Remove(enemy);
        Destroy(enemy);
        killCount = killCount + 1;
        killCountText.text = killCount.ToString();
        print(killCount);
    }


    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
    
}
