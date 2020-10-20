using UnityEngine;

public class WalkingCycle : MonoBehaviour
{

    // thank you to Brackeys
    // https://www.youtube.com/watch?v=whzomFgjT50
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D rb = default;

    [SerializeField]
    private Animator animator = default;
    private Vector2 movement;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    void Update()
    {
        // Input 
        movement.x = Input.GetAxis("Horizontal"); // number between -1 and 1
        movement.y = Input.GetAxis("Vertical"); // number between -1 and 1

        //Animation
        animator.SetFloat("Horizontal", movement.x); //set Horizontal Parameter equal to the actual movement 
        animator.SetFloat("Vertical", movement.y); //set Vertical Parameter equal to the actual movement 
        animator.SetFloat("Speed", movement.SqrMagnitude()); // sqrt of abs of vector = speed 
    }

    private void FixedUpdate()
    {
        //Movement 
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
