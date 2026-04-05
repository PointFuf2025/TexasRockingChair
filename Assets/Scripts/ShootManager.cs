using System.Collections;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootEffect;
    [SerializeField]
    private Transform BulletSpawnPoint;

    [SerializeField] private Transform ForcePoint;

    [SerializeField]
    private ParticleSystem ImpactParticleSystem;

    [SerializeField] private ParticleSystem MuzzleFlash;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private LineRenderer TrailEffect;
    [SerializeField]
    private float ShootDelay = 0.5f;

    [SerializeField]
    public float PlantShootDelay = 8f;

    [SerializeField]
    private LayerMask ShootMask;

    [SerializeField]
    private LayerMask PlantMask;

    [SerializeField]
    private float BulletSpeed = 100;

    [SerializeField]
    private int numberOfProjectiles = 5;

    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private CrowManager crowManager;

    [SerializeField]
    private Transform trailRoot;

    private float LastShootTime;

    public float LastShootTimePlant;

    public static ShootManager Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Update()
    {
        // mouvement de balancier
        if (Input.GetKey(KeyCode.Space))
        {
            var direction = (rb.transform.position -ForcePoint.position) * 1f;
            rb.AddForce(direction, ForceMode.Acceleration);
        }
    }
    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            audioSource.Play();
            playerAnimator.SetTrigger("Shoot");
            rb.AddExplosionForce(100, ForcePoint.position, 15, 1, ForceMode.Impulse);
            SpawnShootEffect();
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                
                Vector3 direction = GetDirection();

                Debug.DrawRay(BulletSpawnPoint.position, direction, Color.blue, 3f);
                if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, ShootMask))
                {
                    Debug.Log($"raycast hit{hit.collider.name}");
                    Crow hitCrow = (Crow)hit.transform.GetComponent("Crow");
                    if (hitCrow != null)
                    {
                        crowManager.KillCrow(hitCrow);
                        Destroy(hitCrow.gameObject);
                    }

                    // TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity, trailRoot);
                    
                    
                    // StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                    LastShootTime = Time.time;
                }
                // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
                else
                {
                    Debug.Log($"raycast not hit");
                    // TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity, trailRoot);
                    

                    // StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

                    LastShootTime = Time.time;
                }
            }
        }
    }

    public void Plant()
    {
        if (LastShootTimePlant + PlantShootDelay < Time.time)
        {
            // TODO tweak to have other anims
            audioSource.Play();
            playerAnimator.SetTrigger("Shoot");
            rb.AddExplosionForce(100, ForcePoint.position, 15, 1, ForceMode.Impulse);
            SpawnShootEffect();

            Vector3 direction = MouseController.instance.ShootPoint.position - BulletSpawnPoint.position;
            direction.Normalize();

            if (Physics.Raycast(BulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, PlantMask))
            {
                FarmManager.Instance.PlantCrop(hit.point);
            }

            LastShootTimePlant = Time.time;
        }
    }

    public void SpawnShootEffect() 
    {
        ParticleSystem newShootEffect = Instantiate(ShootEffect);
        newShootEffect.transform.parent = BulletSpawnPoint;
        newShootEffect.transform.localPosition = Vector3.zero;
        newShootEffect.transform.LookAt(MouseController.instance.ShootPoint.position);
        newShootEffect.transform.parent = MouseController.instance.World;
        MuzzleFlash.Play();
    }

    private Vector3 GetDirection()
    {
        Vector3 direction =  MouseController.instance.ShootPoint.position - BulletSpawnPoint.position;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;
        //Trail.transform.parent = MouseController.instance.transform;


        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }

        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            Debug.Log("hit");
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
