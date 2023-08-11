using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIGameGongGaoComponent : Entity, IAwake
    {

        public GameObject BtnClose;
        public GameObject LabGongGao;
        public GameObject LabUnderGongGao;
        public GameObject GongGaoSpr;
    }
}