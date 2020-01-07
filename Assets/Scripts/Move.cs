using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Vertical") * speed;
       // float vertical = Input.GetAxis("Horizontal") * speed*-1;
        GetComponent<Rigidbody>().AddForce(transform.forward * horizontal);
       // GetComponent<Rigidbody>().AddForce(Vector3.forward * vertical);
    }
}
