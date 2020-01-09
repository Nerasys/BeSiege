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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            vg.gameObject.transform.position = Vector3.zero;
            vg.gameObject.transform.rotation = new Quaternion(0,0,0,0);
            for (int i = 0; i < vg.gameObject.transform.childCount; i++)
            {
                vg.gameObject.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log(i);
            }

                victoryPlaced = false;

                SceneManager.LoadScene(0);
        }

        Debug.Log(victoryPlaced);

  
        if(SceneManager.GetActiveScene().name != "Construction")
        {
            if (!victoryPlaced)
            {
                flag = GameObject.Find("Flag");
                flag.AddComponent<VictoryScript>();
                victoryPlaced = true;


            }
        }
        else
        {
            victoryPlaced = false;
        }
    }


}
