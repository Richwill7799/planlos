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
        gameObject.transform.position += Time.deltaTime * speed * Input.GetAxis("Vertical")   * Vector3.up;
        gameObject.transform.position += Time.deltaTime * speed * Input.GetAxis("Horizontal") * Vector3.right;
    }
}
