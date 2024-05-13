using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destruir el objeto enemigo
            Destroy(gameObject); // Destruir la bala
        }
        else if (other.CompareTag("Room") || other.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir la bala
        }
    }
}
