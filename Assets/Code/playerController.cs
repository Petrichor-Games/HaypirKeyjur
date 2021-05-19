using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
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
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
