using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimCamera : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    public Image Image; 

    void Start()
    {
        Cam1.SetActive(true);
        Cam2.SetActive(false);
        Image.enabled = false; 
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cam2.SetActive(true);
            Cam1.SetActive(false);
            Image.enabled = true; 
        }
        else
        {
            Cam2.SetActive(false);
            Cam1.SetActive(true);
            Image.enabled = false; 
        }
    }
}