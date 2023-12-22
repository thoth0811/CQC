using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryDoor : MonoBehaviour

{
    public Rigidbody rigid;
void OnTriggerEnter(Collider col)      //�θ�� ���� �� ������Ʈ�� Collider�� �ǵ帮�� (���� �ǵ�ȴ��� Ȯ��)
{
    if (col.CompareTag("explode") || col.CompareTag("charge explode"))                 // ���� "Weapon" �±׷� ������ ���Ⱑ Collider�� �ǵ帮��
    {
        rigid.constraints = RigidbodyConstraints.None;     //rigidbody.constraints üũ ����
        Destroy(gameObject, 50);                         // �ı��� ������Ʈ�� 10�� �Ŀ� ����, ���������
    }
}
}