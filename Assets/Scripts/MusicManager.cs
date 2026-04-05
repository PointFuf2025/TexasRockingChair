using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musique;
    [SerializeField] private AudioSource musiqueSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musiqueSource.clip = musique[Random.Range(0, musique.Length)];
        musiqueSource.Play();
    }

}
