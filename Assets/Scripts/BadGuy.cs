using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuy : MonoBehaviour
{
    Rigidbody rigid;
    CapsuleCollider capcol;
    public Transform target;
    NavMeshAgent nav;
    BoxCollider carparkCollider;

    public float stoppingDistance;
    
    void Awake()
    {   
        rigid = GetComponent<Rigidbody>();
        capcol = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        carparkCollider = GameObject.FindWithTag("BadGuyArea").GetComponent<BoxCollider>();
        nav.isStopped = true;
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }

    void Update()
    {
        if (carparkCollider.bounds.Contains(target.position))
        {
            // Player가 Carpark 영역에 들어온 경우 NPC를 쫓아갑니다.
            nav.SetDestination(target.position);
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= stoppingDistance)
            {
                // Player와 일정 거리(stoppingDistance) 안에 있는 경우 멈춥니다.
                nav.isStopped = true;
            }
            else
            {
                nav.isStopped = false;
            }
        }
        else
        {
            // Carpark 영역을 벗어난 경우 멈추도록 처리합니다.
            nav.isStopped = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Debug.Log("플레이어와 부딪힘");
        }
    }
}
