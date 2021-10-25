using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    Animator anim;
    Collider weaponcol;
    public float weaponcd = 0.6f;
    public float abilitycd = 0.6f;
    float offcd;

    [SerializeField] Transform rangedAttack;
    [SerializeField] Transform rangedSpawn;

    void Awake()
    {
        anim = GetComponent<Animator>();
        weaponcol = GetComponent<CapsuleCollider>();
        weaponcol.enabled = false;
        offcd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > offcd)
        {
            if (Input.GetButton("Fire1"))
            {
                anim.SetBool("attacking", true);

                offcd = Time.time + weaponcd;
                weaponcol.enabled = true;
            }
            else if (Input.GetButton("Fire2"))
            {
                anim.SetBool("attacking", true);
                Instantiate(rangedAttack, rangedSpawn.position, rangedSpawn.rotation);

                offcd = Time.time + abilitycd;
            }
        }
        else
        {
            anim.SetBool("attacking", false);
            if(Time.time > offcd - 0.3f)
                weaponcol.enabled = false;
        }
    }
}
