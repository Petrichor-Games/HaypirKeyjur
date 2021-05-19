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

    float touchPosX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
            transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
        }

        if (transform.position.y<-10f)
        {
            Debug.Log("ÖLDÜN SEN");
        }

    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("platform"))
        {
            rb.AddForce(Vector3.up * 13, ForceMode.Impulse);
            audioSource.PlayOneShot(clip, 1f);
            if (birOncekiObj!=coll.collider.gameObject)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 2f, camera.transform.position.z);
            }

            
            birOncekiObj = coll.collider.gameObject;
        }
    }
}
