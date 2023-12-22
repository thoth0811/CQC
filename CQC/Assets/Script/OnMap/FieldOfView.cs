using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    Mesh viewMesh;
    LineRenderer lineRenderer;
    public float viewRadius;
    public float viewNearRadius;
    private float diffRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;
    public float maskCutawayDst;
    public float StunTime;
    public bool isStun;
    public bool inSmoke;
    private bool isStunning;
    private float StunningTime;
    public MeshFilter viewMeshFilter;
    public LayerMask targetMask, obstacleMask, smokeMask;
    public List<Transform> visibleTargets = new List<Transform>();
    public GameObject aimTarget;
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
    public ViewCastInfo Aim;
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask + smokeMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }
    ViewCastInfo NearViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewNearRadius, obstacleMask + smokeMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewNearRadius, viewNearRadius, globalAngle);
        }
    }
    ViewCastInfo AimCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask + targetMask))
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
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        lineRenderer = GetComponent<LineRenderer>();

    }
    void Update()
    {
        if(Check.isPause == false)
        {
            FindVisibleTargets();
            doStun();
            view();
        }
    }
    void LateUpdate()
    {
        DrawFieldOfView();
        DrawAimLine();
    }
    void setDiff()
    {
        if (SaveValue.diff == 1)
        {
            diffRadius = 1.2f;
        }
        else if (SaveValue.diff == 2)
        {
            diffRadius = 1f;
        }
        else
        {
            diffRadius = 0.8f;
        }
    }
    private void OnTriggerEnter(Collider col)//��ü�� �浹��
    {
        if (col.gameObject.CompareTag("smoke"))
        {
            inSmoke = true;
        }
    }
    private void OnTriggerExit(Collider col)//��ü�� �浹��
    {
        if (col.gameObject.CompareTag("smoke"))
        {
            inSmoke = false;
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        // viewRadius�� ���������� �� �� ���� �� targetMask ���̾��� �ݶ��̴��� ��� ������
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // �÷��̾�� forward�� target�� �̷�� ���� ������ ���� �����
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 || Vector3.Distance(transform.position, target.position) < viewNearRadius)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // Ÿ������ ���� ����ĳ��Ʈ�� obstacleMask�� �ɸ��� ������ visibleTargets�� Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo prevViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            // i�� 0�̸� prevViewCast�� �ƹ� ���� ���� ���� ������ �� �� �����Ƿ� �ǳʶڴ�.
            if (i != 0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.dst - newViewCast.dst) > edgeDstThreshold;

                // �� �� �� raycast�� ��ֹ��� ������ �ʾҰų� �� raycast�� ���� �ٸ� ��ֹ��� hit �� ���̶��(edgeDstThresholdExceed ���η� ���)
                if (prevViewCast.hit != newViewCast.hit || (prevViewCast.hit && newViewCast.hit && edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewCast);

                    // zero�� �ƴ� ������ �߰���
                    if (e.PointA != Vector3.zero)
                    {
                        viewPoints.Add(e.PointA);
                    }

                    if (e.PointB != Vector3.zero)
                    {
                        viewPoints.Add(e.PointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            prevViewCast = newViewCast;
        }
        stepCount = Mathf.RoundToInt((360 - viewAngle) * meshResolution);
        stepAngleSize = (360 - viewAngle) / stepCount;

        // ���ø��� ������ ���ϴ� ��ǥ�� ����� stepCount ��ŭ�� �������� ��
        for (int i = 0; i <= stepCount + 2; i++)
        {
            float angle = transform.eulerAngles.y + viewAngle / 2 + stepAngleSize * (i - 1);
            ViewCastInfo newViewCast = NearViewCast(angle);
            // i�� 0�̸� prevViewCast�� �ƹ� ���� ���� ���� ������ �� �� �����Ƿ� �ǳʶڴ�.
            if (i != 0)
            {
                bool edgeDstThresholdExceed = Mathf.Abs(prevViewCast.dst - newViewCast.dst) > edgeDstThreshold;

                // �� �� �� raycast�� ��ֹ��� ������ �ʾҰų� �� raycast�� ���� �ٸ� ��ֹ��� hit �� ���̶��(edgeDstThresholdExceed ���η� ���)
                if (prevViewCast.hit != newViewCast.hit || (prevViewCast.hit && newViewCast.hit && edgeDstThresholdExceed))
                {
                    Edge e = FindEdge(prevViewCast, newViewCast);

                    // zero�� �ƴ� ������ �߰���
                    if (e.PointA != Vector3.zero)
                    {
                        viewPoints.Add(e.PointA);
                    }

                    if (e.PointB != Vector3.zero)
                    {
                        viewPoints.Add(e.PointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            prevViewCast = newViewCast;
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]) + Vector3.forward * maskCutawayDst;
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    void DrawAimLine()
    {
        Aim = AimCast(transform.eulerAngles.y);
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(Aim.point.x, Aim.point.y - 0.1f, Aim.point.z));

    }
    void view()
    {
        if(isStun == false && inSmoke == false)
        {
            if (Input.GetButton("zoom"))
            {
                if(Check.Guntype == 3 && Garget.GunOn == true)
                {
                    viewAngle = 10f;
                    viewRadius = 50f * diffRadius;
                    viewNearRadius = 2f;
                }
                else
                {
                    viewAngle = 15f;
                    viewRadius = 30f * diffRadius;
                    viewNearRadius = 3f;
                }
            }
            else
            {
                viewAngle = 90f;
                viewRadius = 20f * diffRadius;
                viewNearRadius = 5f;
            }
        }
        else
        {
            viewRadius = 5;
            viewNearRadius = 5;
        }
    }
    void doStun()
    {
        if (isStun == true && isStunning == false)
        {
            isStunning = true;
            StunningTime = Time.time + StunTime;

        }
        if (Time.time > StunningTime && isStunning == true)
        {
            isStun = false;
            isStunning = false;
        }
    }
    Edge FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = minAngle + (maxAngle - minAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceed = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceed)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new Edge(minPoint, maxPoint);
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
