using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeExplode : MonoBehaviour
{
    Mesh viewMesh;
    GameObject enemy;
    public int explodeForce;
    public float explodeDamage;
    public float viewRadius;
    public float killRadius;
    public float stunRadius;
    public float maxStunTime;
    public LayerMask targetMask, obstacleMask, destoryMask, DeleteMask;
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
        Destroy(gameObject, 3f);//5���� ������Ʈ ����
        Invoke("DestroyObject", 0.05f);
        Invoke("KillTargets", 0.075f);
        Invoke("StunTargets", 0.1f);
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
            explodeDamage = 600;
        }
        else if (SaveValue.diff == 2)
        {
            explodeDamage = 500;
        }
        else
        {
            explodeDamage = 400;
        }
    }
    void KillTargets()
    {
        // viewRadius�� ���������� �� �� ���� �� targetMask ���̾��� �ݶ��̴��� ��� ������
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, killRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

            // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            {
                if (target.gameObject.CompareTag("enemy"))
                {
                    target.GetComponent<Enemy>().damage(explodeDamage / (dstToTarget + 1));
                }
                if (target.gameObject.CompareTag("Player"))
                {
                    target.GetComponent<Player>().damage(explodeDamage / (dstToTarget + 3));
                }
            }
        }
    }
    void DestroyObject()
    {
        // viewRadius�� ���������� �� �� ���� �� targetMask ���̾��� �ݶ��̴��� ��� ������
        Collider[] ObjectInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, DeleteMask);

        for (int i = 0; i < ObjectInViewRadius.Length; i++)
        {
            Transform target = ObjectInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
            Destroy(target.gameObject);
        }
        // viewRadius�� ���������� �� �� ���� �� targetMask ���̾��� �ݶ��̴��� ��� ������
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, destoryMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

            // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            {
                Rigidbody rigid = target.GetComponent<Rigidbody>();
                rigid.AddExplosionForce(explodeForce, transform.position, viewRadius, 0, ForceMode.VelocityChange);
            }
        }
    }
    void StunTargets()
    {
        // viewRadius�� ���������� �� �� ���� �� targetMask ���̾��� �ݶ��̴��� ��� ������
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, stunRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

            // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add
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
