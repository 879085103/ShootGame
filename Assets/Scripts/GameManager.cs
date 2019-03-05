using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float enemy_speed = 1.0f;//控制敌人速度

    public static GameManager Instance;//静态实例
    public GameObject boss;//Boss物体
    protected bool is_boss = false;//boss是否出现

    public Transform m_canvas_main;//显示分数的UI界面
    public Transform m_canvas_gameover;//游戏失败的UI界面
    public Transform m_canvas_levels;//关卡等级的UI界面
    public Transform m_canvas_speedup;//加速的UI界面
    public Transform m_canvas_Go;//显示Go的UI界面
    public Transform m_canvas_life; //显示生命条的UI界面
    public Transform m_canvas_mp; //显示能量的UI界面
    public Transform m_canvas_pause; //显示暂停的UI界面
    public Transform m_canvas_back; //显示退出的UI界面

    public Transform DustStore;//沙尘暴效果

    public Text m_text_score;//得分UI文本
    public Text m_text_bestscore;//最高分UI文本
    public Text m_text_life;//生命UI文本

    public int m_score = 0;//得分
    public static int m_hiscore = 0; //最高分
    protected Player m_player;//主角

    public AudioClip m_musicClip;//背景音乐
    protected AudioSource m_Audio; //声音源

    public bool paused = false;//控制暂停
    public bool exit = false; //控制退出

	void Start () {
        Instance = this;
        m_Audio = this.gameObject.AddComponent<AudioSource>();//添加音效组件
        m_Audio.clip = m_musicClip; //指定音效
        m_Audio.loop = true; //循环播放
        m_Audio.Play();//播放音乐
        //获取主角
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //获得UI控件
        m_text_score = m_canvas_main.transform.Find("Text_score").GetComponent<Text>();
        m_text_bestscore = m_canvas_main.transform.Find("Text_bestscore").GetComponent<Text>();
        m_text_life = m_canvas_life.transform.Find("Text_life").GetComponent<Text>();

        m_text_score.text = string.Format("分数   {0}", m_score);//初始化UI分数
        m_text_bestscore.text = string.Format("最高分   {0}", m_hiscore);//初始化UI最高分
        m_text_life.text = string.Format("{0} / 100", m_player.m_life * 10);//初始化UI生命值

        Button restart_button = m_canvas_gameover.transform.Find("Button_restart").GetComponent<Button>();//获取重新开始游戏按钮

        restart_button.onClick.AddListener(delegate () //按键事件回调
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新开始关卡
 
        });

        m_canvas_gameover.gameObject.SetActive(false); //默认隐藏游戏失败UI
        m_canvas_speedup.gameObject.SetActive(false);//默认隐藏提示加速UI
        m_canvas_Go.gameObject.SetActive(false);//默认隐藏Go提示
        m_canvas_pause.transform.Find("ButtonPauseMenu").gameObject.SetActive(false); //默认隐藏暂停提示
        m_canvas_back.gameObject.SetActive(false); //默认隐藏退出提示

        //开场ReadyGo动画
        BeginAction();
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && exit == false)//按P暂停
        {
            paused = !paused;
            if (paused == true)
            {
                Button buttonpause = m_canvas_pause.transform.Find("ButtonPause").GetComponent<Button>(); //获取暂停按钮
                buttonpause.onClick.Invoke();//调用暂停按键的监听方法
            }
            else
            {
                Button buttonresume = m_canvas_pause.transform.Find("ButtonPauseMenu").transform.Find("ButtonResume").GetComponent<Button>(); //获取回到游戏按钮
                buttonresume.onClick.Invoke(); //调用回到游戏的监听方法
            }
        }

        if(Input.GetKeyDown(KeyCode.B) && paused == false) //按B返回开始界面
        {
            exit = !exit;
            if (exit == true)
            {
                Button buttonback = m_canvas_pause.transform.Find("ButtonBack").GetComponent<Button>();
                buttonback.onClick.Invoke();
            }
            else
            {
                Button buttonno = m_canvas_back.transform.Find("Button_No").GetComponent<Button>();
                buttonno.onClick.Invoke();
            }
            
        }

        if(Input.GetKeyDown(KeyCode.Y) && exit == true)
        {
            Button buttonyes = m_canvas_back.transform.Find("Button_Yes").GetComponent<Button>();
            buttonyes.onClick.Invoke();
        }
        
        if(Input.GetKeyDown(KeyCode.R) && paused == true) //当暂停时按R重新开始游戏
        {
            Button buttonrestart = m_canvas_pause.transform.Find("ButtonPauseMenu").transform.Find("ButtonRestart").GetComponent<Button>();
            buttonrestart.onClick.Invoke();
        }
        
        if (GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemySpawn>().cnt == 5 && is_boss == false)
        {
            is_boss = true;
            boss.SetActive(true); // Boss显形
            boss.GetComponent<Boss>().enabled = true;
        }
    }

    public void HideLevels()//隐藏游戏关卡等级UI
    {
        m_canvas_levels.gameObject.SetActive(false);
    }

    public void DisplayGo()
    {
        m_canvas_Go.gameObject.SetActive(true);
    }
    public void HideGo()//隐藏GoUI
    {
        m_canvas_Go.gameObject.SetActive(false);
    }
	
    public void BeginAction()
    {
        Invoke("HideLevels", 0.5f);//开局0.5s后调用HideLevels函数
        Invoke("DisplayGo", 0.5f);//开局0.5s后显示Go
        Invoke("HideGo", 1.0f);//开局1.0s后隐藏Go
    }

    //增加分数
    public void AddSource(int point)
    {


        m_score += point;
        //更新高分记录
        if (m_hiscore < m_score)
            m_hiscore = m_score;
        m_text_score.text = string.Format("分数   {0}", m_score);
        m_text_bestscore.text = string.Format("最高分  {0}", m_hiscore);
    }



    //改变生命值UI显示
    public void ChangeLife(float life)
    {
        m_text_life.text = string.Format("{0} / 100", m_player.m_life * 10);
  
        m_canvas_life.transform.Find("Slider").GetComponent<Slider>().value = life * 10;//血条减少
        
        if(life <= 0)
        {
            life = 0;
            m_canvas_gameover.gameObject.SetActive(true);//如果生命值为0，显示游戏失败UI
            
            //m_canvas_life.transform.Find("Fill").GetComponent<Image>().color = Color.clear;
        }
    }

    //改变能量UI显示
    public void ChangeEnergy(float energy)
    {
        m_canvas_mp.transform.Find("Slider").GetComponent<Slider>().value = energy;//能量增加
    }
}
