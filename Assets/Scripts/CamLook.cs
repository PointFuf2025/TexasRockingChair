using UnityEngine;

public class CamLook : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
