using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleAI : MonoBehaviour
{
    public float speed = 10f; // 기본 속도
    public float slowSpeed = 2f; // 마지막 Waypoint로 갈 때 속도
    public float decelerationDistance = 5f; // 속도를 줄이기 시작할 거리

    [Header("WayPointNav")]
    NavMeshAgent agent;
    private int currentNode = 0;
    [SerializeField] List<Transform> waypoint = new List<Transform>();

    [Header("WheelAnim")]
    [SerializeField] Animator[] anim_Wheel;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.speed = speed; // 기본 속도 설정
        GotoNext();
    }

    void Update()
    {
        HandleDeceleration();
        CarAnim();
    }

    void FixedUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 2f)
        {
            GotoNext(); // 다음 Waypoint로 이동
        }
    }

    void GotoNext()
    {
        if (currentNode == waypoint.Count - 1)
        {
            // 마지막 Waypoint에 도착했을 때 첫 번째 Waypoint로 강제 이동
            transform.position = waypoint[0].position;
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

    public void LeftRine()
    {
        foreach (Animator anim in anim_Wheel)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Stop", false);
            anim.SetBool("Left", true);
        }
    }

    public void RightLine()
    {
        foreach (Animator anim in anim_Wheel)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Stop", false);
            anim.SetBool("Right", true);
        }
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
