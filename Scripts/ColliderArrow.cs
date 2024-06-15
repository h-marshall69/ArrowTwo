using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderArrow : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col) {
        Enemy enemy = col.gameObject.GetComponent<Enemy>(); // Intentar obtener el componente Enemy

        if (enemy != null) // Verificar si se encontró el componente Enemy
        {
            enemy.Die(); // Llamar al método Die del componente Enemy
        }

        //Destroy(gameObject);
    }
}
