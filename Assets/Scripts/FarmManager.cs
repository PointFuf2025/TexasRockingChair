using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmManager : MonoBehaviour
{
    [SerializeField]
    private Crop[] crops;

    public List<Crop> aliveCrops = new List<Crop>();

    [SerializeField]
    private int Score;

    public int randomCropGrowFactor = 5;

    public float cropMaxSize = 5f;

    private static FarmManager _instance;

    public static FarmManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindAnyObjectByType<FarmManager>();
            }

            return _instance;
        }
    }

    public void Start()
    {
        for (int i = 0; i < crops.Length; i++)
        {
            aliveCrops.Add(crops[i]);
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
    }

    public void CheckGameOver()
    {
        if (aliveCrops.Count <= 0) 
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Update()
    {
    }
}
