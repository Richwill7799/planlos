using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public MovementScript tofollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!tofollow.Dying)
            transform.position = Vector3.Lerp(transform.position, tofollow.GetPosition() + new Vector3(0, 0.5f, -tofollow.GetPosition().z-10), 0.03f);
    }
}
