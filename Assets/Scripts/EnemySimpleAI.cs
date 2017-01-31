using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySimpleAI : MonoBehaviour
{
    Timer timer;

	void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Start();
	}
	
	void Update()
    {
        if (timer.GetTime() >= 1.0f)
        {
            int r = Random.Range(0, 9);

            if (r >= 0 && r <= 1)
            {
                GetComponent<Spawner>().SpawnUnit("SmallSpawn");
                GetComponent<Spawner>().SpawnUnit("SmallSpawn");
                GetComponent<Spawner>().SpawnUnit("SmallSpawn");
            }
            else if (r >= 2 && r <= 4)
            {
                GetComponent<Spawner>().SpawnUnit("MediumSpawn");
            }
            else if (r >= 5 && r <= 8)
            {
                GetComponent<Spawner>().SpawnUnit("BigSpawn");
            }

            if (r >= 0 && r <= 3)
            {
                GetComponent<Spawner>().LookDown();
            }
            else if (r >= 5 && r <= 8)
            {
                GetComponent<Spawner>().LookUp();
            }

            timer.Reset();
        }
	}
}
