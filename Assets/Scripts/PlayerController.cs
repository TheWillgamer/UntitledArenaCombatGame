using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform camHolder = null;

    [SerializeField] float mouseSenX = 3.0f;
    [SerializeField] float mouseSenY = 3.0f;
    [SerializeField] float walkSpeed = 8.0f;
    [SerializeField] float backwardModifier = 0.7f;
    [SerializeField] float moveSmoothTimeGround = 0.3f;
    [SerializeField] float moveSmoothTimeAir = 0.3f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float jumpHeight = 3.0f;

    public float cameraPitch = 0.0f;
    public float velocityY = 0.0f;
    float slopejumptime = 0.0f;
    public CharacterController controller = null;

    public Vector2 targetDir = Vector2.zero;
    Vector3 velocity = Vector3.zero;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    float moveSmoothTime = 0.3f;

    public bool canLook = true;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canLook)
        {
            UpdateMouseLook();
        }
        if(canMove)
        {
            UpdateMovement();
        }
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSenY;
        cameraPitch = Mathf.Clamp(cameraPitch, 5.0f, 50.0f);

        camHolder.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSenX);
    }

    void UpdateMovement()
    {
        if (controller.isGrounded)
        {
            velocityY = 0.0f;
            if (Input.GetButton("Jump"))
            {
                velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
                slopejumptime = Time.time + 0.1f;
            }
            moveSmoothTime = moveSmoothTimeGround;
        }
        else
        {
            moveSmoothTime = moveSmoothTimeAir;
        }

        velocityY += gravity * Time.deltaTime;

        targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        RaycastHit hit;
        Physics.Raycast(transform.position, -Vector3.up, out hit);
        if (hit.normal.y < 1 && hit.distance < .17f && Time.time > slopejumptime)
        {
            velocityY -= hit.normal.normalized.y;
        }

        velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        if (currentDir.y < -0.1f)
        {
            velocity = velocity * backwardModifier;
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
