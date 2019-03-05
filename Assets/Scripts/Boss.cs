using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

   
	// Use this for initialization
	void Awake () {
        m_life = 100;
 
    }

 


    protected override void UpdateMove()
    {
        if(m_enemy.position.z >= 4.5f)
        m_enemy.Translate(new Vector3(0, 0, GameManager.Instance.enemy_speed * Time.deltaTime * 0.25f));
    }
}
