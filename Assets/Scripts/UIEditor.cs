using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEditor : MonoBehaviour
{
    public enum TypeButton
    {
    Corps,
    Armes,
    Roues,
    Boucliers,
    Nothing,
    Size
    }





    [SerializeField] InputField nameVehicule;
    [SerializeField] GameObject vehicule;
    [SerializeField] GameObject UIPrefab;
    [SerializeField] GameObject UIPrefabButton;
    [SerializeField] GameObject UIPrefabLevel;

    [SerializeField] GameObject UICanvasSelectionModule;
    [SerializeField] GameObject canvasEditor;
    [SerializeField] GameObject canvasMenu;
    [SerializeField] GameObject canvasSelection;
    [SerializeField] GameObject canvasLevel;

    [SerializeField] public Transform listingContainer;
    [SerializeField] public Transform listingContainerLevel;
    [SerializeField] public Transform listingContainerButton;
    List<Vehicule> listVehicule = new List<Vehicule>();
    bool alreadyHere = false;

    [SerializeField] public GameObject[] objectsTemp;
    string jsonString;
    List<GameObject> tempObject = new List<GameObject>();
    List<GameObject> listSave = new List<GameObject>();
    List<GameObject> listSaveLevel = new List<GameObject>();

    List<GameObject> buttonModule = new List<GameObject>();
    GameManager gm;
    EditorMode em;
    BundleManager bm;
    GameObject bundleManager;
    public string nameVehi;
    [SerializeField] Sprite sprite2DButton;
    [SerializeField] Font FontButton;
    // Start is called before the first frame update
    void Start()
    {

      
        em = EditorMode.editorMode;
        gm = GameManager.gameManager;
        bm = BundleManager.bundleManager;
        vehicule = GameObject.Find("Vehicule");
       
    }

    // Update is called once per frame



    public void SavePrefab()
    {
        jsonString = null;
        gm.objectNoSave.Clear();

        if (nameVehicule.text != "")
        {
            
            Vehicule vehiculeJson = new Vehicule();
            vehiculeJson.name = nameVehicule.text;
           
            for (int i = 0; i < vehicule.transform.childCount; i++)
            {
                
                Debug.Log("Je rentre");
                if (vehicule.transform.GetChild(i).name.Contains("Noyau"))
                {
                    vehiculeJson.blocks.Add(0);
                }
                if (vehicule.transform.GetChild(i).name.Contains("Corps"))
                {
                    vehiculeJson.blocks.Add(1);
                }
                if (vehicule.transform.GetChild(i).name.Contains("Arme"))
                {
                    vehiculeJson.blocks.Add(2);
                }
                if (vehicule.transform.GetChild(i).name.Contains("Roue"))
                {
                    vehiculeJson.blocks.Add(3);
                }
                Debug.Log("Positions");
                vehiculeJson.positions.Add(vehicule.transform.GetChild(i).position);
                Vector4 temp = new Vector4();
                temp.x = vehicule.transform.GetChild(i).rotation.x;
                temp.y = vehicule.transform.GetChild(i).rotation.y;
                temp.z = vehicule.transform.GetChild(i).rotation.z;
                temp.w = vehicule.transform.GetChild(i).rotation.w;
                vehiculeJson.quaternions.Add(temp);
                vehiculeJson.index.Add(vehicule.transform.GetChild(i).gameObject.GetComponent<IndexJoint>().index);
                vehiculeJson.indexJoint.Add(vehicule.transform.GetChild(i).gameObject.GetComponent<IndexJoint>().indexJoint);
                nameVehi = nameVehicule.text;
            }


            for (int i = 0; i < listVehicule.Count; i++)
            {

                if (listVehicule[i].name.Equals(nameVehicule.text))
                {
                    alreadyHere = true;
                    listVehicule[i] = vehiculeJson;


                }
             
            }
            if (!alreadyHere)
            {
                listVehicule.Add(vehiculeJson);
                alreadyHere = false;
            }

            for(int i =0; i < listVehicule.Count;i++)
            {
                if(i != listVehicule.Count-1)
                jsonString += JsonUtility.ToJson(listVehicule[i])+",";
                else
                jsonString += JsonUtility.ToJson(listVehicule[i]);
            }
            Debug.Log(jsonString);
            using (StreamWriter sw= new StreamWriter(Application.streamingAssetsPath + "/Vehicule.json"))
            {
                sw.Write(jsonString);

            }
            //File.WriteAllText(bm.jsonFile, jsonString);
           loadFronJSON();
            
         
        }
    }

    public void NewVehicule()
    {
       
        canvasEditor.gameObject.SetActive(true);
        canvasMenu.gameObject.SetActive(false);
        vehicule = GameObject.Find("Vehicule");
        GameObject go = Instantiate(objectsTemp[0]);
        go.transform.SetParent(vehicule.transform);
        this.gameObject.GetComponent<EditorMode>().enabled = true;
        for(int i = 0; i < vehicule.transform.childCount; i++)
        {
            if(i != 0)
            {
                Destroy(vehicule.transform.GetChild(i).gameObject);
            }
        }

    }
    public void LoadVehicule()
    {
        vehicule = GameObject.Find("Vehicule");
        listVehicule.Clear();
        if (listSave.Count != 0)
        {
            for (int i = 0; i < listSave.Count; i++)
            {
                Destroy(listSave[i]);
            }
        }
        loadFronJSON();
       
        canvasMenu.SetActive(false);
        canvasSelection.SetActive(true);
        for (int i = 0; i < listVehicule.Count; i++)
        {
            int temp = i;
            GameObject tempListing = Instantiate(UIPrefab, listingContainer);
            tempListing.transform.GetChild(0).gameObject.GetComponent<Text>().text = listVehicule[i].name;
            tempListing.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => LoadOnVehicule(listVehicule[temp].name));
            tempListing.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => EffacerPrefab(listVehicule[temp].name));
            listSave.Add(tempListing);
        }

    }


    public void LoadLevel()
    {
       
        if (listSaveLevel.Count != 0)
        {
            for (int i = 0; i < listSaveLevel.Count; i++)
            {
                Destroy(listSaveLevel[i]);
            }
        }
        canvasMenu.SetActive(false);
        canvasLevel.SetActive(true);
        for (int i = 0; i < bm.listScenes.Count; i++)
        {
            int temp = i;
            GameObject tempListing = Instantiate(UIPrefabLevel, listingContainerLevel);
            tempListing.transform.GetChild(0).gameObject.GetComponent<Text>().text = bm.listScenes[i];
            tempListing.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => LoadOnLevel(bm.listScenes[temp]));
            listSaveLevel.Add(tempListing);


        }


    }





    public void LoadOnLevel(string path)
    {

       
        if (vehicule.transform.childCount > 0)
        {
            SetJoint();
            vehicule.transform.position = new Vector3(0, 4.5f, 0);
            vehicule.transform.rotation = new Quaternion(vehicule.transform.rotation.x, 90, vehicule.transform.rotation.z, vehicule.transform.rotation.w);
            for (int i = 0; i < vehicule.transform.childCount; i++)
            {
                vehicule.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().useGravity = true;
                vehicule.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                if (vehicule.transform.GetChild(i).gameObject.name.Contains("Roue"))
                {
                    vehicule.transform.GetChild(i).gameObject.AddComponent<Move>();

                }

            }
            SceneManager.LoadScene(path);
        }

    }


  
    public void LoadOnVehicule(string name)
    {
        nameVehi = name;
        for (int i = 0; i < vehicule.transform.childCount; i++)
        {
            Destroy(vehicule.transform.GetChild(i).gameObject);
        }

 
        Debug.Log(listVehicule.Count);
        for (int i = 0; i < listVehicule.Count; i++)
        {
            if (listVehicule[i].name.Equals(name))
            {
               
                for (int j = 0; j < listVehicule[i].blocks.Count; j++)
                {
                    GameObject go = Instantiate(objectsTemp[listVehicule[i].blocks[j]]);
                   // go.gameObject.name = listVehicule[i].name;
                    go.transform.position = listVehicule[i].positions[j];
                    Quaternion qua = new Quaternion(listVehicule[i].quaternions[j].x, listVehicule[i].quaternions[j].y, listVehicule[i].quaternions[j].z, listVehicule[i].quaternions[j].w);
                    go.transform.rotation = qua;
                    go.transform.SetParent(vehicule.transform);

                    if (!go.GetComponent<IndexJoint>())
                        go.AddComponent<IndexJoint>();
                    if (!go.GetComponent<Rigidbody>())
                        go.AddComponent<Rigidbody>();
                    if (!go.GetComponent<BoxCollider>())
                        go.AddComponent<BoxCollider>();

                    go.GetComponent<IndexJoint>().index = listVehicule[i].index[j];
                    go.GetComponent<IndexJoint>().indexJoint = listVehicule[i].indexJoint[j];
                   
                    for (int k = 0; k < go.transform.childCount; k++)
                    {
                        go.transform.GetChild(k).gameObject.AddComponent<Constructable>();
                    }
                    Debug.Log(go.name + " Index : " + go.GetComponent<IndexJoint>().index + " IndexJoint : " + go.GetComponent<IndexJoint>().indexJoint);
                    if (!go.name.Equals("Noyau(Clone)"))
                    {
                        go.gameObject.AddComponent<FixedJoint>();
                   

                    }

                  
                    go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;




                }

            }

        }
  
        if(canvasSelection)canvasSelection.SetActive(false);
        if (canvasEditor) canvasEditor.SetActive(true);
        this.gameObject.GetComponent<EditorMode>().enabled = true;
        tempObject.Clear();
        loadFronJSON();
      

   
        }


    private void SetJoint()
    {
        for (int i = 0; i < vehicule.transform.childCount; i++)
        {
            if (!vehicule.transform.GetChild(i).name.Contains("Noyau"))
            {
                vehicule.transform.GetChild(i).GetComponent<FixedJoint>().connectedBody = vehicule.transform.GetChild(vehicule.transform.GetChild(i).GetComponent<IndexJoint>().indexJoint).gameObject.GetComponent<Rigidbody>();
            }
        }
    }


    public void loadFronJSON()
    {
        
        //string path = Application.dataPath + "/Resources/Skins.json";
        if (listVehicule.Count != 0)
        {
            listVehicule.Clear();
        }
        using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/Vehicule.json"))
        {
            jsonString = sr.ReadToEnd();

        }
        listVehicule = JsonHelper.getJsonArray<Vehicule>(jsonString);

        



    }

    public void QuitSelection()
    {
        canvasSelection.SetActive(false);
        canvasMenu.SetActive(true);
        this.gameObject.GetComponent<EditorMode>().enabled = false;
    }
    public void QuitSelectionLevel()
    {
        canvasLevel.SetActive(false);
        canvasMenu.SetActive(true);
        this.gameObject.GetComponent<EditorMode>().enabled = false;
    }

    public void QuitEdition()
    {
        for(int i = 0; i < gm.objectNoSave.Count; i++)
        {
            Destroy(gm.objectNoSave[i]);
        }
        canvasEditor.SetActive(false);
        canvasMenu.SetActive(true);
        SetJoint();
        this.gameObject.GetComponent<EditorMode>().enabled = false;
    }

    public void EffacerPrefab(string name)
    {
        List<Vehicule> tempListVehicule = new List<Vehicule>();
        string json = null;
        for (int i = 0; i < listVehicule.Count; i++)
        {
            if(listVehicule[i].name != name)
            {
                tempListVehicule.Add(listVehicule[i]);
            }

        }
        for (int i = 0; i < tempListVehicule.Count; i++)
        {
            if (i != tempListVehicule.Count - 1)
                json += JsonUtility.ToJson(listVehicule[i]) + ",";
            else
                json += JsonUtility.ToJson(listVehicule[i]);
        }


        using (StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/Vehicule.json"))
        {
            sw.Write(json);

        }
        LoadVehicule();
        
    }



  
    public void SetButtonModule(int indexModule)
    {
        for(int i = 0; i < buttonModule.Count; i++)
        {
            Destroy(buttonModule[i]);
        }


        if(indexModule == (int)TypeButton.Corps)
        {
            UICanvasSelectionModule.SetActive(true);
            for (int i = 0; i < bm.modulesCorps.Count ; i++)
            {
                int temp = i;
                GameObject tempListing = Instantiate(UIPrefabButton, listingContainerButton);
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = bm.modulesCorps[i].name;
                Debug.Log("Module : " + (int)TypeButton.Corps + " Numero :" + i) ;
                tempListing.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => em.SetButtonConstruction((int)TypeButton.Corps, temp));
                tempListing.transform.GetChild(0).GetComponent<Image>().sprite = sprite2DButton;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().fontSize = 40;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().font = FontButton;
                buttonModule.Add(tempListing);
            }
        }

        if (indexModule == (int)TypeButton.Armes)
        {
            UICanvasSelectionModule.SetActive(true);
            for (int i = 0; i < bm.modulesArmes.Count; i++)
            {
                int temp = i;
                GameObject tempListing = Instantiate(UIPrefabButton, listingContainerButton);
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = bm.modulesArmes[i].name;
                tempListing.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => em.SetButtonConstruction((int)TypeButton.Armes, temp));
                tempListing.transform.GetChild(0).GetComponent<Image>().sprite = sprite2DButton;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().fontSize = 40;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().font = FontButton;
                buttonModule.Add(tempListing);
            }
        }
        if (indexModule == (int)TypeButton.Roues)
        {
            UICanvasSelectionModule.SetActive(true);
            for (int i = 0; i < bm.modulesRoues.Count; i++)
            {
                int temp = i;
                GameObject tempListing = Instantiate(UIPrefabButton, listingContainerButton);
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = bm.modulesRoues[i].name;
                tempListing.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => em.SetButtonConstruction((int)TypeButton.Roues, temp));
                tempListing.transform.GetChild(0).GetComponent<Image>().sprite = sprite2DButton;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().fontSize = 40;
                tempListing.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().font = FontButton;
                buttonModule.Add(tempListing);
            }
        }





        if(indexModule == (int)TypeButton.Nothing)
        {
            UICanvasSelectionModule.SetActive(false);
            if(em.gameObjectBuild)em.gameObjectBuild.SetActive(false);
            em.IsModuleChoose = false;
        }


    }



    public class JsonHelper
    {
        public static List<T> getJsonArray<T>(string json)
        {
          
            string newJson = "{ \"Vehicule\":[ " + json + "]}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);

            return wrapper.Vehicule;
        }

        [System.Serializable]
        public class Wrapper<T>
        {
            public List<T> Vehicule;
        }
    }
}
