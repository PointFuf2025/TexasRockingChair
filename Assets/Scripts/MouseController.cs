using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public Transform World;
    public Transform CrossHairPivot;
    public float sensX;
    public float sensY;

    float yRotation;
    float xRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CameraLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-50f, 50f);
        World.transform.rotation = Quaternion.Euler(0,yRotation ,0 );
        CrossHairPivot.transform.rotation = Quaternion.Euler(xRotation ,0,0);


    }

    // Update is called once per frame
    void Update()
    {
        CameraLook();
    }
}
