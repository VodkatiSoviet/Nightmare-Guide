using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI.Table;

public class VehicleAI : MonoBehaviour
{
    public float speed = 10f; // 기본 속도
    public float slowSpeed = 2f; // 마지막 Waypoint로 갈 때 속도
    public float decelerationDistance = 3f; // 속도를 줄이기 시작할 거리


    [Header("WayPointNav")]
    NavMeshAgent agent;
    private int currentNode = 0;
    [SerializeField] List<Transform> waypoint = new List<Transform>(); // 자동차가 이동할 라인

    [Header("WheelAnim")]
    [SerializeField] Animator[] anim_Wheel;

    private bool isPlayerInRange = false; // 플레이어가 범위 안에 있는지 확인하는 플래그
    private bool isShinho = false; // 신호등 인식

    public bool test = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.speed = speed; // 기본 속도 설정
        isPlayerInRange = false;
        isShinho = false;
        GotoNext();
        IdleLine();
    }

    void Update()
    {
            if (!isPlayerInRange) // 플레이어가 범위에 없을 때만 이동
            {
                HandleDeceleration();
            }
  
        CarAnim();
    }

    void FixedUpdate()
    {
        if (!isPlayerInRange && !isShinho && !agent.pathPending && agent.remainingDistance < 2f)
        {
            GotoNext(); // 다음 Waypoint로 이동
        }
    }

    void GotoNext()
    {
        if (currentNode == waypoint.Count - 1)
        {
            currentNode = 0;
            agent.speed = speed; // 속도 원래대로 복구
        }
        else
        {
            // 다음 Waypoint로 이동
            agent.destination = waypoint[currentNode].position;
            currentNode++;
        }
    }

    void HandleDeceleration()
    {
        
        if (currentNode == waypoint.Count - 1 && agent.remainingDistance <= decelerationDistance)
        {
            // 마지막 Waypoint로 접근 시 속도 점진적으로 줄이기
            agent.speed = Mathf.Lerp(agent.speed, slowSpeed, Time.deltaTime);
            Invoke("ClearPosition", 1f);//1초 뒤 차량 위치를 처음 위치로 초기화
        }
    }
    public void ClearPosition()//차량 위치 초기화
    {
        transform.position = waypoint[0].position;
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoint.Count; i++)
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            //Gizmos.DrawSphere(waypoint[i].transform.position, 2);
            //Gizmos.DrawWireSphere(waypoint[i].transform.position, 20f);

            if (i < waypoint.Count - 1)
            {
                if (waypoint[i] && waypoint[i + 1])
                {
                    Gizmos.color = Color.red;
                    if (i < waypoint.Count - 1)
                        Gizmos.DrawLine(waypoint[i].position, waypoint[i + 1].position);
                    if (i < waypoint.Count - 2)
                    {
                        Gizmos.DrawLine(waypoint[waypoint.Count - 1].position, waypoint[0].position);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shinho"))
        {
            isPlayerInRange = true;
            agent.isStopped = true; // 차량 멈춤
            StopLine(); // 정지 애니메이션
        }
        if (other.CompareTag("LeftPoint")|| other.CompareTag("RightPoint")) // 바퀴가 회전할 타이밍에 애니메이션 적용 
        {
            RotationPoint(other.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shinho"))
        {
            isPlayerInRange = false;
            agent.isStopped = false; // 차량 재시작
            agent.speed = speed; // 기본 속도로 복구
            IdleLine(); // 기본 애니메이션
        }
        
    }
    public void CarAnim()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LeftRine();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RightLine();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            IdleLine();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StopLine();
        }
    }
    public void RotationPoint(string rot)
    {
        if (rot.Equals("LeftPoint"))
        {
            LeftRine();
            Invoke("IdleLine", 2f);
        }
        else if(rot.Equals("RightPoint"))
        {
            RightLine();
            Invoke("IdleLine", 2f);
        }
        else
        {
            StopLine();
        }
    }

    public void LeftRine()
    {
        foreach (Animator anim in anim_Wheel)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Stop", false);
            anim.SetBool("Left", true);
        }
        Debug.Log("좌회전중");
    }

    public void RightLine()
    {
        foreach (Animator anim in anim_Wheel)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Stop", false);
            anim.SetBool("Right", true);
        }
        Debug.Log("우회전중");
    }

    public void IdleLine()
    {
        foreach (Animator anim in anim_Wheel)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Stop", false);
        }
    }

    public void StopLine()
    {
        foreach (Animator anim in anim_Wheel)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Stop", true);
        }
    }

    
}
