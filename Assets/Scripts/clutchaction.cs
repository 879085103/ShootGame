using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clutchaction : MonoBehaviour {

    public Transform m_explosionFx; //爆炸特效
    protected Transform m_quad;
    //protected GameObject[] enemy = new GameObject[10];//储存敌人

    void Start()
    {
        m_quad = GetComponent<Transform>();
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.F) && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_mp >= 100)
        {
          
            Instantiate(m_explosionFx, m_quad.position, Quaternion.identity);
            
        }
	}
}
