using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 0.2f;
    private Transform m_player;
    public Transform m_rocket;
    public float m_rocketsTime = 0;//发射频率
    public float m_life = 3;//主角生命值
    public float m_mp = 0;//主角初始能量

    public AudioClip m_shootClip;//声音
    protected AudioSource m_audio;//声音源
    public Transform m_explosionFx; //爆炸特效

    protected Vector3 m_targetPos;//目标位置
    public LayerMask m_inputMask;//鼠标射线碰撞层


	void Start () {
        m_player = GetComponent<Transform>();
        m_audio = this.GetComponent<AudioSource>();
        m_targetPos = this.m_player.position;//初始化目标点位置
	}

   

    // Update is called once per frame
    void Update () {
        //上下左右移动
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (m_player.position.x >= -5.56 && m_player.position.x <= 5.11 && m_player.position.z <= 8.47 && m_player.position.z >= -8.66)//在边界内的话随意运动
        {    
            m_player.Translate(new Vector3(v, 0, -h) * speed);
        }
     
        if (m_player.position.x < -5.56)//如果超出下边界
        {
            if (v <= 0) //不能再往下
            {
                m_player.Translate(new Vector3(v, 0, -h) * speed);
            }
        }
        else if (m_player.position.x > 5.11)//如果超出上边界
        {
            if (v >= 0) //不能再往上
            {
                m_player.Translate(new Vector3(v, 0, -h) * speed);
            }
        }
        else if (m_player.position.z < -8.66)//如果超出左边界
        {
            if (h >= 0) //不能再往左
            {
                m_player.Translate(new Vector3(v, 0, -h) * speed);
            }
        }
        else if (m_player.position.z > 8.47)//如果超出右边界
        {
            if (h <= 0) //不能再往右
            {
                m_player.Translate(new Vector3(v, 0, -h) * speed);
            }
        }

        m_rocketsTime -= Time.deltaTime;
        if(m_rocketsTime <= 0)
        {
            m_rocketsTime = 0.1f;

            //按空格键或鼠标左键发射子弹
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_player.position, m_player.rotation);

                //播放射击声音
                m_audio.PlayOneShot(m_shootClip);
            }
        }
       //MoveTo();
	}

    void MoveTo()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;//获取鼠标屏幕位置
            Ray ray = Camera.main.ScreenPointToRay(ms); //将屏幕位置转化为射线
            RaycastHit hitinfo; //用来记录射线碰撞信息
            if (Physics.Raycast(ray, out hitinfo, 1000, m_inputMask))//如果射中目标，记录碰撞点
            {
                m_targetPos = new Vector3(hitinfo.point.x, 0.24f,hitinfo.point.z);
            }
        }

        //使用Vector3 提供的MoveTowards函数，获得朝目标移动的位置
        Vector3 pos = Vector3.MoveTowards(this.m_player.position, m_targetPos, speed);
        //更新当前位置
        this.m_player.position = pos;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("PlayerRocket") != 0) //如果主角与子弹外的物体相撞
        {
            m_life -= 1; //生命减少
            GameManager.Instance.ChangeLife(m_life);
            if (m_life <= 0)
            {
                //播放爆炸特效
                Instantiate(m_explosionFx, m_player.position, Quaternion.identity);
                Destroy(this.gameObject);//自我销毁
            }
        }
    }
}


