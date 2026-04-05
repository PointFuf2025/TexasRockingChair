using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text cropCountText;
    [SerializeField] private Animation scoreAnim;
    [SerializeField] private ParticleSystem scorePS;
    public static UiManager instance;
    public int cropCount;

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

    public void AddCorn() 
    { 
        cropCount++;
        this.cropCountText.text = cropCount.ToString();
    }

    public void RemoveCorn() 
    {
        cropCount--;
        this.cropCountText.text = cropCount.ToString();
    }

    public void UpdateScore(int score) 
    {
        scoreText.text = score.ToString();
        scoreAnim.Rewind();
        scoreAnim.Play();
        scorePS.Play();
    }
}
