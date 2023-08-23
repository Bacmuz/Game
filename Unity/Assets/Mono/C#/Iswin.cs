using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Iswin : MonoBehaviour
{
    public GameObject win;
    void Update()
    {
        GameObject[] enemies= GameObject.FindGameObjectsWithTag("Enemy");
        int enemyCount = enemies.Length;
        if( enemyCount == 0)
        {
            //SceneManager.LoadScene("Win");    //胜利场景跳转
            win.GetComponent<CanvasGroup>().alpha = 1;

            Debug.Log("获得了胜利");
        }
    }
}
