using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //public float m_speed = 1;//速度
    public float m_life = 10f;//生命
    protected float m_rotSpeed = 30;//旋转速度
    public int m_point = 10;//被消灭时获得的分数
    protected Transform m_enemy;

    internal Renderer m_renderer;//模型渲染组件
    internal bool m_isActiv = false;//是否激活

    public Transform m_explosionFx; //爆炸特效

    public Transform rocket; //子弹prefab
    protected float fireTimer = 1.0f; //射击计时器

    void Start () {
        m_enemy = GetComponent<Transform>();//获取enemy的transform组件
        m_renderer = GetComponent<Renderer>();//获取模渲染组件
	}
	
    void OnBecameVisible() //如果模型进入屏幕
    {
        m_isActiv = true;
    }
	
	void Update () {
        UpdateMove();
        if( m_isActiv && !this.m_renderer.isVisible)//如果移动到屏幕外
        {
            Destroy(this.gameObject);
        }
        //Debug.Log(m_isActiv);
        //Debug.Log(this.m_renderer.isVisible);
	}

    //为了扩展功能，UpdateMove是一个虚函数
    //敌人在向前飞行时，还会左右迂回移动
    protected virtual void UpdateMove()
    {

        //左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        //前进
        m_enemy.Translate(new Vector3(rx, 0, -GameManager.Instance.enemy_speed * Time.deltaTime));
        if (GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemySpawn>().cnt == 4)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                fireTimer = 1.0f ;//每m_fireTimer秒射击一次
                Instantiate(rocket, m_enemy.position, Quaternion.Euler(new Vector3(1, 0, 1)));//创建子弹
            }
        }
    }
        
    void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("PlayerRocket") == 0)//如果碰到主角子弹
        {
            Rockets rockets = other.GetComponent<Rockets>();//获取子弹上的Rockets组件
            if(rockets != null)
            {
                m_life -= rockets.m_power;//减少生命
                if(m_life <= 0)
                {
                    GameManager.Instance.AddSource(m_point);//更新UI上的分数
                    //播放爆炸特效
                    Instantiate(m_explosionFx, m_enemy.position, Quaternion.identity);
                    Destroy(this.gameObject); //自我销毁
                }
            }
        }
        else if(other.tag.CompareTo("Player") == 0)//如果碰到主角飞机
        {
            m_life = 0;
            Instantiate(m_explosionFx, m_enemy.position, Quaternion.identity);
            Destroy(this.gameObject);//自我销毁
        }
    }
}
