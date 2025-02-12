using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Maze_Mgr : MonoBehaviour
{
    public static Maze_Mgr instance;

    [Header("Nuber")]
    // [RED=1, ORANGE=2, YELLOW=3, GREEN=4, BLUE=5, PURPLE=6, BLACK=7, WHITE=8, GRAY=9]
    [SerializeField] GameObject[] num_Obj; //머테리얼을 변경할 오브젝트 
    protected int[] num_Answer = new int[4]; // answerKey
    private int[] num = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public int[] answer;
    bool anw_Check = false;
    int check_Point = 0;

    [Header("Panel")]//패널 체크
    public int panel_Check=0;

    private void Start()
    {
        if (instance == null) { instance = this; }
        Make_Answer();
        
    }
    private void Make_Answer()
    {
        //AnswerKey 생성
        System.Random rand = new System.Random();
        var randomized = num.OrderBy(item => rand.Next());

        foreach (var value in randomized)
        {
            if (check_Point == 4)
            {
                check_Point = 0;
                break;
            }
            num_Answer[check_Point] = value;
     
            check_Point++;
        }
        //정답키 설정후 키에 따른 오브젝트 색상 변경
        Clear_Num();
    }
    public void Clear_Num()
    {
        //맵 초기화 색상 및 정답 변경
        for (int i=0; i < num_Obj.Length; i++)
        {
            Renderer renderer = num_Obj[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                // [RED=1, ORANGE=2, YELLOW=3, GREEN=4, BLUE=5, PURPLE=6, BLACK=7, WHITE=8, GRAY=9]
                int color = num_Answer[i];
                switch (color) {
                    case 1:
                        renderer.material.color = Color.red;
                        break;
                    case 2:
                        renderer.material.color = new Color(1.0f, 0.5f, 0.0f);
                        break;
                    case 3:
                        renderer.material.color = Color.yellow;
                        break;
                    case 4:
                        renderer.material.color = Color.green;
                        break;
                    case 5:
                        renderer.material.color = Color.blue;
                        break;
                    case 6:
                        renderer.material.color = new Color(0.5f, 0.0f, 0.5f);
                        break;
                    case 7:
                        renderer.material.color = Color.black;
                        break;
                    case 8:
                        renderer.material.color = Color.white;
                        break;
                    case 9:
                        renderer.material.color = Color.gray;
                        break;
                }

                    
            }
        }
       
    }

    public void Write_Answer(int num)
    {

    }
    public void Answer_Check()
    {
        //정답 체크

    }

}
