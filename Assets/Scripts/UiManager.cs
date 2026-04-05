using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int killedCrows = 0;

    // Loose panel
    [SerializeField] private TMP_Text scoreTextLoose;
    [SerializeField] private TMP_Text killedCrowsTextLoose;
    [SerializeField] private GameObject looseCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private Button continueButton;

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

        killedCrows = 0;
        continueButton.onClick.AddListener(BackToMainMenu);
        this.looseCanvas.SetActive(false);
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLoosePanel()
    {
        this.gameCanvas.SetActive(false);
        this.looseCanvas.SetActive(true);
        scoreTextLoose.text = scoreText.text;
        killedCrowsTextLoose.text = killedCrows.ToString();
        Cursor.lockState = CursorLockMode.None;
    }
}
