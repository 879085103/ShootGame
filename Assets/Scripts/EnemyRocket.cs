using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rockets {


	void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") != 0)//碰到的不是主角
            return;

        Destroy(this.gameObject);
    }
}
