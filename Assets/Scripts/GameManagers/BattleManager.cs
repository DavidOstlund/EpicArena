using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public GameObject thePlayer;
    [SerializeField] private GameObject enemyPrefab;
    private int killCount;
    private Text killCountText;
    private List<GameObject> enemyList;


    public void initBattleManager()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        thePlayer = GameObject.Find("Player");
        

        killCountText = GameObject.Find("KillCount").GetComponent<Text>();
        killCount = 0;
        killCountText.text = killCount.ToString();

        enemyList = new List<GameObject>();
    }
    public async void StartBattle(int level)
    {
        thePlayer.transform.position = new Vector2(18, 20);

        for (int i = 0; i < level; i++)
        {
            enemyList.Add(Instantiate(enemyPrefab, new Vector2(20, 20), Quaternion.identity));
        }
    }

    public void EndBattle()
    {
        thePlayer.transform.position = new Vector2(16,10);
    }

    public void HandleKilledEnemy(GameObject enemy) {
        enemyList.Remove(enemy);
        Destroy(enemy);
        killCount = killCount + 1;
        killCountText.text = killCount.ToString();
        print(killCount);

        if(enemyList.Count == 0)
        {
            this.EndBattle();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
