using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour {

    public Enemy m_enemy;
	// Use this for initialization
	void Start () {
        m_enemy = this.GetComponentInParent<Enemy>();//获得Enemy脚本
	}
	
    void OnBecameVisible()
    {
        m_enemy.m_isActiv = true;
        m_enemy.m_renderer = this.GetComponent<Renderer>();
    }
}
