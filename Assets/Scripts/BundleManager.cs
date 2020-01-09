using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class BundleManager : MonoBehaviour
{

    AssetBundle bundleModule;
    AssetBundle bundleAutre;
    AssetBundle bundleScene;

    public static BundleManager bundleManager;

    public List<GameObject> modulesCorps = new List<GameObject>();
    public List<GameObject> modulesArmes = new List<GameObject>();
    public List<GameObject> modulesRoues = new List<GameObject>();
    public List<GameObject> modulesBoucliers = new List<GameObject>();
    public List<string> listScenes = new List<string>();
    static bool alreadyLoad = false;
    public string jsonFile;

    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath + "", "module");
    string filePath2 = System.IO.Path.Combine(Application.streamingAssetsPath + "", "autre");
    string filePath3 = System.IO.Path.Combine(Application.streamingAssetsPath + "", "scene");

    void Awake()
    {
        if (bundleManager == null)
        {

            bundleManager = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this.gameObject);
        }

      //  DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
            bundleModule = AssetBundle.LoadFromFile(filePath);
            bundleAutre = AssetBundle.LoadFromFile(filePath2);
            bundleScene = AssetBundle.LoadFromFile(filePath3);
       

       
        for (int i = 0; i < bundleModule.GetAllAssetNames().Length; i++)
        {
            if (bundleModule.GetAllAssetNames()[i].Contains("corps"))
            {
                GameObject myTempGo = bundleModule.LoadAsset<GameObject>(bundleModule.GetAllAssetNames()[i]);
                modulesCorps.Add(myTempGo);
            }
            if (bundleModule.GetAllAssetNames()[i].Contains("arme"))
            {
                GameObject myTempGo = bundleModule.LoadAsset<GameObject>(bundleModule.GetAllAssetNames()[i]);
                modulesArmes.Add(myTempGo);
            }
            if (bundleModule.GetAllAssetNames()[i].Contains("roue"))
            {
                GameObject myTempGo = bundleModule.LoadAsset<GameObject>(bundleModule.GetAllAssetNames()[i]);
                modulesRoues.Add(myTempGo);
            }
        }

        for (int i = 0; i < bundleScene.GetAllScenePaths().Length; i++)
        {

            listScenes.Add(bundleScene.GetAllScenePaths()[i]);
        }


     
    }
        // Update is called once per frame
        void Update()
    {

      
    }
}
