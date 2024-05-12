using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Transform target;
    void Update()
    {
        // Seguimiento del enemigo hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // C�lculo de la direcci�n hacia el jugador
        Vector3 direction = (target.position - transform.position).normalized;

        // C�lculo de la rotaci�n hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Aplicar la rotaci�n
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}
