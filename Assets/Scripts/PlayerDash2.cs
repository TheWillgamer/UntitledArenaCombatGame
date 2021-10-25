using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash2 : MonoBehaviour
{
    PlayerController ps;

    [SerializeField] float dashSpeed = 2f;
    [SerializeField] float dashTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        if (ps.targetDir != Vector2.zero)
        {
            Vector2 temp = ps.targetDir;
            temp.Normalize();
            while (Time.time < startTime + dashTime)
            {
                ps.controller.Move((transform.forward * temp.y + transform.right * temp.x) * dashSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
