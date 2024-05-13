using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject BulletOrigin; // Punto de inicio de la Instancia
    public GameObject BulletPrefab; // Prefab de la Bala
    public float BulletSpeed; // Velocidad de Bala
    private bool isShootable;
    public GameObject AimAnimation;

    // Update is called once per frame
    void Update()
    {
        //bool finishedAnimation = 
        if (Input.GetButton("Aim"))
        {
            isShootable = true;
        } else
        {
            isShootable = false;
        }

        if (isShootable)
        {
            if (Input.GetButtonDown("Shoot"))
            {
                //Instanciar el Prefab de la Bala en BulletOrigin
                GameObject TempBullet = Instantiate(BulletPrefab, BulletOrigin.transform.position, BulletOrigin.transform.rotation) as GameObject;
                //Obtener Rigidbody para agregar fuerza
                Rigidbody rb = TempBullet.GetComponent<Rigidbody>();
                //Agregar fuerza a la bala
                rb.AddForce(transform.forward * BulletSpeed);
                //Destruir bala después de un tiempo
                Destroy(TempBullet, 5.0f);
            }
        }        
    }
}
