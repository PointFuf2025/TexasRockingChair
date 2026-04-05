using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerOutgame : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        playButton.onClick.AddListener(this.Play);
    }

    public void Play()
    {
        SceneManager.LoadScene("LD");
    }
}
