using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerController ps;

    [SerializeField] float dashSpeed = 40f;
    [SerializeField] float dashTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        ps.canLook = false;
        ps.canMove = false;

        while (Time.time < startTime + dashTime)
        {
            ps.controller.Move((transform.forward + transform.up * -(ps.cameraPitch/90f)) * dashSpeed * Time.deltaTime);
            ps.velocityY = 0.0f;

            yield return null;
        }

        ps.canLook = true;
        ps.canMove = true;
    }
}
