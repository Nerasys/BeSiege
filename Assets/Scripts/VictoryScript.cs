using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{

    [SerializeField] GameObject Victoire;
    // Start is called before the first frame update
    void Start()
    {
        Victoire = GameObject.Find("Victory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
           Victoire.transform.GetChild(0).gameObject.SetActive(true);
        
    }
}
