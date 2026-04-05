using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmManager : MonoBehaviour
{
    [SerializeField]
    public Crop[] crops;

    public List<Crop> aliveCrops = new List<Crop>();

    [SerializeField]
    private int Score;
    int internalScore;

    [SerializeField] public AudioSource cropDeathSound;

    [SerializeField] public Sprite DeadCrop;

    public float growIncrement;

    public int randomCropGrowFactor = 5;

    public float cropMaxSize = 5f;
    public int maxIncrementCount;

    public static FarmManager Instance;

    [SerializeField] private GameObject cropSample;

    [SerializeField] private Transform cropRoot;

    public void Awake()
    {
        
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
    }

    public void Start()
    {
        this.crops = FindObjectsByType<Crop>();
        for (int i = 0; i < crops.Length; i++)
        {
            aliveCrops.Add(crops[i]);
            UiManager.instance.AddCorn();
        }
    }

    public void CheckForLevelUp() 
    { 
        if(internalScore > 10) 
        { 
            LevelUp();
            internalScore = 0;
        }
        else 
        {
            internalScore++;
        }

    }

    public void LevelUp() 
    {
        CrowManager.instance.globalCrowSpeed += 0.5f;
        CrowManager.instance.CrowSpawnRate -= 0.5f;
    }

    public Crop GetRandomCrop()
    {
        int randomCropIndex = Random.Range(0, aliveCrops.Count - 1);

        if (randomCropIndex < 0 || aliveCrops.Count == 0)
        {
            return null;
        }

        return aliveCrops[randomCropIndex];
    }

    public void AddScore()
    {
        Score++;
        UiManager.instance.UpdateScore(Score);
        CheckForLevelUp();
    }

    public void CheckGameOver()
    {
        if (aliveCrops.Count <= 0) 
        {
            UiManager.instance.ShowLoosePanel();
        }
    }

    public void PlantCrop(Vector3 position)
    {
        Crop crop = Instantiate(cropSample, position, Quaternion.identity, cropRoot).GetComponent<Crop>();
        this.aliveCrops.Add(crop);
        UiManager.instance.AddCorn();
    }

    public void Update()
    {
    }
}
