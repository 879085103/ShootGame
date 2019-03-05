using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPause : MonoBehaviour {

    public GameObject m_canvas_paused;
    public GameObject m_canvas_back;

    void Start()
    {
        Button buttonpause = m_canvas_paused.transform.Find("ButtonPause").GetComponent<Button>();
        Button buttonback = m_canvas_paused.transform.Find("ButtonBack").GetComponent<Button>();
        Button buttonrestart = m_canvas_paused.transform.Find("ButtonPauseMenu").transform.Find("ButtonRestart").GetComponent<Button>();
        Button buttonresume = m_canvas_paused.transform.Find("ButtonPauseMenu").transform.Find("ButtonResume").GetComponent<Button>();
        Button buttonyes = m_canvas_back.transform.Find("Button_Yes").GetComponent<Button>();
        Button buttonno = m_canvas_back.transform.Find("Button_No").GetComponent<Button>();

        //点击暂停的回调事件
        buttonpause.onClick.AddListener(delegate ()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed = 0; //Player停止
            Time.timeScale = 0;
            m_canvas_paused.transform.Find("ButtonPauseMenu").gameObject.SetActive(true);//显示暂停界面
        });

        //点击退出的回调事件
        buttonback.onClick.AddListener(delegate ()
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed = 0; //Player停止
            Time.timeScale = 0;
            m_canvas_back.gameObject.SetActive(true); //显示退出界面
        });

        //点击返回游戏的回调事件
        buttonresume.onClick.AddListener(delegate ()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed = 0.2f;
            Time.timeScale = 1;
            m_canvas_paused.transform.Find("ButtonPauseMenu").gameObject.SetActive(false);//隐藏暂停界面
        });

        //点击重新开始的回调事件
        buttonrestart.onClick.AddListener(delegate ()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed = 0.2f;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新开始关卡
            m_canvas_paused.transform.Find("ButtonPauseMenu").gameObject.SetActive(false);//隐藏暂停界面
        });

        buttonyes.onClick.AddListener(delegate ()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed = 0.2f;
            Time.timeScale = 1;
            SceneManager.LoadScene("start");//返回开始界面
        });

        buttonno.onClick.AddListener(delegate ()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().speed = 0.2f;
            Time.timeScale = 1;
            m_canvas_back.gameObject.SetActive(false); //隐藏退出界面
        });

    }


        // Update is called once per frame
        void Update () {
		
	}
}
