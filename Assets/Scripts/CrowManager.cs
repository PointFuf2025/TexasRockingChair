using System.Collections.Generic;
using UnityEngine;

public class CrowManager : MonoBehaviour
{
    [SerializeField]
    private List<Crow> crowList = new List<Crow>();

    [SerializeField]
    private float CrowSpawnRate = 3f;

    [SerializeField]
    private GameObject crowSample;

    [SerializeField]
    private Transform crowRoot;

    [SerializeField]
    private float spawnRadius = 10f;

    private float timer = 0;

    [SerializeField]
    private GameObject crowDeathEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > CrowSpawnRate)
        {
            Vector2 randomPoint = Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPosition = crowRoot.position + new Vector3(randomPoint.x, 0, randomPoint.y);

            var crowObject = Instantiate(crowSample, spawnPosition, Quaternion.identity, crowRoot);
            var crow = crowObject.GetComponent<Crow>();
            crowList.Add(crow);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void KillCrow(Crow crow)
    {
        for (int i = 0; i < crowList.Count; i++)
        {
            if (crowList[i] == crow)
            {
                crow.KillCrow(crowDeathEffect);
                crowList.RemoveAt(i);
                Destroy(crow.gameObject);
                break;
            }
        }      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(crowRoot.position, 10);
    }
}
