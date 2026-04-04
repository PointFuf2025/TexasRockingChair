using JetBrains.Annotations;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private float timer = 0;

    public float randomCropGrowTimer = 5f;

    int growCount;

    bool isDead = false;

    public GameObject cropVisualHolder;
    public SpriteRenderer cropVisual;
    public ParticleSystem cropFx;
    AudioSource cropAudio;
    public Vector3 initialVisualPos;
    public Vector3 currentPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cropVisualHolder = transform.GetChild(0).gameObject;
        cropVisual = cropVisualHolder.GetComponentInChildren<SpriteRenderer>();
        initialVisualPos = cropVisualHolder.transform.localPosition;
        currentPos = initialVisualPos;
        cropFx = transform.GetChild(1).GetComponent<ParticleSystem>();
        cropAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (timer > randomCropGrowTimer)
        {
            /*
            float randomGrow = Random.Range(1, FarmManager.Instance.randomCropGrowFactor) / 10f;
            var toto = new Vector3(0, randomGrow, 0);
            this.transform.localScale += toto;
            */
            IncrementVisual();
            timer = 0;
            randomCropGrowTimer = Random.Range(5, 10);
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (growCount > FarmManager.Instance.maxIncrementCount)
        {
            FarmManager.Instance.AddScore();
            //this.transform.localScale = new Vector3(1, 0, 1);

            //ADD FLUFF FOR EAT
            ResetVisual();
        }
    }

    public void IncrementVisual() 
    {
        currentPos.y += FarmManager.Instance.growIncrement;
        growCount++;
        cropVisualHolder.transform.localPosition = currentPos;
    }

    public void ResetVisual() 
    {
        cropFx.Play();
        cropAudio.Play();
        currentPos = initialVisualPos;
        growCount = 0;
        cropVisualHolder.transform.localPosition = currentPos;
    }

    public void OnCropDeath() 
    { 
        cropVisualHolder.transform.localPosition = initialVisualPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO change this so its the crows that check and use a timer to eat the crop
        Crow crow = other.GetComponent<Crow>();
        if (crow != null && crow.target == this.transform)
        {
            OnCropDeath();
            crow.target = FarmManager.Instance.GetRandomCrop().transform;
            this.isDead = true;
            this.transform.localScale = new Vector3(1, 0, 1);
            FarmManager.Instance.aliveCrops.Remove(this);
            FarmManager.Instance.CheckGameOver();
        }
    }
}
