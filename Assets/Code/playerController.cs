using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    private GameObject birOncekiObj;
    public GameObject camera;

    private Rigidbody rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float controlSpeed;

    private int DegdigiPlatform;

    private Vector3 cameraNewPos;

    public float minimum = -1.88194F;
    public float maximum = -2.775f;
    static float t = 0.0f;

    public GameObject text;

    public int paraSayisi = 0;
    private bool igroneNextCollision;
    private Collider test;
    
    public Animation ezilmeAnim;

    private Animator anim;
    
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;

    float touchPosX;

    private Text skorYazi;

    private platformCreatorManager pCM;
    private float bironcekiY;
    private GameObject ikiOnceki;
	private SphereCollider sphere;
    public GameObject olmePlatformu;

    // Start is called before the first frame update
    void Start()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        rb = GetComponent<Rigidbody>();
		sphere = GetComponent<SphereCollider>();
        pCM = GameObject.Find("platformCreatorManager").GetComponent<platformCreatorManager>();
        anim = GetComponent<Animator>();
        skorYazi = GameObject.Find("Yazi").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
        //     transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
        //
        // }
        // else
        // {
        //     touchPosX = 0;
        // }

        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(0, 0, swerveAmount);
        
        
		if (bironcekiY < transform.position.y)
        {
            sphere.isTrigger = true;
        }
        else
        {
            sphere.isTrigger = false;
        }
		bironcekiY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        rb.WakeUp();
        skorYazi.text = "PARA: " + paraSayisi.ToString() +"   Platform : " +DegdigiPlatform.ToString();
        

        
        //t += 0.5f * Time.deltaTime;

        if (transform.position.y < -10f)
        {
            Debug.Log("�LD�N SEN");
        }

        
        //camera.transform.position = new Vector3(camera.transform.position.x, Mathf.Lerp(camera.transform.position.y, cameraNewPos.y, t), camera.transform.position.z);


        camera.transform.position = new Vector3(
            0.76f,
            Mathf.Clamp(transform.position.y, transform.position.y + -10.87f, transform.position.y + 30.3f),
            -10.06f);


        float maxSpeed = 10;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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
            //ezilmeAnim.Play("ezilme");
            anim.SetTrigger("EzilmeTrigger");

            rb.velocity = new Vector3(1, 50 * Time.deltaTime * 7, 500f);
            igroneNextCollision = true;
            Invoke("AllowCollision", .4f);

            audioSource.PlayOneShot(clip, 1f);
            if (birOncekiObj != coll.collider.gameObject)
            {
                //cameraNewPos = new Vector3(camera.transform.position.x, camera.transform.position.y + 2f, camera.transform.position.z);
                DegdigiPlatform++;


                pCM.yeniPlatfromEkle();

                if(ikiOnceki != null)
                {
                    var test = Instantiate(olmePlatformu, ikiOnceki.transform.position, Quaternion.identity);
					Destroy(test, 15f);
                    Destroy(ikiOnceki);

                    
                }
                ikiOnceki = birOncekiObj;

            }


            birOncekiObj = coll.collider.gameObject;
        }

       if(coll.collider.CompareTag("olmePlatformu"))
        {
            Destroy(GameObject.Find("Player"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // test = other;
        // Invoke("TrigeriDegistir", .075f);
        if (other.CompareTag("PARA"))
        {
            paraSayisi++;
            Destroy(other.gameObject);
        }
    }
/*
    private void TrigeriDegistir()
    {
        test.GetComponent<MeshCollider>().isTrigger = false;
    }*/


    private void AllowCollision()
    {
       
        igroneNextCollision = false;
    }
}
