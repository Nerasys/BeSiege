using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    public bool CameraMode = false;
    public bool EditorMode = false;

    public List<GameObject> objectNoSave = new List<GameObject>();
    public static GameManager gameManager;

    void Awake()
    {
        if (GameManager.gameManager == null)
        {
            GameManager.gameManager = this;
        }
        else
        {
            if (GameManager.gameManager != this)
            {
                Destroy(GameManager.gameManager.gameObject);
                GameManager.gameManager = this;
            }

        }
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
    
    }


}
