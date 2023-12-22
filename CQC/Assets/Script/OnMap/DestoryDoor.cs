using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryDoor : MonoBehaviour

{
    public Rigidbody rigid;
void OnTriggerEnter(Collider col)      //부모로 만든 빈 오브젝트의 Collider를 건드리면 (누가 건드렸는지 확인)
{
    if (col.CompareTag("explode") || col.CompareTag("charge explode"))                 // 만약 "Weapon" 태그로 지정된 무기가 Collider를 건드리면
    {
        rigid.constraints = RigidbodyConstraints.None;     //rigidbody.constraints 체크 해제
        Destroy(gameObject, 50);                         // 파괴된 오브젝트는 10초 후에 삭제, 사라지게함
    }
}
}