using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCreatorManager : MonoBehaviour
{
    public GameObject ilkPlatform;

    public GameObject platformPrefab;
    
    public GameObject PARA;

    private Vector3 birOnceki;


    private int platformSayisi = 0;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        birOnceki = ilkPlatform.transform.position;

        for (int i = 0; i < 4; i++)
        {
            yeniPlatfromEkle();
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        

    }
    
    public void yeniPlatfromEkle()
    {
        platformSayisi++;
        var yeniTransform = new Vector3(Random.Range(-1.2f, 3f), birOnceki.y + 2f, birOnceki.z);
        var asd = Instantiate(platformPrefab, yeniTransform, Quaternion.identity);
        birOnceki = yeniTransform;
        if (platformSayisi %  5 == 0)
        {
            var test = new Vector3(birOnceki.x, birOnceki.y + 0.5f, birOnceki.z);
            Instantiate(PARA, test, Quaternion.identity);
        }
        Destroy(asd,40f);
    }
}
