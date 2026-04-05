using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text cropCountText;
    [SerializeField] private Animation scoreAnim;
    [SerializeField] private ParticleSystem scorePS;
    [SerializeField] private TMP_Text tutoText;
    [SerializeField] private TMP_Text shotReadyText;
    [SerializeField] private Image cooldownFillImage;
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

    private void Update()
    {
        var shootManager = ShootManager.Instance;
        float cooldownRatio = (Time.time - shootManager.LastShootTimePlant) / shootManager.PlantShootDelay;
        cooldownRatio = Mathf.Abs(cooldownRatio);
        this.cooldownFillImage.fillAmount = cooldownRatio;
        bool showText = cooldownRatio >= 1f;
        this.tutoText.alpha = showText ? 1f : 0f;
        this.shotReadyText.alpha = showText ? 1f : 0f;
    }
}
