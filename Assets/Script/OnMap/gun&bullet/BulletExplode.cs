using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplode : MonoBehaviour
{
    public int explodeForce;
    public float viewRadius;
    public LayerMask obstacleMask, destoryMask, DeleteMask;
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
        Destroy(gameObject, 0.1f);//5초후 오브젝트 삭제
        DestroyObject();
    }
    void Update()
    {

    }
    void LateUpdate()
    {

    }
    void DestroyObject()
    {
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] ObjectInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, DeleteMask);

        for (int i = 0; i < ObjectInViewRadius.Length; i++)
        {
            Transform target = ObjectInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
            Destroy(target.gameObject);
        }
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, destoryMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

            // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            {
                Rigidbody rigid = target.GetComponent<Rigidbody>();
                rigid.AddExplosionForce(explodeForce, transform.position, viewRadius);
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
