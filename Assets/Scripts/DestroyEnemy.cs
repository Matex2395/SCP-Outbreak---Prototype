using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private Collider _collider;
    private void OnTriggerEnter(Collider other)
    {
        _collider = other;
        if (_collider.CompareTag("Enemy"))
        {
            Destroy(_collider.gameObject); // Destruir el objeto enemigo
            Destroy(gameObject); // Destruir la bala
        }
        else if (_collider.CompareTag("Room") || _collider.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir la bala
        }
    }
}
