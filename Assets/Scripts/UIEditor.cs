using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIEditor : MonoBehaviour
{

    [SerializeField] InputField nameVehicule;
    [SerializeField] GameObject vehicule;
    [SerializeField] GameObject UIPrefab;
    [SerializeField] GameObject canvasEditor;
    [SerializeField] GameObject canvasMenu;
    [SerializeField] GameObject canvasSelection;
    [SerializeField] public Transform listingContainer;
    List<Vehicule> listVehicule = new List<Vehicule>();

    string jsonString;
    List<GameObject> tempObject = new List<GameObject>();
    List<GameObject> listSave = new List<GameObject>();
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gameManager;
        loadFronJSON();
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SavePrefab()
    {
        
        if (nameVehicule.text != "")
        {
            gm.objectNoSave.Clear();
            Vehicule vehiculeJson = new Vehicule();
            vehiculeJson.name = nameVehicule.text;
            listVehicule.Add(vehiculeJson);
            for(int i =0; i < listVehicule.Count;i++)
            {
                if(i != listVehicule.Count-1)
                jsonString += JsonUtility.ToJson(listVehicule[i])+",";
                else
                jsonString += JsonUtility.ToJson(listVehicule[i]);
            }
            Debug.Log(jsonString);
            File.WriteAllText("Assets/Resources/Vehicule.json", jsonString);
            PrefabUtility.SaveAsPrefabAssetAndConnect(vehicule, "Assets/Prefabs/Vehicule/" + nameVehicule.text + ".prefab", InteractionMode.UserAction);
            PrefabUtility.UnpackPrefabInstance(vehicule, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        }
    }

    public void NewVehicule()
    {
        canvasEditor.gameObject.SetActive(true);
        canvasMenu.gameObject.SetActive(false);
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


    public void LoadOnVehicule(string name)
    {
       

        GameObject go = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Vehicule/" + name + ".prefab");
        GameObject loadObject = Instantiate(go);

     


        for (int i = 0; i < vehicule.transform.childCount; i++)
        {
            Destroy(vehicule.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < go.transform.childCount; i++)
        {
         
        tempObject.Add(loadObject.transform.GetChild(i).gameObject);
            
        }

        for (int i = 0; i < tempObject.Count; i++)
        {
          tempObject[i].transform.SetParent(vehicule.transform);
            
        }




        Destroy(loadObject);
        canvasSelection.SetActive(false);
        canvasEditor.SetActive(true);
        this.gameObject.GetComponent<EditorMode>().enabled = true;
        tempObject.Clear();
    }


    public void loadFronJSON()
    {
        //string path = Application.dataPath + "/Resources/Skins.json";
        if (listVehicule.Count != 0)
        {
            listVehicule.Clear();
        }
        var JSONString = Resources.Load<TextAsset>("Vehicule");
        //Debug.Log(JSONString);
        listVehicule = JsonHelper.getJsonArray<Vehicule>(JSONString.text);
       




    }

    public void QuitSelection()
    {
        canvasSelection.SetActive(false);
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
       
        File.WriteAllText("Assets/Resources/Vehicule.json", json);
        loadFronJSON();
        LoadVehicule();
        
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
