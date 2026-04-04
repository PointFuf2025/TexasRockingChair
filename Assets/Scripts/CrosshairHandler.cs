using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    public GameObject target;
    Camera main;

    public Vector3 scaler;

    public void Start()
    {
        main = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = main.WorldToScreenPoint(target.transform.position);
        Vector3 translatePosition = main.ScreenToWorldPoint(targetPosition);
        transform.position = translatePosition + scaler;
    }
}
