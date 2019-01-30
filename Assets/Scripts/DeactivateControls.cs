using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(!SessionManager.Instance.IsGamePaused && SessionManager.Instance.IsGameStarted);
        }
    }
}
