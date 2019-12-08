using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour {
    [Header("Jump Variables")]
    public float zCoord; //offset from floor
    public float startJumpHeight, gravity, cooldown; //start jump boost, downward force, time in sec between jumps

    [Space(10)]
    
    public float speed;


    private bool dying;
    
    private ParticleSystem skatesParticles;
    private ParticleSystem.EmissionModule skatesParticlesEmission;
    private float zCoordDelta; //movement up or down
    private float lastJump;
    private Rigidbody2D rigidbody;
    private float deathtime;
    
    // Start is called before the first frame update
    void Start() {
        zCoord = 0;
        dying = false;

        skatesParticles = this.GetComponentInChildren<ParticleSystem>();
        skatesParticlesEmission = skatesParticles.emission;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
        if (dying) {
            skatesParticlesEmission.enabled = false;
            rigidbody.drag += 100*Time.deltaTime;

            if (Time.time-deathtime > 4f) {
                SceneManager.LoadScene("Menu");
                //TODO add defeat screen here, change if clause parameters
                //Debug.Log("DEFEAT");
            }
                
            transform.position += Vector3.back * Time.deltaTime + Vector3.down*Time.deltaTime;
            return;
        }
        //Debug.Log("dying? "+Time.deltaTime);
        //Jump stuff ####
        if (zCoord <= 0 && Input.GetAxis("Jump") >= 0.8 && lastJump+cooldown<Time.time) {
            Jump();
        }
        
        //var position = transform.position;
        if (zCoord > 0) {
            if (zCoordDelta > 0) {
                zCoordDelta -= gravity*Time.deltaTime;
            }
            else zCoordDelta -= gravity * 2*Time.deltaTime;

            zCoord += zCoordDelta*Time.deltaTime;
            transform.position += zCoordDelta * Time.deltaTime * Vector3.up;
        }

        if (zCoord <= 0) {
            skatesParticlesEmission.enabled = true;
            transform.position -= Vector3.up*zCoord;
            zCoord = 0;
            zCoordDelta = 0;
        }
        //Jump stuff end ####
        
        //transform.position += speed * Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.right +
            //                 speed * Input.GetAxis("Vertical") * Time.deltaTime * Vector3.up;// + Vector3.up * zCoord;


        if (Input.GetAxis("Horizontal") > 0) 
            transform.localScale = new Vector3( 1f, 1f, 1);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector3(-1f, 1f, 1);
        rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
        
        //transform.position = position;

    }

    public Vector3 GetPosition()
    {
        //return transform.position + new Vector3(0.04f, -0.52f - zCoord, 0);
        return transform.position + new Vector3(0,  - zCoord, 0);
    }

    public bool IsAir()
    {
        return zCoord > 0||dying;
    }

    public void Death() {
        dying = true;
        deathtime = Time.time;
        //Debug.Log("HI IM DEAD!");
        //transform.position += Vector3.back;
    }

    public bool Dying => dying;


    private void Jump() {
        
        skatesParticlesEmission.enabled = false;
        zCoordDelta = startJumpHeight;
        transform.position += zCoordDelta * Time.deltaTime * Vector3.up;
        zCoord += zCoordDelta*Time.deltaTime;

        lastJump = Time.time;
    }
}
