using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempShoot : MonoBehaviour
{
    [SerializeField] Transform rangedAttack;
    float offcd = 0;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > offcd)
        {
            Instantiate(rangedAttack, transform.position + new Vector3(0,0,1.3f), transform.rotation);
            offcd = Time.time + 3f;
        }
    }
}
