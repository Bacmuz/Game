using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemycontrol : MonoBehaviour
{
    private GameObject player;    //玩家
    public int Hp = 1;    //敌人血量
    public  float Speed = 0.8f;    //敌人速度
    public float timer = 0;    //计时器
    private NavMeshAgent nav;    //寻路组件
    public GameObject fire;    //法球预设体
    private Transform _firePosition;    //发射位置
    public float shellSpeed = 2f;    //法球速度
    public float cd = 3;    //攻击冷却
    public AudioClip audioClip;   //拖入音频源
    public float dietimer = 0;    //死亡后计时
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        _firePosition = transform.Find("FirePosition");    //得到发射的位置
    }

    void Update()
    {
        timer += Time.deltaTime;    //计时器
        if(Hp <= 0 || transform.position.y < -1)
        {
            dietimer  += Time.deltaTime;    //计时器计时
            transform.gameObject.GetComponent<Animator>().Play("die");    //播放死亡动画
            if(dietimer >=1.5)
            {
                Destroy(gameObject);    //1.5s后销毁游戏物体
            }
        }
        else
        {
            float dis = Vector3.Distance(player.transform.position, transform.position);    //获取玩家和敌人之间的距离
            float l = Vector3.Distance(new Vector3(-1.7f, -0.5f, 4.7f), transform.position);    //获取敌人和定点之间的距离
            if(dis < 6f && dis > 4f && transform.position.y > -1)
            {
                transform.LookAt(player.transform);    //距离小于2会看向玩家
                //transform.Translate(Vector3.forward * Speed * Time.deltaTime);    //朝前走去
                nav.SetDestination(player.transform.position);
                transform.gameObject.GetComponent<Animator>().Play("run");    //播放运动动画
            }
            else if(dis <= 4f)
            {
                //transform.LookAt(new Vector3(-1.7f, -0.5f, 4.7f));    //看向定义的位置
                //transform.Translate(Vector3.forward * Speed  * Time.deltaTime);    //朝前走去
                //nav.SetDestination(Vector3.forward);
                //transform.gameObject.GetComponent<Animator>().Play("run");    //播放运动动画
                
                if(timer >= cd)    //如果冷却好了
                {
                transform.LookAt(player.transform);    //朝向玩家
                GameObject fireInstance = GameObject.Instantiate(fire, _firePosition.position, _firePosition.rotation);    //生成法球
                AudioSource.PlayClipAtPoint(audioClip,transform.position);    //产生音效
                transform.gameObject.GetComponent<Animator>().Play("atk");    //播放攻击动画
                //fireInstance.GetComponent<Rigidbody>().velocity = fireInstance.transform.forward * shellSpeed;    //给法球施加速度
                timer = 0;
                }
            }
            else
            {
                transform.gameObject.GetComponent<Animator>().Play("idle");    //播放静止动画
            }
        }
    }

    public void Gethit()
    {
        Hp -= 1;
    }
}