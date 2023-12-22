using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfEnemyView : MonoBehaviour
{
    Vector3 dirToTarget;
    public int fullammo;
    public int ammo;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    [Range(0, 360)]
    public float shootAngle;
    public static float rotateSpeed = 120;
    public float fireRate;
    public float firstFire;
    public float StunTime;
    public bool isStun;
    private float reloading = 2.1f;
    private float StunningTime;
    private float nextFire;
    private bool isReloading;
    private bool isStunning;
    public Transform gun;
    public GameObject shell;
    public GameObject bullet;//총알 가져오기
    public AudioSource shoot;//총소리 가져오기
    public AudioSource reload;//재장전 소리 가져오기
    public LayerMask targetMask, obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();
    void Start()
    {
        setDiff();
    }
    void Update()
    {
        if (Check.isPause == false)
        {
            FindVisibleTargets();
            Aim();
            doReload();
            doStun();
        }
    }
    private void OnTriggerEnter(Collider col)//물체와 충돌시
    {
        if (col.gameObject.CompareTag("smoke"))
        {
            viewRadius = 5;
        }
    }
    private void OnTriggerExit(Collider col)//물체와 충돌시
    {
        if (col.gameObject.CompareTag("smoke"))
        {
            viewRadius = 20;
        }
    }
    void setDiff()
    {
        if (SaveValue.diff == 1)
        {
            firstFire = 0.4f;
        }
        else if (SaveValue.diff == 2)
        {
            firstFire = 0.3f;
        }
        else
        {
            firstFire = 0.2f;
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            dirToTarget = (target.position - transform.position).normalized;

            // 플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면
            if (Vector3.Angle(transform.forward, dirToTarget) < 180)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
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
    void Aim()
    {
        if (visibleTargets.Count > 0 && isStun == false)
        {
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * rotateSpeed);
            }
            if (Vector3.Angle(transform.forward, dirToTarget) >= viewAngle / 2)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * rotateSpeed / 40);
            }
            if (Vector3.Angle(transform.forward, dirToTarget) < shootAngle / 2)
            {
                if (Time.time > nextFire && ammo > 0)
                {
                    DoShoot();
                }
            }
        }
        if (visibleTargets.Count == 0)
        {
            nextFire = Time.time + firstFire;
        }
    }
    void DoShoot()
    {
        nextFire = Time.time + fireRate;
        ammo--;
        Instantiate(bullet, gun.position, gun.rotation);
        Instantiate(shell, gun.position, gun.rotation);//위치에 탄피 복사
        shoot.Play();//총소리
    }
    void DoReload()
    {
        ammo = fullammo;
        isReloading = false;
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
    void doReload()
    {
        if (ammo == 0 && isReloading == false)
        {
            isReloading = true;
            nextFire = Time.time + reloading;
            reload.Play();
            Invoke("DoReload", 1.6f);
        }
    }
}