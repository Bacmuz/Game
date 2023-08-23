using ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk : MonoBehaviour
{
    private GameObject hero;    //角色
    //public GameObject boom;    //炸弹预设体
    public float timer = 0f;   //计时器
    public float CD = 1;    //冷却时间
    //public GameObject prefab;    //炸弹预设体
    void Start()
    {
        //father = GameObject.FindGameObjectWithTag("Unit");    //获取角色父物体
        hero = GameObject.FindGameObjectWithTag("Player");    //获取角色
        //Vector3 posi = Camera.main.WorldToScreenPoint(hero.transform.position);    //将角色的世界坐标转化为屏幕上的坐标
        //posi -= new Vector3(Screen.width / -4, Screen.height /10, 0);    //Screen.width和Screen.height分别代表屏幕的宽和高
        ///this.transform.position = posi;    //设置位置，让其保持在角色右边
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
            /*
                Instantiate(boom, new Vector3(hero.transform.position.x , hero.transform.position.y + 0.14f , hero.transform.position.z),  hero.transform.rotation);    //生成炸弹               
                hero.transform.gameObject.GetComponent<Animator>().Play("boom");    //播放释放炸弹动画
            */

            //获取炸弹预设体
            GameObject prefab = GameLoadAssetsHelp.LoadEffect(LoadAssets_EffectType.Skill, "Boom");


            //生成炸弹
            Instantiate(prefab, new Vector3(hero.transform.position.x, hero.transform.position.y + 0.15f, hero.transform.position.z), hero.transform.rotation);
            hero.transform.GetComponent<Animator>().Play("boom");
            //GameObject gol = UnityEngine.Object.Instantiate(prefab);




        }
    }
}
