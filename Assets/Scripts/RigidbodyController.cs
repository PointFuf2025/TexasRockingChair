using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public Rigidbody claytusRigid;
    public Transform shootPosition;
    public float shootForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            claytusRigid.AddExplosionForce(shootForce, shootPosition.position, 15,1,ForceMode.Impulse);
            
        }
    }
}
