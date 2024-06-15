using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{

    private Vector3 targetPosition; // Posición objetivo
    [SerializeField] private Animator playerAnimatorBotton;
    [SerializeField] private Animator playerAnimatorTop;

    public GameObject crossHair;
    public GameObject arrowPrefab;
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
        
        //playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.deltaTime);
        //transform.position = transform.position + moveInput * Time.deltaTime;

        bool isAiming = Input.GetButton("Fire1");
        playerAnimatorTop.SetBool("Aim", Input.GetButton("Fire1"));
        
        if (isAiming)
        {
            crossHair.SetActive(true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            mousePosition.z = 0.0f;

            crossHair.transform.localPosition = aimDirection * 6.0f;
            //crossHair.transform.localPosition = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime);

            playerAnimatorTop.SetFloat("AimHorizontal", aimDirection.x);
            playerAnimatorTop.SetFloat("AimVertical", aimDirection.y);
            playerAnimatorTop.SetFloat("AimMagnitude", aimDirection.sqrMagnitude);

            // Convertir las coordenadas del cursor a coordenadas del mundo
            //targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //targetPosition = Input.mousePosition;
            //targetPosition.z = 0f; // Asegurar que la posición Z sea 0 en un entorno 2D
        }

        if(Input.GetMouseButtonUp(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 aimDirection = (mousePosition - transform.position).normalized;

            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = aimDirection * 10.0f;
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
            crossHair.SetActive(false);
        }
        transform.position = transform.position + moveInput * Time.deltaTime;
        // Mover el objeto suavemente hacia la posición objetivo
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
