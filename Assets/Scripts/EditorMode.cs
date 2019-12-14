using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMode : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] GameObject construction;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<Constructable>())
            {
                

            }

        }
    }
}
