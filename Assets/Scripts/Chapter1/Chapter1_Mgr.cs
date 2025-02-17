using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1_Mgr : MonoBehaviour
{
    public static Chapter1_Mgr instance;

    [Header("플레이어")]
    public GameObject player;

    private void Start()
    {
        if (instance == null) { instance = this; }
    }
}
