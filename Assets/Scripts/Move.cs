using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 75;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < 1.0f)
        {
            Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
            float horizontal = Input.GetAxis("Vertical") * speed;
            GetComponent<Rigidbody>().AddForce(localForward * horizontal);
        }

    }
}
