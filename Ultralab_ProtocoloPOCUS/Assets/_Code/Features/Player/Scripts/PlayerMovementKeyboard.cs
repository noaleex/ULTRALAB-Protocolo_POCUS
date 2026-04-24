using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementKeyboard : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    Vector2 motionVector;
    Ultralab_InputSystem_Actions controls;
    Animator animator;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        controls = new Ultralab_InputSystem_Actions();

        controls.Player.Move.performed += ctx => motionVector = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => motionVector = Vector2.zero;

        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        Move();
        animator.SetFloat("horizontal", motionVector.x);
        animator.SetFloat("vertical", motionVector.y);
    }

    void Move()
    {
        rigidbody2d.linearVelocity = motionVector * speed;
    }
}