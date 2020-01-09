using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouclier : MonoBehaviour
{
    // Start is called before the first frame update
    bool isActive;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isActive = !isActive;
        }

        if (isActive)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
