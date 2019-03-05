using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TestButton : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        Button start_button = this.transform.Find("Button_gamestart").GetComponent<Button>();
        start_button.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("level1");
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
