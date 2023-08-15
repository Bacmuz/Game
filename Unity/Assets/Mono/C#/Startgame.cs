using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startgame : MonoBehaviour
{
   public void onClick()
    {
        SceneManager.LoadScene("Levelselect");  //要切换的场景
    }
}
