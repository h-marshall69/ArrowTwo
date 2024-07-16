using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    public float MOVEMENT_BASE_SPEED = 1.0f;
    public float ARROW_BASE_SPEED = 1.0f;
    public float AIMING_BASE_PENALTY = 1.0f;
    public float SHOOTING_RECOIL_TIME = 1.0f;
    public float CROSSHAIR_DISTANCE =  1.0f;
    public bool lockPosition;
    

    [Space]
    [Header("Character statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool endOfAiming;
    public bool isAiming;

    public float shootingRecoil = 0;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject crosshair;    

    [Space]
    [Header("Prefabs:")]
    public GameObject arrowPrefab;
    
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
        Aim();
        Shoot();
    }

    void ProcessInput() {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        endOfAiming = Input.GetButtonUp("Fire1");
        isAiming = Input.GetButton("Fire1");
        lockPosition = Input.GetButton("LockPosition");


        if(isAiming || shootingRecoil > 0.0f) {
            movementSpeed *= AIMING_BASE_PENALTY;
        }

        if(endOfAiming) {
            shootingRecoil = SHOOTING_RECOIL_TIME;
        }
        if(shootingRecoil > 0.0f) {
            shootingRecoil -= Time.deltaTime;
        }
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

        if(isAiming) {
            animator.SetFloat("AimingState", 0.5f);
        } else if (shootingRecoil > 0.0f){
            animator.SetFloat("AimingState", 1.0f);
        }else {
            animator.SetFloat("AimingState", 0.0f);
        }
    }

    void Aim() {
        if(movementDirection != Vector2.zero) {
            crosshair.transform.localPosition = movementDirection * CROSSHAIR_DISTANCE;
        }
    }

    void Shoot() {
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();

        if(endOfAiming) {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * ARROW_BASE_SPEED;
            arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
        }
    }
}
