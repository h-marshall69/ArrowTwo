using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f; // Velocidad a la que se mueve el Cube
    private Vector3 targetPosition; // Posición objetivo
    [SerializeField] private Animator playerAnimatorBotton;
    [SerializeField] private Animator playerAnimatorTop;
    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la posición objetivo con la posición actual del Cube
        targetPosition = transform.position;
        playerAnimatorBotton.GetComponent<Animator>();
        playerAnimatorTop.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;

        playerAnimatorBotton.SetFloat("Horizontal", moveInput.x);
        playerAnimatorBotton.SetFloat("Vertical", moveInput.y);
        playerAnimatorBotton.SetFloat("Magnitude", moveInput.sqrMagnitude);

        playerAnimatorTop.SetFloat("MoveHorizontal", moveInput.x);
        playerAnimatorTop.SetFloat("MoveVertical", moveInput.y);
        playerAnimatorTop.SetFloat("MoveMagnitude", moveInput.sqrMagnitude);

        playerAnimatorTop.SetFloat("AimHorizontal", moveInput.x);
        playerAnimatorTop.SetFloat("AimVertical", moveInput.y);
        playerAnimatorTop.SetFloat("AimMagnitude", moveInput.sqrMagnitude);
        playerAnimatorTop.SetBool("Aim", Input.GetButton("Fire1"));
        
        //playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.deltaTime);
        transform.position = transform.position + moveInput * Time.deltaTime;
        
        if (Input.GetMouseButtonDown(1))
        {
            // Convertir las coordenadas del cursor a coordenadas del mundo
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //targetPosition = Input.mousePosition;
            targetPosition.z = 0f; // Asegurar que la posición Z sea 0 en un entorno 2D
        }

        // Mover el objeto suavemente hacia la posición objetivo
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
