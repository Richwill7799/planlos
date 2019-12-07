using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    [Header("Jump Variables")]
    public float zCoord; //offset from floor
    public float startJumpHeight, gravity, cooldown; //start jump boost, downward force, time in sec between jumps
    [Space(10)]
    
    public float speed;

    private float zCoordDelta; //movement up or down
    private float lastJump;
    
    // Start is called before the first frame update
    void Start() {
        zCoord = 0;
        
    }

    // Update is called once per frame
    void Update() {
        
        //Jump stuff ####
        if (zCoord <= 0 && Input.GetAxis("Jump") >= 0.8 && lastJump+cooldown<Time.time) {
            Jump();
        }
        
        var position = transform.position;
        if (zCoord > 0) {
            if (zCoordDelta > 0) {
                zCoordDelta -= gravity;
            }
            else zCoordDelta -= gravity * 2;

            zCoord += zCoordDelta;
            position += Vector3.up * zCoordDelta;
        }

        if (zCoord <= 0) {
            position -= Vector3.up*zCoord;
            zCoord = 0;
            zCoordDelta = 0;
        }
        //Jump stuff end ####
        
        position += speed * Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.right +
                             speed * Input.GetAxis("Vertical") * Time.deltaTime * Vector3.up;// + Vector3.up * zCoord;


        if (Input.GetAxis("Horizontal") > 0) 
            transform.localScale = new Vector3( 0.2f, 0.2f, 1);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);
        transform.position = position;

    }

    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(0.04f, -0.52f - zCoord, 0);
    }

    public bool IsAir()
    {
        return zCoord > 0;
    }
    
    private void Jump() {
        zCoordDelta = startJumpHeight;
        transform.position += Vector3.up * zCoordDelta;
        zCoord += zCoordDelta;

        lastJump = Time.time;
    }
}
