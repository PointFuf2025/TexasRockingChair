using UnityEngine;

public class Crop : MonoBehaviour
{
    private float timer = 0;

    public float randomCropGrowTimer = 5f;

    bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            float randomGrow = Random.Range(1, FarmManager.Instance.randomCropGrowFactor) / 10f;
            var toto = new Vector3(0, randomGrow, 0);
            this.transform.localScale += toto;
            timer = 0;
            randomCropGrowTimer = Random.Range(5, 10);
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (this.transform.localScale.y > FarmManager.Instance.cropMaxSize)
        {
            FarmManager.Instance.AddScore();
            this.transform.localScale = new Vector3(1, 0, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO change this so its the crows that check and use a timer to eat the crop
        Crow crow = other.GetComponent<Crow>();
        if (crow != null && crow.target == this.transform)
        {
            crow.target = FarmManager.Instance.GetRandomCrop().transform;
            this.isDead = true;
            this.transform.localScale = new Vector3(1, 0, 1);
            FarmManager.Instance.aliveCrops.Remove(this);
            FarmManager.Instance.CheckGameOver();
        }
    }
}
