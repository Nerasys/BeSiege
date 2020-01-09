using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    public bool CameraMode = false;
    public bool EditorMode = false;

    public List<GameObject> objectNoSave = new List<GameObject>();
    public static GameManager gameManager;
    VehiculeGestion vg;
    GameObject uiEditor;
    [SerializeField] GameObject victory;
    GameObject flag;

    public string nameVehicule;
  
    bool victoryPlaced = false;
    void Awake()
    {
        if (gameManager == null)
        {

            gameManager = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        vg = VehiculeGestion.vehiculeGestion;
        
    }



    private void Update()
    {
       

        

  
       
    }


}
