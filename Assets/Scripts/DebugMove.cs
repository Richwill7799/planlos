using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{

    [Range(0, 20)]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
            gameObject.transform.position += Time.deltaTime * speed * Vector3.up;
        if(Input.GetKey(KeyCode.S))
            gameObject.transform.position += Time.deltaTime * speed * Vector3.down;
        if(Input.GetKey(KeyCode.A))
            gameObject.transform.position += Time.deltaTime * speed * Vector3.left;
        if(Input.GetKey(KeyCode.D))
            gameObject.transform.position += Time.deltaTime * speed * Vector3.right;
    }
}
