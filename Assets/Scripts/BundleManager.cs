using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleManager : MonoBehaviour
{

    AssetBundle bundleModule;

    public static BundleManager bundleManager;

    public List<GameObject> modulesCorps = new List<GameObject>();
    public List<GameObject> modulesArmes = new List<GameObject>();
    public List<GameObject> modulesRoues = new List<GameObject>();

    void Awake()
    {
        if (BundleManager.bundleManager == null)
        {
            BundleManager.bundleManager = this;
        }
        else
        {
            if (BundleManager.bundleManager != this)
            {
                Destroy(BundleManager.bundleManager.gameObject);
                BundleManager.bundleManager = this;
            }

        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath + "", "module");
        bundleModule = AssetBundle.LoadFromFile(filePath);

        for(int i = 0; i< bundleModule.GetAllAssetNames().Length; i++)
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
      



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
