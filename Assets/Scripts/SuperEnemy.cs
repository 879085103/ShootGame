using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemy : Enemy {

    public Transform m_rocket; //子弹prefab
    protected float m_fireTimer = 1.0f; //射击计时器
    protected Transform m_player; //主角

    void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");//查找主角
        if(obj != null)
        {
            m_player = obj.transform;
        }
    }


	protected  override void UpdateMove()
    {
        m_fireTimer -= Time.deltaTime;
        if(m_fireTimer <= 0)
        {
            m_fireTimer = 1.0f / GameManager.Instance.enemy_speed;//每m_fireTimer秒射击一次
            if(m_player != null)
            {
                //计算子弹初始方向，使其面对主角
                Vector3 relativePos = m_enemy.position - m_player.position;
                Instantiate(m_rocket, m_enemy.position, Quaternion.LookRotation(relativePos));//创建子弹
              
            }
        }

        m_enemy.Translate(new Vector3(0, 0,-GameManager.Instance.enemy_speed * Time.deltaTime));
    }
}
