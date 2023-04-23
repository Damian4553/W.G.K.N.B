using UnityEngine;

public class Player : MonoBehaviour
{

    private Vector3 _playerMovementInput;
    private Vector3 _playerMouseInput;
    private float _xRot;
    private bool _onGround;
    private static Camera _mainCamera;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Rigidbody rb;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float sens;
    [SerializeField] private float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = mainCamera;
        
        _onGround = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    public static Camera GetCamera()
    {
        return _mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        _playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _onGround = true;
        }

    }



    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(_playerMovementInput) * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _onGround = false;
        }
    }

    private void MovePlayerCamera()
    {
        _xRot -= _playerMouseInput.y * sens;

        _xRot = Mathf.Clamp(_xRot, -90.0f, 90.0f);


        transform.Rotate(0.0f, _playerMouseInput.x * sens, 0.0f);
        playerCamera.transform.localRotation = Quaternion.Euler(_xRot, 0.0f, 0.0f);
    }
}
