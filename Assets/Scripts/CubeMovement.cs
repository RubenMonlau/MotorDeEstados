using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 7f; // Fuerza del salto
    private Rigidbody _rb;
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento en los ejes X y Z
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(moveX, _rb.velocity.y, moveZ);
        _rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}