using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    public GameObject shine;
    public List<Vector3> laneDirections;    //0 top, 1 mid, 2 bot

    public List<Checkpoint> checkpoints;

    public GameObject enemySpawner;

    public bool canBeAI;

    public Text coreText;
    public Text grooveText;
    public Text incomeText;

    public int income;
    public int groove;

    Timer incomeTimer;

    int currentFacingLane = 0;

    void Start()
    {
        incomeTimer = gameObject.AddComponent<Timer>();
        incomeTimer.Start();

        if (canBeAI)
        {
            GameObject parameters = GameObject.Find("Parameters");
            if (parameters.GetComponent<Parameters>().AIPlayer == true)
            {
                gameObject.AddComponent<EnemySimpleAI>();
            }
        }
    }
	
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) && GetComponent<Stats>().player == 0)
            || (Input.GetKeyDown(KeyCode.W) && GetComponent<Stats>().player == 1))
        {
            currentFacingLane--;
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) && GetComponent<Stats>().player == 0)
            || (Input.GetKeyDown(KeyCode.S) && GetComponent<Stats>().player == 1))
        {
            currentFacingLane++;
        }

        if ((Input.GetKeyDown(KeyCode.Keypad1) && GetComponent<Stats>().player == 0)
            || (Input.GetKeyDown(KeyCode.G) && GetComponent<Stats>().player == 1))
        {
            SpawnUnit("SmallSpawn");
        }

        if ((Input.GetKeyDown(KeyCode.Keypad2) && GetComponent<Stats>().player == 0)
            || (Input.GetKeyDown(KeyCode.H) && GetComponent<Stats>().player == 1))
        {
            SpawnUnit("MediumSpawn");
        }

        if ((Input.GetKeyDown(KeyCode.Keypad3) && GetComponent<Stats>().player == 0)
            || (Input.GetKeyDown(KeyCode.J) && GetComponent<Stats>().player == 1))
        {
            SpawnUnit("BigSpawn");
        }

        if (currentFacingLane > 2) {currentFacingLane = 2;}
        if (currentFacingLane < 0) {currentFacingLane = 0;}

        shine.transform.eulerAngles = laneDirections[currentFacingLane];

        income = 1;
        foreach (Checkpoint cp in checkpoints)
        {
            if (cp.gameObject.GetComponent<Stats>().player == GetComponent<Stats>().player)
            {
                income++;
            }
        }

        if (incomeTimer.GetTime() >= 1.0f)
        {
            groove += income;
            incomeTimer.Reset();
        }

        coreText.text = "Core: " + GetComponent<Stats>().hitPoints.ToString();
        grooveText.text = "Groove: " + groove.ToString();
        incomeText.text = "Income: " + income.ToString();
    }

    public void SpawnUnit(string unitType)
    {
        GameObject unit = (GameObject)Instantiate(Resources.Load("Prefabs/" + unitType));

        int cost = unit.GetComponent<Stats>().cost;
        if (cost > groove) {Destroy(unit);}
        else {groove -= cost;}

        unit.GetComponent<Stats>().player = GetComponent<Stats>().player;
        unit.transform.position = transform.position;
        unit.transform.LookAt(laneDirections[currentFacingLane]);

        int walkable = 1 << 0;
        int lane = 1 << (currentFacingLane + 3);

        unit.GetComponent<UnityEngine.AI.NavMeshAgent>().areaMask = walkable | lane;

        unit.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        unit.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

        unit.GetComponent<Unit>().SetDestination(enemySpawner.transform.position);
        unit.GetComponent<Unit>().enemySpawnerPosition = enemySpawner.transform.position;
    }

    public void LookUp()
    {
        currentFacingLane--;
        if (currentFacingLane > 2) {currentFacingLane = 2;}
        if (currentFacingLane < 0) {currentFacingLane = 0;}
    }

    public void LookDown()
    {
        currentFacingLane++;
        if (currentFacingLane > 2) {currentFacingLane = 2;}
        if (currentFacingLane < 0) {currentFacingLane = 0;}
    }

    public void Damage(int damage)
    {
        GetComponent<Stats>().hitPoints -= damage;

        if (GetComponent<Stats>().hitPoints <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
