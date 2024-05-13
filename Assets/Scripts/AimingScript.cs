using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingScript : MonoBehaviour
{
    [SerializeField] public GameObject RightArm;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Aim"))
        {
            RightArm.GetComponent<Animator>().Play("ArmAimingAnimation");
        }

        if (Input.GetButtonUp("Aim"))
        {
            RightArm.GetComponent<Animator>().Play("New State");
        }
    }
}
