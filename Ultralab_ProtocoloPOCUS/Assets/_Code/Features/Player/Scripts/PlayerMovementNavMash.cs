using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovementNavMash : MonoBehaviour
{
    private NavMeshAgent agent;
    private Camera mainCamera;
    private Ultralab_InputSystem_Actions controls;

    [SerializeField] private Animator animator;

    void Awake()
    {
        controls = new Ultralab_InputSystem_Actions();

        controls.UI.Click.performed += ctx => MoveToCursor();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        agent.updateRotation = false; 
        agent.updateUpAxis = false;
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (agent.velocity.sqrMagnitude > 0.01f) 
        {
            Vector2 moveDir = agent.velocity.normalized;
            
            animator.SetFloat("horizontal", moveDir.x);
            animator.SetFloat("vertical", moveDir.y);
        }
    }

    private void MoveToCursor()
    {
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        
        Vector3 targetPath = mainCamera.ScreenToWorldPoint(pointerPosition);
        targetPath.z = 0;

        agent.SetDestination(targetPath);
    }
}