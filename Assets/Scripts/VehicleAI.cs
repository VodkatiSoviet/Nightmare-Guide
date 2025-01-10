using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VehicleAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    Transform target;

    [Header("WheelAnim")]
    [SerializeField] Animator[] anim_Wheel;
    

    public float speed;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

      /*  for(int i=0; i<4; i++)
        {
            anim_Wheel[i] = GetComponent<Animator>();
        }*/
      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.speed *= speed;
            agent.SetDestination(target.position);
        }
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
        foreach(Animator anim in anim_Wheel)
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
