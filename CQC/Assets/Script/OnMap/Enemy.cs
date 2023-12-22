using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem impact;
    public float fullHealth;
    public float health;
    public float speed;
    public float dst;
    public GameObject bullet;
    List<Transform> visibleTargets = new List<Transform>();
    bool isStun;
    public bool isMove = false;
    public bool isRotating = false;
    float viewRadius;
    RaycastHit hit;
    public float wayAngle;
    public LayerMask obstacleMask, ignoreMask;
    void Start()
    {
        health = fullHealth;
        InvokeRepeating("setWay", 0, 3);
        InvokeRepeating("setMove", Time.deltaTime * 3, 3);
        wayAngle = Random.Range(0f, 360f);
    }
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        visibleTargets = transform.GetComponent<FieldOfEnemyView>().visibleTargets;
        isStun = transform.GetComponent<FieldOfEnemyView>().isStun;
        viewRadius = transform.GetComponent<FieldOfEnemyView>().viewRadius;
        rotation();
        checkDst();
    }
    private void OnCollisionEnter(Collision col)//물체와 충돌시
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            showImpact(col.gameObject);
        }
    }
    void showImpact(GameObject col)
    {
        float impactX = col.transform.localEulerAngles.x + 90f;
        float impactY = col.transform.localEulerAngles.y;
        float impactZ = col.transform.localEulerAngles.z;
        Instantiate(impact, col.transform.position, Quaternion.Euler(impactX, impactY, impactZ));
    }
    public void damage(float damage)
    {
        health -= damage;
    }
    void setWay()
    {
        if (isRotating == false)
        {
            if(dst >= 2f)
            {
                wayAngle = Random.Range(wayAngle - 60f, wayAngle + 60f);
            }
            else
            {
                wayAngle = Random.Range(wayAngle + 60f, wayAngle + 300f);
                if(wayAngle >= 360f)
                {
                    wayAngle -= 360f;
                }
            }
            isRotating = true;
        }
    }
    void setMove()
    {
        if (Check.GameStart == true && dst >= 2f)
        {
            isRotating = false;
            isMove = true;
            InvokeRepeating("move", 0, Time.deltaTime);
            Invoke("cancelRotate", 2);
        }
        else
        {
            cancelRotate();
        }
    }
    void rotation()
    {
        if (visibleTargets.Count == 0 && isStun == false)
        {
            if (isMove == false)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, wayAngle, transform.rotation.z), Time.deltaTime * 3);
            }
        }
        else
        {
            cancelRotate();
        }
    }
    void move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
    void cancelRotate()
    {
        isRotating = false;
        isMove = false;
        CancelInvoke("move");
    }
    void checkDst()
    {
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewRadius, obstacleMask + ~ignoreMask);
        if (hit.distance == 0)
        {
            dst = viewRadius;
        }
        else
        {
            dst = hit.distance;
        }
    }
}
