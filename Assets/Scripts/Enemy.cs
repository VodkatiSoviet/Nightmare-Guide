using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Enemy : MonoBehaviour
{
    public static Enemy enemy_single {  get; private set; }
    private bool caught_player = false; // 플레이어 캐치 전 = false, 플레이어 캐치 후 = true
    public Transform deathTarget;

    private void Awake()
    {
        if (enemy_single == null)
        {
            enemy_single = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !caught_player)
        {
            caught_player = true;

            other.GetComponent<PlayerController>().DisableInput(); // 컴포넌트 꺼버림 , 입력 조작불가능하게 만듬
            Debug.Log(PlayerMainCamera.camera_single);
            PlayerMainCamera.camera_single.RotateTarget();



        }
    }

}
