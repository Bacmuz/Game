using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//创建虚拟摇杆
public class ScrollCircle : ScrollRect
{
    private float mradius = 0f;    //半径
    private const float dis = 0.5f;    //距离
    private GameObject hero;    //获取角色
    public float x=0;
    public float y=0;
    private Vector3 v2;

    protected override void Start()
    {
        base.Start();
        //能移动的半径 = 摇杆的宽 * Dis
        mradius = content.sizeDelta.x * dis;
        hero = GameObject.FindGameObjectWithTag("Player");    //获取角色
	}

    //当拖动摇杆时触发
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        //获取摇杆和锚点之间的相对位置
        Vector2 contentPosition = content.anchoredPosition;    //获取锚点指向轴点的向量
        //判断摇杆轴点和锚点之间的距离是否大于我们限制的半径
        if (contentPosition.magnitude > mradius)
        {
            //设置摇杆可移动的最远距离
            contentPosition = contentPosition.normalized * mradius;    //normalized是把当前向量的模长变为1，就是把向量变为单位向量
            SetContentAnchoredPosition(contentPosition);    //设置内容的锚点位置
        }
        v2 = content.anchoredPosition;    //获取锚点指向轴点的向量，如果没有拖动摇杆那就是（0,0）
        //x和y的作用是让角色跟随摇杆转动，在后面的角色控制脚本中会用到
        x = v2.x;
        y = v2.y;       
    }

    //当结束拖动摇杆时触发
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        //当摇杆不被使用时，x,y置0，这样做是让角色停止移动
        x = 0;
        y = 0;
    }
}