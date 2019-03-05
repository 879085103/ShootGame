using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : MonoBehaviour {

    public float m_speed = 10;//子弹飞行速度
    public float m_liveTime = 1.5f;//生存时间
    public float m_power = 1.0f;//威力
    protected Transform m_transform;



	void Start () {
        m_transform = this.transform;
        Destroy(this.gameObject, m_liveTime);//生存时间后自动销毁
	}
	

	void Update () {
        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
	}


    //子弹与敌人相碰的时候销毁自身
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Enemy") != 0)
            return;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_mp += 2.5f;//子弹每次击中敌人加2.5能量
            GameManager.Instance.ChangeEnergy(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().m_mp);//更新能量条

            Destroy(this.gameObject);
        }
    }
}
