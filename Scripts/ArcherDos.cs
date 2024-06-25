using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using Rewared;

public class ArcherDos : MonoBehaviour
{
    [Header("Input settings:")]
    public int playerId;
    Player player;

    [Space]
    [Header("Character attributes:")]
    public float MOVEMENT_BASE_SPEED = 1.0F;

    [Space]
    [Header("Character statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update(){
        ProcessInput();
        Move();
        Animate();
    }

    void ProcessInput() {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move() {
        rb.velocity = movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
    }

    void Animate() {
        if(movementDirection != Vector2.zero) {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        
        animator.SetFloat("Speed", movementSpeed);
    }
}
