using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavTest : MonoBehaviour {

    public GameObject point;
    private NavMeshAgent nav;
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        point = GameObject.FindGameObjectWithTag("Player");
        nav.SetDestination(point.transform.position);
    }

    // Update is called once per frame
    void Update () {

    }
}