using System.Collections.Generic;
using UnityEngine;

public class CrowManager : MonoBehaviour
{
    [SerializeField] private AudioClip initCorbSound;
    [SerializeField] private AudioClip[] crowDeathSound;

    [SerializeField] private AudioSource crowSpawnSound;

    public AudioClip GetRandomDeathSound() 
    { 
        AudioClip predicate = null;
        if (Random.Range(0, 100) > 96)
        { 
            predicate = initCorbSound;
        }
        else 
        { 
            predicate = crowDeathSound[Random.Range(0,crowDeathSound.Length)];
        }
        return predicate;
    }

    public static CrowManager instance;

    [SerializeField]
    private List<Crow> crowList = new List<Crow>();

    [SerializeField]
    public float CrowSpawnRate = 5f;

    public float globalCrowSpeed = 3;

    [SerializeField]
    private GameObject crowSample;

    [SerializeField]
    private List<Transform> crowRoot;

    [SerializeField]
    private float spawnRadius = 10f;

    private float timer = 0;

    [SerializeField]
    private GameObject crowDeathEffect;

    [SerializeField]
    public Transform defaultTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > CrowSpawnRate)
        {
            int crowAmount = Random.Range(0, 3);
            for(int i = 0;i<crowAmount;i++) 
            {
                SpawnACrow();
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void SpawnACrow() 
    {
        Transform spawnPoint = GetSpawnPosition();
        Vector2 randomPoint = Random.insideUnitSphere * spawnRadius;
        Vector3 spawnPosition = spawnPoint.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        crowSpawnSound.Play();
        var crowObject = Instantiate(crowSample, spawnPosition, Quaternion.identity, spawnPoint);
        var crow = crowObject.GetComponent<Crow>();
        crow.speed = globalCrowSpeed;
        crowList.Add(crow);
        timer = 0;
    }

    public Transform GetSpawnPosition()
    { 
        return crowRoot[Random.Range(0, crowRoot.Count)];
    }

    public void KillCrow(Crow crow)
    {
        for (int i = 0; i < crowList.Count; i++)
        {
            if (crowList[i] == crow)
            {
                crow.PreDeathEffect();
                crow.KillCrow(crowDeathEffect, GetRandomDeathSound());
                crowList.RemoveAt(i);
                Destroy(crow.gameObject);

                break;
            }
        }      
    }

    private void OnDrawGizmos()
    {
        if(crowRoot == null) 
        {
            foreach (Transform t in crowRoot)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(t.position, 10);
            }
        }
        
        
    }
}
