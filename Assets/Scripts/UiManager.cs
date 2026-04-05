using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    public static UiManager instance;

    private void Awake()
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

    public void UpdateScore(int score) 
    {
        scoreText.text = "Score " + score;
    }
}
