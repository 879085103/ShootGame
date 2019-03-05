using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProcessModel : AssetPostprocessor{

	void OnPostprocessModel(GameObject input)
    {
        //只处理名为“Enemy2b”的模型
        if (!input.name.Contains("Enemy2b"))
            return;
        //设置导入模型的tag
        input.tag = "Enemy";

        //查找碰撞模型
        foreach(Transform obj in input.GetComponentsInChildren<Transform>())
        {
            if (obj.name.CompareTo("col") == 0)
            {
                //取消碰撞模型的显示
                MeshRenderer r = obj.GetComponent<MeshRenderer>();
                r.enabled = false;

                //添加Mesh碰撞体
                obj.gameObject.AddComponent<MeshCollider>();

                //设置碰撞体的tag
                obj.tag = "Enemy";
            }
            else
            {
                obj.gameObject.AddComponent<EnemyRenderer>();//指定Render脚本
            }
        }

        //设置刚体
        Rigidbody rigid = input.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;

        //取得导入模型的相关信息
        ModelImporter importer = assetImporter as ModelImporter;

        //从工程中将该模型读出来
        GameObject tar = (GameObject)AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(GameObject));

        //将这个模型设置为prefab
        GameObject prefab = (GameObject)PrefabUtility.CreatePrefab("Assets/Prefabs/Enemy2c.prefab", tar);

        //为prefab添加声音组件
        prefab.AddComponent<AudioSource>();

        //获得子弹的prefab
        GameObject rocket = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/EnemyRocket.prefab", typeof(GameObject));

        //获得爆炸效果的prefab
        var fx = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/FX/Explosion.prefab", typeof(GameObject));

        //为prefab添加脚本
        SuperEnemy enemy = prefab.AddComponent<SuperEnemy>();

        //设置脚本的默认参数
        enemy.m_life = 10;
        enemy.m_point = 10;
        enemy.m_rocket = rocket.transform;
        enemy.m_explosionFx = fx.transform;
    }
}
