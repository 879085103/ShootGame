using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitileScreen : MonoBehaviour {

	
    //响应游戏开始按钮事件
	public void OnButtonGameStart()
    {
        SceneManager.LoadScene("level1");
    }
	
}
