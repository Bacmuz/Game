using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHotMessageBox : MonoBehaviour
{
    public GameObject BtnTrue;
    public GameObject BtnFalse;
    public GameObject LabMessage;
    public UnityEvent onEvOk = new UnityEvent();
    public UnityEvent onEvCancel = new UnityEvent();
    // Use this for initialization
    void Start()
    {
        BtnTrue.GetComponent<Button>().onClick.AddListener(()=> OnBtnOk());
        BtnFalse.GetComponent<Button>().onClick.AddListener(() => OnBtnNot());
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void OnBtnOk()
    {
        onEvOk.Invoke();
    }
    public void OnBtnNot()
    {
        onEvCancel.Invoke();
    }
}