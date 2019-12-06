using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    public float zCoord;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = transform.position + Vector3.right * Input.GetAxis("Horizontal") +
                             Vector3.up * Input.GetAxis("Vertical") + Vector3.up * zCoord;
        
    }
}
