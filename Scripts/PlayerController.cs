using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    [SerializeField] private Animator topAnimator;
    [SerializeField] private Animator bottonAnimator;

    public GameObject crossHair;
    public GameObject arrowPrefab;

    Vector3 movement;
    Vector3 aim;
    Vector3 mousePosition;
    bool isAiming;
    bool endOfAiming;
    // Start is called before the first frame update
    void Start()
    {
        topAnimator.GetComponent<Animator>();
        bottonAnimator.GetComponent<Animator>();
    }

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        AimAndShoot();
        Animate();
        Move();
    }

    private void ProcessInput() {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        aim = aim + mouseMovement;
        if (aim.magnitude > 1.0f) {
            aim.Normalize();
        }
        isAiming = Input.GetButton("Fire1");
        endOfAiming = Input.GetButtonUp("Fire1");

        if (movement.magnitude > 1.0f) {
            movement.Normalize();
        }
    }

    private void Move() {
        transform.position = transform.position + movement * Time.deltaTime;
    }

    private void Animate() {
        bottonAnimator.SetFloat("Horizontal", movement.x);
        bottonAnimator.SetFloat("Vertical", movement.y);
        bottonAnimator.SetFloat("Magnitude", movement.sqrMagnitude);

        topAnimator.SetFloat("MoveHorizontal", movement.x);
        topAnimator.SetFloat("MoveVertical", movement.y);
        topAnimator.SetFloat("MoveMagnitude", movement.sqrMagnitude);

        topAnimator.SetFloat("AimHorizontal", aim.x);
        topAnimator.SetFloat("AimVertical", aim.y);
        topAnimator.SetFloat("AimMagnitude", aim.sqrMagnitude);
        topAnimator.SetBool("Aim", isAiming);
    }

    private void AimAndShoot() {

        Vector2 shootDirection = new Vector2(aim.x, aim.y);

        if(aim.magnitude > 0.0f) {
            crossHair.transform.localPosition = aim * 0.4f; 
            crossHair.SetActive(true);

            if(endOfAiming) {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootDirection * 3.0f;
                arrow.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, 2.0f);
            }
        } else {
            crossHair.SetActive(false);
        } 
    }
}
