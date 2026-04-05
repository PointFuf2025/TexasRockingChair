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

    [SerializeField] public AudioSource cropDeathSound;

    [SerializeField] public Sprite DeadCrop;

    public float growIncrement;

    public int randomCropGrowFactor = 5;

    public float cropMaxSize = 5f;
    public int maxIncrementCount;

    public static FarmManager Instance;
  

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

    public Crop GetRandomCrop()
    {
        int randomCropIndex = Random.Range(0, aliveCrops.Count - 1);

        return aliveCrops[randomCropIndex];
    }

    public void AddScore()
    {
        Score++;
        UiManager.instance.UpdateScore(Score);
    }

    public void CheckGameOver()
    {
        if (aliveCrops.Count <= 0) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Update()
    {
    }
}
