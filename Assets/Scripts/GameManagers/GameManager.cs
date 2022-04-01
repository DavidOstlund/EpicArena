using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerInputHandler playerInputHandler;

    private Pathfinding pathfinding;
    private GameObject tileMap;
    private GameObject door;

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
    private Text helpText;

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
        tileMap = GameObject.Find("EpicArenaMap");
        door = GameObject.Find(("movableDoor"));
        
        pathfinding = new Pathfinding(gridHeight, gridWidth, cellSize);
        
        enemyList.Add(GameObject.Find("Enemy"));
        thePlayer = GameObject.Find("Player");
        playerInputHandler = thePlayer.GetComponent<PlayerInputHandler>();

        killCountText = GameObject.Find("KillCount").GetComponent<Text>();
        killCount = 0;
        killCountText.text = killCount.ToString();

        GameObject.Find("Instructions").GetComponent<Text>().text = ("Walk with WASD \nAttack with left moust button \nPick up items with F \nCycle weapons with Q and E\nThe sign opens the door");
        helpText = GameObject.Find("HelpText").GetComponent<Text>();

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

    public void changeHelpText(string newHelpText) {
        helpText.enabled = true;
        helpText.text = newHelpText;
    }

    public void hideHelpText() {
        helpText.enabled = false;
    }

    public void switchDoor() {
        if (door.activeSelf == true) {
            print("hiding door");
            door.SetActive(false);
        } else {
            print("showing door");
            door.SetActive(true);
        }
    }

    public void PlayerInputHandlerToggle(bool onOff)
    {
        playerInputHandler.enabled = onOff;
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
    
}
