using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{

    GameManager gm;
    [SerializeField] GameObject lookAt;
    [SerializeField] float sensibility;
    [SerializeField] float sensibilityZoom;
    [SerializeField] float orbitDamp;
    float zoomLevel = 4;
    float distance = 0;
    Quaternion rotate;
    Vector3 localRotation;
    public bool CameraMode = false;
    // Start is called before the first frame update
    private void Start()
    {
        gm = GameManager.gameManager;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            distance = new Vector3(lookAt.transform.position.x - transform.position.x, lookAt.transform.position.y - transform.position.y, lookAt.transform.position.z - transform.position.z).magnitude;

            if ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
            {
                localRotation.x += Input.GetAxisRaw("Mouse X") * sensibility * distance;
                localRotation.y -= Input.GetAxisRaw("Mouse Y") * sensibility * distance;

            }

            rotate = Quaternion.Euler(localRotation.y, localRotation.x, 0);

            lookAt.transform.rotation = Quaternion.Lerp(lookAt.transform.rotation, rotate, Time.deltaTime * orbitDamp);


        
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel") * sensibilityZoom;
            zoomLevel -= scroll;
        }
        if (transform.localPosition.z <= zoomLevel - 0.001f || transform.localPosition.z >= zoomLevel + 0.001f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, -zoomLevel, Time.deltaTime * orbitDamp));
        }

    }
}

