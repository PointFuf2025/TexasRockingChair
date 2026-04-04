using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Crow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    GameObject crowVisual;

    [SerializeField]
    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FarmManager.Instance.GetRandomCrop().transform;
        crowVisual = transform.GetChild(0).gameObject;
        crowVisual.transform.localPosition = Vector3.zero;
    }

    public void KillCrow(GameObject _effect) 
    { 
        
        GameObject newPs = Instantiate(_effect);
        newPs.transform.parent = crowVisual.transform;
        newPs.transform.localPosition = Vector3.zero;
        crowVisual.gameObject.SetActive(false);
        newPs.transform.parent = MouseController.instance.World;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
        crowVisual.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
