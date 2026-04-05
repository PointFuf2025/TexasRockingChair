using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject cornCountItem;
    [SerializeField] private GameObject cornCountainer;
    [SerializeField] private Animation scoreAnim;
    [SerializeField] private ParticleSystem scorePS;
    public static UiManager instance;
    public int cornIndex;

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
        GameObject newCorn = Instantiate(cornCountItem);
        newCorn.transform.SetParent(cornCountainer.transform, false);
        newCorn.transform.localScale = Vector3.one;
        newCorn.transform.localPosition = Vector3.zero;
    }

    public void RemoveCorn() 
    { 
        cornCountainer.transform.GetChild(cornIndex).gameObject.SetActive(false);
        cornIndex++;
    }

    public void UpdateScore(int score) 
    {
        scoreText.text = score.ToString();
        scoreAnim.Rewind();
        scoreAnim.Play();
        scorePS.Play();
    }
}
