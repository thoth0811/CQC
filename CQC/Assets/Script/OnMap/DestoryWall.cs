using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWall : MonoBehaviour
{
    public Rigidbody rigid;
    void OnTriggerEnter(Collider col)      //�θ�� ���� �� ������Ʈ�� Collider�� �ǵ帮�� (���� �ǵ�ȴ��� Ȯ��)
    {
        if (col.CompareTag("charge explode"))                 // ���� "Weapon" �±׷� ������ ���Ⱑ Collider�� �ǵ帮��
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            rigid.constraints = RigidbodyConstraints.None;     //rigidbody.constraints üũ ����
            Destroy(gameObject, 50);                         // �ı��� ������Ʈ�� 10�� �Ŀ� ����, ���������
        }
    }
}
