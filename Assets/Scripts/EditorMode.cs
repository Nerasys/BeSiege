using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorMode : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Material buildFinish;
    [SerializeField] Material roueFinish;
    int index = 1;

    [SerializeField] GameObject[] construction;
    [SerializeField] GameObject vehicule;
    public bool IsModuleChoose = false;
    bool isConstruction = false;
    bool isRoue = false;
    bool isSupr = false;
    bool isBoucliers = false;
    bool isWeapon = false;
    // public bool nothingInvoke = false;
    public GameObject gameObjectBuild;
    GameObject collisionSet;
    GameManager gm;
    BundleManager bm;
    public static EditorMode editorMode;
    public string nameVehicule;
    Material goodMaterial;

    void Awake()
    {
        if (EditorMode.editorMode == null)
        {
            EditorMode.editorMode = this;
        }
        else
        {
            if (EditorMode.editorMode != this)
            {
                Destroy(EditorMode.editorMode.gameObject);
                EditorMode.editorMode = this;
            }

        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gameManager;
        bm = BundleManager.bundleManager;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.GetComponent<UIEditor>().nameVehi);
         MoveBlockPreview();
         CommandsEditor();
        if (isSupr)
        {
            SuppBlock();
        }
        else
        {
            CreateBlock();
        }
    }

    public void CommandsEditor()
    {
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


        if (Input.GetKeyDown(KeyCode.S))
        {

            isSupr = !isSupr;
            if (isSupr)
            {
               if(gameObjectBuild) gameObjectBuild.SetActive(false);
            }
            else
            {
                Debug.Log("Je ne suis plus en suppression");
                if (gameObjectBuild) gameObjectBuild.SetActive(true);
            }

        }

       
    }

    public void MoveBlockPreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<Constructable>())
            {
                if (IsModuleChoose)
                {
                    if (!isSupr)
                   
                    collisionSet = hit.collider.gameObject;
                    gameObjectBuild.transform.position = hit.collider.gameObject.transform.position;
                    gameObjectBuild.SetActive(true);
                    if (collisionSet.gameObject.name.Equals("Gauche") || collisionSet.gameObject.name.Equals("Droite"))
                    {
                     //gameObjectBuild.transform.forward = hit.collider.gameObject.transform.right;
                    }
                    else
                    {
                     //  gameObjectBuild.transform.forward = hit.collider.gameObject.transform.forward;
                    }

                }

            }
        }

    }


    public void CreateBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > 228 && Input.mousePosition.x < 829 && Input.mousePosition.y > 128 && Input.mousePosition.y < 550)
            {
                if (gameObjectBuild.activeInHierarchy)
                {



                    if (IsModuleChoose)
                    {
                        GameObject build = Instantiate(gameObjectBuild);
                        build.AddComponent<Rigidbody>();
                        build.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        build.AddComponent<BoxCollider>();
                        build.transform.position = new Vector3(Mathf.Round(build.transform.position.x), Mathf.Round(build.transform.position.y), Mathf.Round(build.transform.position.z));

                        gm.objectNoSave.Add(build);
                        build.GetComponent<MeshRenderer>().material = goodMaterial;

                        gameObjectBuild.SetActive(false);

                        for (int i = 0; i < build.transform.childCount; i++)
                        {
                            build.transform.GetChild(i).gameObject.AddComponent<Constructable>();

                        }

                        index = vehicule.transform.childCount;
                        build.transform.SetParent(vehicule.transform);
                        build.AddComponent<FixedJoint>();
                        build.GetComponent<FixedJoint>().connectedBody = collisionSet.transform.parent.gameObject.GetComponent<Rigidbody>();
                        build.AddComponent<IndexJoint>();
                        build.GetComponent<IndexJoint>().index = index;
                        build.GetComponent<IndexJoint>().indexJoint = collisionSet.transform.parent.gameObject.GetComponent<IndexJoint>().index;

                        index++;
                    }

                }
            }

        }

    }
    public void SuppBlock()
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

    Vector3 tempPos = Vector3.zero;
    Quaternion tempRot = Quaternion.identity;
    public void SetButtonConstruction(int moduleType, int numeroBlock)
    {
        isSupr = false;

        IsModuleChoose = true;
     
        if (gameObjectBuild)
        {
            
            tempPos = gameObjectBuild.transform.position;
            tempRot = gameObject.transform.rotation;
        }
        Destroy(gameObjectBuild);

        isRoue = false;
        isBoucliers = false;
        isWeapon = false;
        switch (moduleType)
        {
            case (int)UIEditor.TypeButton.Corps:
                gameObjectBuild = Instantiate(bm.modulesCorps[numeroBlock], tempPos, tempRot);
                break;

            case (int)UIEditor.TypeButton.Armes:
                gameObjectBuild = Instantiate(bm.modulesArmes[numeroBlock], tempPos, tempRot);
                isWeapon = true;
                break;
            case (int)UIEditor.TypeButton.Roues:
                gameObjectBuild = Instantiate(bm.modulesRoues[numeroBlock], tempPos, tempRot);
                isRoue = true;
                break;
            case (int)UIEditor.TypeButton.Boucliers:
                gameObjectBuild = Instantiate(bm.modulesBoucliers[numeroBlock], tempPos, tempRot);
                isBoucliers = true;
                break;
              
        }
        gameObjectBuild.SetActive(false); ;
        goodMaterial = gameObjectBuild.GetComponent<MeshRenderer>().material;
        gameObjectBuild.GetComponent<MeshRenderer>().material = mat;


    }
}



