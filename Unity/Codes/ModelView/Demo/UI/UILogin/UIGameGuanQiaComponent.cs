using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UI))]
    public class UIGameGuanQiaComponent : Entity, IAwake
    {
        public GameObject BtnLevel01;
        public GameObject BtnLevel02;
        public GameObject BtnLevel03;
        public GameObject BtnLevel04;
        public GameObject BtnLevel05;
        public GameObject BtnLevel06;
        public GameObject BtnLevel07;
        public GameObject BtnLevel08;
        public GameObject BtnLevel09;
        public GameObject CloseBtn;
    }
}
