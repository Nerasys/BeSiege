﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMode : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Material buildFinish;
    [SerializeField] Material roueFinish;
    int index = 1;

    [SerializeField] GameObject[] construction;
    [SerializeField] GameObject vehicule;
    bool IsModuleChoose = false;
    bool isConstruction = false;
    bool isModuleConstruction = false;
    bool isRoue = false;
    bool isSupr = false;
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
                     if(!isSupr)gameObjectBuild.SetActive(true);
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

            if (isSupr)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit2;
                    if (Physics.Raycast(ray2, out hit2, 100))
                    {
                        if (hit2.collider.name.Contains("Module"))
                        {
                            Destroy(hit2.collider.gameObject);

                        }

                    }

                }

            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {

                    GameObject build = Instantiate(gameObjectBuild);
                    gm.objectNoSave.Add(build);

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
                   
                    index = vehicule.transform.childCount;
                    build.GetComponent<Rigidbody>().isKinematic = false;
                    build.transform.SetParent(vehicule.transform);
                    build.AddComponent<FixedJoint>();
                    build.GetComponent<FixedJoint>().connectedBody = collisionSet.transform.parent.gameObject.GetComponent<Rigidbody>();
                    build.GetComponent<IndexJoint>().index = index;
                    build.GetComponent<IndexJoint>().indexJoint = collisionSet.transform.parent.gameObject.GetComponent<IndexJoint>().index;
                    index++;
                    // collisionSet.SetActive(false);

                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (collisionSet.gameObject.name.Equals("Haut") || collisionSet.gameObject.name.Equals("Bas"))
                        gameObjectBuild.gameObject.transform.Rotate(Vector3.up, 90.0f);
                    else
                        gameObjectBuild.gameObject.transform.Rotate(Vector3.right, 90.0f);

                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (collisionSet.gameObject.name.Equals("Haut") || collisionSet.gameObject.name.Equals("Bas"))
                        gameObjectBuild.gameObject.transform.Rotate(Vector3.right, 90.0f);
                    else
                        gameObjectBuild.gameObject.transform.Rotate(Vector3.forward, 90.0f);

                }


            }
            if (Input.GetKeyDown(KeyCode.S))
            {

                isSupr = !isSupr;
                if (isSupr)
                {
                    gameObjectBuild.SetActive(false);

                }
                else
                {
                    gameObjectBuild.SetActive(true);
                }

            }

        }
    }
    Vector3 tempPos = Vector3.zero;
    Quaternion tempRot = Quaternion.identity;
    public void SetButtonConstruction(int index)
    {
        isSupr = false;
        if (gameObjectBuild)
        {
            tempPos = gameObjectBuild.transform.position;
            tempRot = gameObject.transform.rotation;
        }
        Destroy(gameObjectBuild);
        gameObjectBuild = Instantiate(construction[index], tempPos, tempRot);

        if (index == 2)
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



