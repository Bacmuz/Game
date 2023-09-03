using ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playercontrol : MonoBehaviour
{
    public float m_speed = 1.2f;  //速度
    public int hp = 1;    //血量
    public float timer = 0;    //计时器
    public float diet = 0;    //死亡后计时
    private ScrollCircle sc;    //获取ScrollCircle
    public bool DieFlag = false;    //判断死亡ui是否打开
    public string WinFlag = "false";    //判断胜利ui是否打开
    private Vector3 v;
    private Rigidbody rb;
    
    void Update()
    {
        if(transform.position.y < -3)
        {
            hp -= 1;
        }
        if(hp<=0 && DieFlag == false)    //如果血量为0或者掉落，则跳转至死亡面板
        {
            transform.gameObject.GetComponent<Animator>().Play("die");
            diet  += Time.deltaTime;    //死亡后计时
            if(diet >= 1.5)
            {
                Die();
            }
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int enemyCount = enemies.Length;
            if (enemyCount == 0 && WinFlag == "false")
            {
                Win();
            }
        }
    }

    void FixedUpdate()
    {
        sc = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<ScrollCircle>();    //获取ScrollCircle
        rb = this.GetComponent<Rigidbody>();    //获取刚体
        if ((sc.x != 0 || sc.y != 0) && hp > 0)    //等于0说明摇杆没有被拉动，所以不要移动和转弯
        {
            v = new Vector3(sc.x, this.transform.position.y, sc.y);    //因为世界坐标中，物体前方的是Z轴，所以我们把ScrollCircle脚本里面的 y 作为角色的Z坐标
            rb.velocity =m_speed * transform.forward;    //让角色往前移动
            this.transform.LookAt(v);    //利用LookAt函数让角色转弯
            transform.gameObject.GetComponent<Animator>().Play("run");    //播放运动动画
        }
        else if(hp <= 0)
        {
        }
        else
        {
            transform.gameObject.GetComponent<Animator>().Play("idle");
        }
    }
    
    public void GetHit()
    {
        hp -= 1;    //受到伤害Hp减1
    }

    public void Die()
    {
        //获取死亡UI
        GameObject Dieprefab = GameLoadAssetsHelp.LoadEffect(LoadAssets_EffectType.Skill, "Die");
        GameObject Diegol = UnityEngine.Object.Instantiate(Dieprefab);
        DieFlag = true;
    }

    public void Win()
    {
        //获取死亡UI
        GameObject Winprefab = GameLoadAssetsHelp.LoadEffect(LoadAssets_EffectType.Skill, "Win");
        GameObject Wingol = UnityEngine.Object.Instantiate(Winprefab);
        WinFlag = "true";
    }
}