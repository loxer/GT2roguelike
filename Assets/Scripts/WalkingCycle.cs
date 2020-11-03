using UnityEngine;
using System.Collections;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class WalkingCycle : MonoBehaviour
{

    // 2D Character Movement
    // https://www.youtube.com/watch?v=whzomFgjT50
    // tile based movement 
    // https://www.youtube.com/watch?v=_Pm16a18zy8
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D rb = default;

    [SerializeField]
    private Animator animator = default;
    private Vector2 movement;
    private bool isMoving;

    void FixedUpdate()
    {
        if (!isMoving)
        {
            // Input 
            movement.x = Input.GetAxisRaw("Horizontal"); // number between -1 and 1
            movement.y = Input.GetAxisRaw("Vertical"); // number between -1 and 1

            // remove diagonal movement 
            if (movement.x != 0)
            {
                movement.y = 0;
            }
            
            if (movement != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += movement.x;
                targetPos.y += movement.y;

                StartCoroutine(Move(targetPos));
            }
            
            //Animation
            animator.SetFloat("Horizontal", movement.x); //set Horizontal Parameter equal to the actual movement 
            animator.SetFloat("Vertical", movement.y); //set Vertical Parameter equal to the actual movement 
            animator.SetFloat("Speed", movement.SqrMagnitude()); // sqrt of abs of vector = speed 

        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
    
}
