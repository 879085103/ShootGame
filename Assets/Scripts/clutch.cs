using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clutch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F) && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_mp >= 100)//按下F键且能量满
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_mp = 0;//放完必杀技后能量清零
            GameManager.Instance.ChangeEnergy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_mp);//更新能量条

            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            //Debug.Log(enemy.Length);
            if (enemy != null)
            {
                foreach (GameObject it in enemy)//遍历敌人数组
                {
                    GameManager.Instance.AddSource(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().m_point);//更新UI上的分数
                    Destroy(it);//消灭敌人
                }
            }
        }
    }
   
}
