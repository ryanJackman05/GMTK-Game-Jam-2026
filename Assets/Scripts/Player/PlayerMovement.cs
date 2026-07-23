using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 moveVector;
    
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        
        moveVector = move;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = (moveVector * speed);
        if (moveVector.magnitude > 0){
            // TODO animator.SetBool("walking", true);
        }
        
    }
}
