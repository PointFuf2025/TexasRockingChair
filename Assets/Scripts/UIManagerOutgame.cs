using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerOutgame : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button backButton;

    [SerializeField] private Camera creditCamera;
    [SerializeField] private Camera controlsCamera;
    [SerializeField] private CameraMainMenu camInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void Awake()
    {
        playButton.onClick.AddListener(this.Play);
        creditButton.onClick.AddListener(this.ShowCredits);
        backButton.onClick.AddListener(this.ExitCreditsOrControls);
        controlsButton.onClick.AddListener(this.ShowControls);
    }

    public void ShowCredits() 
    { 
        camInstance.currentCam.gameObject.SetActive(false);
        creditCamera.gameObject.SetActive(true);
        camInstance.canplay = false;
        playButton.gameObject.SetActive(false);
        creditButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }

    public void ShowControls()
    {
        camInstance.currentCam.gameObject.SetActive(false);
        controlsCamera.gameObject.SetActive(true);
        camInstance.canplay = false;
        playButton.gameObject.SetActive(false);
        creditButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }

    public void ExitCreditsOrControls() 
    {
        creditCamera.gameObject.SetActive(false);
        controlsCamera.gameObject.SetActive(false);
        camInstance.currentCam.gameObject.SetActive(true);
        camInstance.canplay = true;
        playButton.gameObject.SetActive(true);
        creditButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        camInstance.timer = 0;
    }

    public void Play()
    {
        SceneManager.LoadScene("LD");
    }
}
