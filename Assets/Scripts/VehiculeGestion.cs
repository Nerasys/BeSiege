using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeGestion : MonoBehaviour
{
    // Start is called before the first frame update
    public static VehiculeGestion vehiculeGestion ;
    public string name;
    void Start()
    {
        if (vehiculeGestion == null)
        {

            vehiculeGestion = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
