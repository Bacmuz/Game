using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playercontrol : MonoBehaviour
{
    public GameObject die;    //死亡UI
    public float m_speed = 1.2f;  //速度
    public GameObject bombPrefab;   //炸弹预设体
    public float CD = 1;    //间隔时间
    public int hp = 1;    //血量
    public float timer = 0;    //计时器
    public GameObject Player;    //获取游戏对象
    public float diet = 0;    //死亡后计时
    private ScrollCircle sc;    //获取ScrollCircle
    private Vector3 v;
    private Rigidbody rb;
    
    void Update()
    {
        if(transform.position.y < -1)
        {
            hp -= 1;
        }
        if(hp<=0)    //如果血量为0或者掉落，则跳转至死亡面板
        {
            transform.gameObject.GetComponent<Animator>().Play("die");
            diet  += Time.deltaTime;    //死亡后计时
            if(diet >= 1.5)
            {
            //SceneManager.LoadScene("Die");    //死亡1.5s后跳转死亡界面
            die.GetComponent<CanvasGroup>().alpha = 1;    //死亡1.5s后显示死亡UI
            }
        }
        else
        {
            /*
            float horizontal = Input.GetAxis("Horizontal"); //获取水平轴
            float vertical = Input.GetAxis("Vertical"); //垂直轴
            Vector3 dir = new Vector3(horizontal,0,vertical);   //获取向量
            if(dir != Vector3.zero)    //如果向量不为零，则在运动
            {
                transform.rotation = Quaternion.LookRotation(dir);  //角色朝向向量的方向
                transform.position += dir * m_speed * Time.deltaTime;   //进行移动
                //运动状态，播放移动动画
                transform.gameObject.GetComponent<Animator>().Play("run");
            }
            else
            {
                //静止状态，播放站立动画
                transform.gameObject.GetComponent<Animator>().Play("idle");
            }           
            timer += Time.deltaTime;    //计时器
            if(Input.GetKeyDown (KeyCode.Space)  && timer>=CD)   //按下空格键释放炸弹
                {
                Instantiate(bombPrefab, new Vector3(transform.position.x , transform.position.y + 0.14f , transform.position.z),  transform.rotation);    //生成炸弹
                timer = 0;    //重置CD
                transform.gameObject.GetComponent<Animator>().Play("boom");    //播放释放炸弹动画
            }
            */
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
}