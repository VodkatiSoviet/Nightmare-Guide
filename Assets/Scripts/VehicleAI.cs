using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleAI : MonoBehaviour
{
    public float speed = 10f; // 기본 속도
    public float slowSpeed = 2f; // 마지막 Waypoint로 갈 때 속도
    public float decelerationDistance = 3f; // 속도를 줄이기 시작할 거리

    [Header("WayPointNav")]
    [SerializeField] private List<Transform> waypoint = new List<Transform>(); // 자동차가 이동할 라인
    private NavMeshAgent agent;
    private int currentNode = 0;

    [Header("WheelAnim")]
    [SerializeField] private Animator[] anim_Wheel;

    private bool isPlayerInRange = false;
    private bool isShinho = false;

    public bool offline = false; // 주차용 차량 판별
    public bool roopCar = true;
    void Start()
    {
        // NavMeshAgent 및 Waypoint 초기화 검증
        agent = GetComponent<NavMeshAgent>();
        if (agent == null || waypoint == null || waypoint.Count == 0)
        {
            Debug.LogError("NavMeshAgent 또는 Waypoint가 제대로 설정되지 않았습니다.");
            enabled = false; // 스크립트 비활성화
            return;
        }

        if (offline) return; // 주차 차량은 초기화 작업 생략

        agent.autoBraking = false;
        agent.speed = speed;
        GotoNext(); // 첫 Waypoint로 이동
        SetWheelAnimation("Idle"); // 기본 애니메이션
    }

    void Update()
    {
        if (offline || isPlayerInRange) return;

        HandleDeceleration();
    }

    void FixedUpdate()
    {
        if (offline || isPlayerInRange || isShinho || agent.pathPending) return;

        if (agent.remainingDistance < 2f)
        {
            GotoNext();
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < waypoint.Count; i++)
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            Gizmos.DrawSphere(waypoint[i].transform.position, 2);
            Gizmos.DrawWireSphere(waypoint[i].transform.position, 20f);

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
    private void GotoNext()
    {
        if (waypoint.Count == 0) return;

        // 마지막 Waypoint에 도달하면 초기화
        if (currentNode >= waypoint.Count && !roopCar)
        {
            currentNode = 0;
            agent.speed = speed; // 속도 복구
        }

        // 유효한 Waypoint로 이동
        if (waypoint[currentNode] != null)
        {
            agent.destination = waypoint[currentNode].position;
            currentNode++;
        }
    }

    private void HandleDeceleration()
    {
        if (currentNode == waypoint.Count - 1 && agent.remainingDistance <= decelerationDistance)
        {
            agent.speed = Mathf.Lerp(agent.speed, slowSpeed, Time.deltaTime);
            Invoke(nameof(ResetPosition), 1f); // 1초 뒤 위치 초기화
        }
    }

    private void ResetPosition()
    {
        transform.position = waypoint[0].position;
        currentNode = 0;
        agent.speed = speed; // 기본 속도로 복구
    }

    private void OnTriggerEnter(Collider other)
    {
        if (offline) return;

        if (other.CompareTag("Player") || other.CompareTag("Shinho"))
        {
            isPlayerInRange = true;
            agent.isStopped = true;
            SetWheelAnimation("Stop");
        }
        else if (other.CompareTag("LeftPoint") || other.CompareTag("RightPoint"))
        {
            HandleTurnAnimation(other.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (offline) return;

        if (other.CompareTag("Player") || other.CompareTag("Shinho"))
        {
            isPlayerInRange = false;
            agent.isStopped = false;
            agent.speed = speed; // 기본 속도로 복구
            SetWheelAnimation("Idle");
        }
    }

    private void HandleTurnAnimation(string tag)
    {
        if (tag == "LeftPoint")
        {
            SetWheelAnimation("Left");
        }
        else if (tag == "RightPoint")
        {
            SetWheelAnimation("Right");
        }
        Invoke(nameof(ResetAnimation), 2f);
    }

    private void SetWheelAnimation(string state)
    {
        foreach (var anim in anim_Wheel)
        {
            anim.SetBool("Left", state == "Left");
            anim.SetBool("Right", state == "Right");
            anim.SetBool("Stop", state == "Stop");
        }
    }

    private void ResetAnimation()
    {
        SetWheelAnimation("Idle");
    }
}
