using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    private GameObject birOncekiObj;
    public GameObject camera;

    private Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float controlSpeed;

    private Vector3 cameraNewPos;
    
    public float minimum = -1.88194F;
    public float maximum =  -2.775f;
    static float t = 0.0f;
    private bool igroneNextCollision;
    private Collider test;

    float touchPosX;

    private platformCreatorManager pCM;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pCM = GameObject.Find("platformCreatorManager").GetComponent<platformCreatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.WakeUp();

        if (Input.GetMouseButton(0))
        {
            touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
            transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
           
        }
        //t += 0.5f * Time.deltaTime;

        if (transform.position.y<-10f)
        {
            Debug.Log("�LD�N SEN");
        }

        //camera.transform.position = new Vector3(camera.transform.position.x, Mathf.Lerp(camera.transform.position.y, cameraNewPos.y, t), camera.transform.position.z);
        
        
        camera.transform.position = new Vector3(
            0.76f,
            Mathf.Clamp(transform.position.y, transform.position.y+ -10.87f, transform.position.y+ 30.3f),
            -10.06f);
        
        
        
        // if (t > 0.5f)
        // {
        //     t = 0.0f;
        // }

        
        //float maxSpeed = 10;
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("platform"))
        {
            if (igroneNextCollision)
            {
                return;
            }
            
            //rb.velocity = Vector3.zero;
            //rb.AddForce(Vector3.up * 13, ForceMode.Impulse);

            rb.velocity = new Vector3(1, 50 * Time.deltaTime * 7, 500f);
            igroneNextCollision = true;
            Invoke("AllowCollision", .4f);
            
            audioSource.PlayOneShot(clip, 1f);
            if (birOncekiObj!=coll.collider.gameObject)
            {
                //cameraNewPos = new Vector3(camera.transform.position.x, camera.transform.position.y + 2f, camera.transform.position.z);


                pCM.yeniPlatfromEkle();
            }

            
            birOncekiObj = coll.collider.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        test = other;
        Invoke("TrigeriDegistir", .075f);
    }

    private void TrigeriDegistir()
    {
        test.GetComponent<MeshCollider>().isTrigger = false;
    }


    private void AllowCollision()
    {
        igroneNextCollision = false;
    }
}
