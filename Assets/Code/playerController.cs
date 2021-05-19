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


    float touchPosX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        t += 0.5f * Time.deltaTime;

        if (transform.position.y<-10f)
        {
            Debug.Log("�LD�N SEN");
        }

        camera.transform.position = new Vector3(camera.transform.position.x, Mathf.Lerp(camera.transform.position.y, cameraNewPos.y, t), camera.transform.position.z);
        
        if (t > 0.5f)
        {
            t = 0.0f;
        }

        
        float maxSpeed = 10;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("platform"))
        {
            rb.AddForce(Vector3.up * 13, ForceMode.Impulse);
            audioSource.PlayOneShot(clip, 1f);
            if (birOncekiObj!=coll.collider.gameObject)
            {
                cameraNewPos = new Vector3(camera.transform.position.x, camera.transform.position.y + 2f, camera.transform.position.z);
            }

            
            birOncekiObj = coll.collider.gameObject;
        }
    }
}
