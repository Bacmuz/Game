using ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk : MonoBehaviour
{
    private GameObject hero;    //角色
    public float timer = 0f;   //计时器
    public float CD = 3;    //冷却时间
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");    //获取角色
	}

    public void Update()
    {
        timer += Time.deltaTime;    //计时器
    }
    public void atk()
    {
        if(timer >= CD)   //按下空格键释放炸弹
        {
            timer = 0;    //重置CD

            //获取炸弹预设体
            GameObject prefab = GameLoadAssetsHelp.LoadEffect(LoadAssets_EffectType.Skill, "Boom");


            //生成炸弹
            Instantiate(prefab, new Vector3(hero.transform.position.x, hero.transform.position.y + 0.15f, hero.transform.position.z), hero.transform.rotation);
            hero.transform.GetComponent<Animator>().Play("boom");
        }
    }
}
