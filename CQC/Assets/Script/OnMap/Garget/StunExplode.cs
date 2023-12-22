using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunExplode : MonoBehaviour
{
    Mesh viewMesh;
    GameObject enemy;
    GameObject player;
    public float viewRadius;
    public float maxStunTime;
    public LayerMask targetMask, obstacleMask;
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;
        

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct Edge
    {
        public Vector3 PointA, PointB;
        public Edge(Vector3 _PointA, Vector3 _PointB)
        {
            PointA = _PointA;
            PointB = _PointB;
        }
    }
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }
    void Start()
    {
        setDiff();
        Destroy(gameObject, 3f);//5초후 오브젝트 삭제
        StunTargets();
    }
    void Update()
    {
        
    }
    void LateUpdate()
    {

    }
    void setDiff()
    {
        if (SaveValue.diff == 1)
        {
            maxStunTime = 10;
        }
        else if (SaveValue.diff == 2)
        {
            maxStunTime = 8;
        }
        else
        {
            maxStunTime = 6;
        }
    }
    void StunTargets()
    {
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

            // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            {
                if (target.gameObject.CompareTag("enemy"))
                {
                    enemy = targetsInViewRadius[i].gameObject;
                    enemy.GetComponent<FieldOfEnemyView>().isStun = true;
                    enemy.GetComponent<FieldOfEnemyView>().StunTime = maxStunTime / (dstToTarget + 0.1f);

                }
                if (target.gameObject.CompareTag("Player"))
                {
                    player = targetsInViewRadius[i].gameObject;
                    player.GetComponent<FieldOfView>().isStun = true;
                    player.GetComponent<FieldOfView>().StunTime = (16 - maxStunTime) / (dstToTarget + 0.1f);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
