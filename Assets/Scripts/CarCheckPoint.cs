using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckPoint : MonoBehaviour
{
    public static CarCheckPoint instance;

    [Header("충돌 지점")]
    protected bool pointA = true;
    protected bool pointB = true;
    //체크 포인트
    public bool a_one = true;//a지점 우선순위가 1번
    public bool a_two = true;
    public bool b_one = true;//b지점 우선순위가 1번
    public bool b_two = true;

    void Awake() { if (instance == null) { instance = this; } }



}
