using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private ShootManager shootManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("shoot");
            shootManager.Shoot();
        }
    }
}
