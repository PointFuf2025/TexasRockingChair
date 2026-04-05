using NUnit.Framework;
using UnityEngine;

public class CameraMainMenu : MonoBehaviour
{
    public Camera[] allCameras;
    public Camera currentCam;
    int camIndex;

    public float camTimer;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCam = allCameras[0];

    }

    public void ChangeCamera() 
    {
        if (camIndex >= allCameras.Length-1)
        {
            camIndex = 0;
        }
        else
        {
            camIndex++;
        }
        currentCam.gameObject.SetActive(false);
        currentCam = allCameras[camIndex];
        currentCam.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > camTimer) 
        { 
            timer = 0;
            ChangeCamera();
        }
        else 
        {
            timer += Time.deltaTime;
        }
    }
}
