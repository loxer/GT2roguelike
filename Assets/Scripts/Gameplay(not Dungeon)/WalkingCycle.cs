using System;
using UnityEngine;
using System.Collections;
using System.Numerics;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class WalkingCycle : MonoBehaviour
{

    // 2D Character Movement
    // https://www.youtube.com/watch?v=whzomFgjT50
    // tile based movement 
    // https://www.youtube.com/watch?v=_Pm16a18zy8
    [SerializeField] private float moveSpeed = 5f;
    //[SerializeField] private Rigidbody2D rb = default;
   // [SerializeField] private GameObject enemy;
    [SerializeField] private Animator animator  = default;
    [SerializeField] private Slider battery = default;
    
    private Vector2 movement;
    private bool isMoving;

    /* void Start()
     {
         Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
     }*/

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Akkufresser"))
        {
            Debug.Log("-5");
            battery.value -= 5f;
        }
        if (other.CompareTag("Steckdose"))
        {
            Debug.Log("+5");
            battery.value += 5f;
        }

        if (battery.value <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
        Debug.Log("You Lost :( ");
    }
}
