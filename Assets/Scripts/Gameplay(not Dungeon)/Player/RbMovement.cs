using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=ixM2W2tPn6c
    [SerializeField] private  float speed = 5f;
    [SerializeField] private  Rigidbody2D rb;
    [SerializeField] private Animator animator  = default;
    [SerializeField] private  Vector2 movement;
    

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // remove diagonal movement 
        if (movement.x != 0)
        {
            movement.y = 0;
        }
        
        //Animation
        animator.SetFloat("Horizontal", movement.x); //set Horizontal Parameter equal to the actual movement 
        animator.SetFloat("Vertical", movement.y); //set Vertical Parameter equal to the actual movement 
        animator.SetFloat("Speed", movement.SqrMagnitude()); // sqrt of abs of vector = speed     }
    }

    void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * speed * Time.fixedDeltaTime ));
    }
    
   
}
