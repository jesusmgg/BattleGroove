using UnityEngine;
using System.Collections;


public class Unit : MonoBehaviour
{
    public MusicController musicController;
    public int band;

    public Vector3 enemySpawnerPosition;

    public Texture redTexture;

    bool attackReady;

    [SerializeField]
    Unit targetEnemy;

    void Start()
    {
        attackReady = true;
        targetEnemy = null;

        musicController = GameObject.Find("Music Source").GetComponent<MusicController>();
  
        GetComponent<UnityEngine.AI.NavMeshAgent>().speed = GetComponent<Stats>().speed;

        if (GetComponent<Stats>().player == 1)
        {
            GameObject body = transform.Find("Body").gameObject;
            body.GetComponent<Renderer>().material.SetTexture("_MainTex", redTexture);
        }
    }

    void Update()
    {
        float newSpeed = musicController.GetBandData(band) * GetComponent<Stats>().speed;
        GetComponent<UnityEngine.AI.NavMeshAgent>().speed = newSpeed;

        if (targetEnemy == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<Stats>().range);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                GameObject go = hitColliders[i].gameObject;
                if (go.tag == "Unit")
                {
                    if (go.GetComponent<Stats>().player != GetComponent<Stats>().player)
                    {
                        targetEnemy = go.GetComponent<Unit>();
                        break;
                    }
                }
            }
            SetDestination(enemySpawnerPosition);
        }
        else
        {
            SetDestination(targetEnemy.transform.position);
        }

        if (GetComponent<Animator>().GetBool("Attack"))
        {
            if (!attackReady)
            {
                if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f < 0.5f && !GetComponent<Animator>().IsInTransition(0))
                {
                    attackReady = true;
                }
            }

            else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f >= 0.85f && !GetComponent<Animator>().IsInTransition(0))
            {
                if (targetEnemy != null)
                {
                    if (targetEnemy.GetComponent<Stats>().hitPoints - GetComponent<Stats>().attack <= 0)
                    {
                        targetEnemy.Damage(GetComponent<Stats>().attack);
                        targetEnemy = null;
                        GetComponent<Animator>().SetBool("Attack", false);
                    }
                    else
                    {
                        targetEnemy.Damage(GetComponent<Stats>().attack);
                    }
                    attackReady = false;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (targetEnemy != null)
        {
            if (collider.gameObject == targetEnemy.gameObject)
            {
                GetComponent<Animator>().SetBool("Attack", true);
            }
        }

        if (collider.gameObject.tag == "Spawner" && collider.gameObject.GetComponent<Stats>().player != GetComponent<Stats>().player)
        {
            collider.gameObject.GetComponent<Spawner>().Damage(GetComponent<Stats>().attack);
            Die();
        }
    }

    public void SetDestination(Vector3 destination)
    {        
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = destination;
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