using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMode : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Material buildFinish;
    [SerializeField] Material roueFinish;



    [SerializeField] GameObject[] construction;
    [SerializeField] GameObject vehicule;
    bool IsModuleChoose = false;
    bool isConstruction = false;
    bool isModuleConstruction = false;
    bool isRoue = false;
    GameObject gameObjectBuild;
    GameObject collisionSet;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gameManager;
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
                    if (IsModuleChoose)
                    {
                        gameObjectBuild.SetActive(true);
                        collisionSet = hit.collider.gameObject;
                        gameObjectBuild.transform.position = hit.collider.gameObject.transform.position;
                        if (collisionSet.gameObject.name.Equals("Gauche") || collisionSet.gameObject.name.Equals("Droite"))
                        {
                            gameObjectBuild.transform.forward = hit.collider.gameObject.transform.right;
                        }

                        else
                        {
                            gameObjectBuild.transform.forward = hit.collider.gameObject.transform.forward;
                        }


                    }


                }
            
            if (Input.GetMouseButtonDown(0))
            {
               
                GameObject build = Instantiate(gameObjectBuild);
                if (!isRoue)
                {
                    build.GetComponent<MeshRenderer>().material = buildFinish;
                }
                else
                {
                    build.GetComponent<MeshRenderer>().material = roueFinish;

                }
              
                gameObjectBuild.SetActive(false);
               
                    for (int i = 0; i < build.transform.childCount; i++)
                    {
                        build.transform.GetChild(i).gameObject.AddComponent<Constructable>();

                    }

                
                build.GetComponent<Rigidbody>().isKinematic = false;
                build.transform.SetParent(vehicule.transform);
                build.AddComponent<FixedJoint>();
                build.GetComponent<FixedJoint>().connectedBody = collisionSet.transform.parent.gameObject.GetComponent<Rigidbody>();
                Destroy(collisionSet);



            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (collisionSet.gameObject.name.Equals("Haut") || collisionSet.gameObject.name.Equals("Bas"))
                    gameObjectBuild.gameObject.transform.Rotate(Vector3.up, 90.0f);
                else
                    gameObjectBuild.gameObject.transform.Rotate(Vector3.right, 90.0f);



            }
        }
    }
    Vector3 tempPos =  Vector3.zero;
    Quaternion tempRot = Quaternion.identity;
    public void SetButtonConstruction(int index)
    {
        if (gameObjectBuild)
        {
            tempPos = gameObjectBuild.transform.position;
            tempRot = gameObject.transform.rotation;
        }
        Destroy(gameObjectBuild);
        gameObjectBuild = Instantiate(construction[index], tempPos, tempRot);

        if(index == 2)
        {
            isRoue = true;
        }
        else
        {
            isRoue = false;
        }
        IsModuleChoose = true;
        isModuleConstruction = false;
        gameObjectBuild.GetComponent<MeshRenderer>().material = mat;
        gameObjectBuild.GetComponent<Rigidbody>().isKinematic = true;
       
    }
}



