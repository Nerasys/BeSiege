using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMode : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] GameObject construction;
    [SerializeField] GameObject vehicule;
    bool IsBuilding = false;
    GameObject gameObjectBuild;
    GameObject collisionSet;


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
              
                if (!IsBuilding)
                {
                    gameObjectBuild = Instantiate(construction);
                    gameObjectBuild.GetComponent<MeshRenderer>().material = mat;
                    gameObjectBuild.transform.SetParent(vehicule.transform);
                    gameObjectBuild.transform.position = hit.collider.gameObject.transform.position;
                
               
                    
                    IsBuilding = true;
                    collisionSet = hit.collider.gameObject;
                }
                else
                {
                    gameObjectBuild.transform.position = hit.collider.gameObject.transform.position;
                    collisionSet = hit.collider.gameObject;
                }

                float dot = Vector3.Dot(gameObjectBuild.transform.forward, hit.normal);
                Debug.Log(dot);
            }
        }



        if (IsBuilding && Input.GetMouseButtonDown(0))
        {
            Debug.Log(collisionSet.name);
            Destroy(collisionSet);
            IsBuilding = false;
        }


    }

}



