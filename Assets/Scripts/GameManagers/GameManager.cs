using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private BattleManager battleManager;

    private Pathfinding pathfinding;
    private GameObject tileMap;
    private GameObject door;

    public int gridHeight = 31;
    public int gridWidth = 31;
    public int cellSize = 1;

    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject crossbow;
    [SerializeField] private GameObject gameCanvas;
    
    private int killCount;
    private Text killCountText;
    private Text helpText;

    public GameObject thePlayer;

    private int currentLevel = 1;

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

        thePlayer = GameObject.Find("Player");
        battleManager = this.GetComponent<BattleManager>();
        battleManager.initBattleManager();

        GameObject.Find("Instructions").GetComponent<Text>().text = ("Walk with WASD \nAttack with left moust button \nPick up items with F \nCycle weapons with Q and E\nThe sign opens the door");
        helpText = GameObject.Find("HelpText").GetComponent<Text>();

    }
    
    void Update()
    {   
    }
    /*

    public void HandleKilledPlayer() {
        foreach (GameObject enemyGameObject in enemyList) {
            enemyGameObject.GetComponent<Enemy>().RemovePlayerFromTarget();
            }
    }
    */
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

    public void StartBattle(int level) {
        battleManager.enabled = true;
        battleManager.StartBattle(currentLevel);
        currentLevel++;
    }

    public void PlayerInputHandlerToggle(bool onOff)
    {
        //playerInputHandler.enabled = onOff;
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
    
}
