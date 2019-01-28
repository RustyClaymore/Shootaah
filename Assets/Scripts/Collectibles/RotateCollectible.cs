using UnityEngine;

public class RotateCollectible : MonoBehaviour
{
    public float rotationSpeed; 
    
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
