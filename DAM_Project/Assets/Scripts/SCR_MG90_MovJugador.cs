using UnityEngine;
using UnityEngine.InputSystem;

public class SCR_MG90_MovJugador : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] private GameObject palanca1;
    [SerializeField] private GameObject[] aguas;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == palanca1)
        {
            Debug.Log("Se ha activado la palanca1");
            aguas[0].GetComponent<Collider2D>().enabled=true;
            aguas[1].GetComponent<Collider2D>().enabled=true;
            aguas[2].GetComponent<Collider2D>().enabled=true;
        }
    }
}
