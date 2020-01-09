using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{

    BundleManager bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = BundleManager.bundleManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           GameObject go = Instantiate(bm.modulesAutres[0], transform.GetChild(1).position, Quaternion.identity);
            go.AddComponent<CapsuleCollider>();
            go.AddComponent<Rigidbody>();
            go.GetComponent<Rigidbody>().AddForce(transform.GetChild(0).forward * 500);
            go.AddComponent<Bullet>();
        }
    }
}
