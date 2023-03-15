using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector3 PlayerMouseInput;
    private float xRot;
    private bool onGround;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody rb;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sens;
    [SerializeField] private float JumpForce;


    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

    }



    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sens;

        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);


        transform.Rotate(0.0f, PlayerMouseInput.x * Sens, 0.0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
    }
}
