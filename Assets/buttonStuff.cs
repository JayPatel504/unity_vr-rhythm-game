using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonStuff : MonoBehaviour
{
    private int buttonWidth = 100;
    private int buttonHeight = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    private void OnGUI() {
        if(GUI.Button(new Rect((Screen.width - buttonWidth)/2, (Screen.height - buttonHeight)/2, buttonWidth, buttonHeight), "Start!")){
            
            SceneManager.LoadScene(sceneName:"dontknow");
        }
    }
}
