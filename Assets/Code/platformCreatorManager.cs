using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCreatorManager : MonoBehaviour
{
    public GameObject ilkPlatform;

    public GameObject platformPrefab;

    private Vector3 birOnceki;
    
    // Start is called before the first frame update
    void Start()
    {
        birOnceki = ilkPlatform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 yeniTransform;
        yeniTransform = new Vector3(Random.Range(-2f, 2f), birOnceki.y + 2f, birOnceki.z);
        Instantiate(platformPrefab, yeniTransform, Quaternion.identity);
        birOnceki = yeniTransform;

    }
}
