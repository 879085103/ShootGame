using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public Transform m_enemy;
    protected Transform m_transform;
    
    private bool []flag = new bool [5];//判断是否进入加速
    public int cnt = 1;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < 5; i++)
        {
            flag[i] = false;
        }//把flag数组初始化为false
        m_transform = GetComponent<Transform>();

        StartCoroutine("SpawnEnemy");
        
    }
	
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().m_score / 100 ==  cnt && flag[cnt - 1] == false && cnt <= 4)//如果得分等于100
        {
            StopCoroutine("SpawnEnemy");//停止生成敌人
            if (cnt == 4) cnt++;
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().m_score >= 100 * cnt  && flag[cnt - 1] == false &&cnt <= 3)//游戏界面里没有敌人存在
        {
            
            flag[cnt - 1] = true;
            if (cnt == 3)
            {
                Instantiate(GameManager.Instance.DustStore, this.transform.position, Quaternion.identity);
            }//300分后沙尘暴出现
            Invoke("ShowSpeedUp", 2.0f);
            cnt++;
        }

    }

    public void ShowSpeedUp()  //显示提示加速UI
    {
        GameManager.Instance.m_canvas_speedup.gameObject.SetActive(true);
        
        Invoke("HideSpeedUp", 2.0f); //2s后隐藏提示加速UI
        StartCoroutine("SpawnEnemy"); //重新生成敌人
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().enemy_speed += 0.25f;//每次调用速度增加1
    }

    public void HideSpeedUp()
    {
        GameManager.Instance.m_canvas_speedup.gameObject.SetActive(false);
    }

    IEnumerator SpawnEnemy()//使用协程创建敌人
    {
        yield return new WaitForSeconds(Random.Range(7, 10));//每隔5到15s生成一个敌人
        Instantiate(m_enemy, m_transform.position, Quaternion.identity);
        StartCoroutine("SpawnEnemy");//循环

    }
	
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "item.png", true);
    }
}
