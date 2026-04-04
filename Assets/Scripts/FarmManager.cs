using Unity.VisualScripting;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    [SerializeField]
    private Crop[] crops;

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

    public Crop GetRandomCrop()
    {
        int randomCropIndex = Random.Range(0, crops.Length - 1);

        return crops[randomCropIndex];
    }
}
