using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI.Table;

public class Crow : MonoBehaviour
{
    GameObject crowVisual;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float cropEatingSpeed = 3f;

    [SerializeField]
    private ParticleSystem CropEatingMiamMiamMiam;

    private bool isEatingCrop = false;
    public Crop cropTarget;

    [SerializeField] 
    private AudioSource crowEatingSound;

    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cropTarget = FarmManager.Instance.GetRandomCrop();
        crowVisual = transform.GetChild(0).gameObject;
        crowVisual.transform.localPosition = Vector3.zero;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            crowVisual.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void PreDeathEffect() 
    { 
        crowVisual.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void KillCrow(GameObject _effect, AudioClip _clip) 
    { 
        GameObject newPs = Instantiate(_effect);
        newPs.transform.parent = crowVisual.transform;
        newPs.transform.localPosition = Vector3.zero;
        crowVisual.gameObject.SetActive(false);
        newPs.transform.parent = MouseController.instance.World;
        newPs.GetComponent<AudioSource>().clip = _clip;
        newPs.GetComponent<AudioSource>().Play();

        this.cropTarget.isBeingEat = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(
            transform.position,
            cropTarget.topTarget.position,
            speed * Time.deltaTime
        );
        crowVisual.transform.LookAt(Camera.main.transform.position, Vector3.up);

        // change target if dead (killed by another crow)
        if (this.cropTarget.isDead)
        {
            this.cropTarget = FarmManager.Instance.GetRandomCrop();
            timer = 0;
            isEatingCrop = false;
        }

        if (!isEatingCrop)
        {
            if (this.crowEatingSound.isPlaying)
            {
                this.crowEatingSound.Pause();
                CropEatingMiamMiamMiam.Stop();
            }
            
            return;
        }

        if (timer > cropEatingSpeed)
        {
            /*
            float randomGrow = Random.Range(1, FarmManager.Instance.randomCropGrowFactor) / 10f;
            var toto = new Vector3(0, randomGrow, 0);
            this.transform.localScale += toto;
            */
            
            cropTarget.OnCropDeath();
            isEatingCrop = false;
            timer = 0;
            this.cropTarget = FarmManager.Instance.GetRandomCrop();
        }
        else
        {
            this.cropTarget.isBeingEat = true;
            timer += Time.deltaTime;
        }
    }

    public void StartEatCrop()
    {
        this.isEatingCrop = true;
        this.crowEatingSound.Play();
        CropEatingMiamMiamMiam.Play();
    }
}
