using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniVRM10;

public class BezierMissile : MonoBehaviour
{
    Vector3[] point = new Vector3[4];       //4���� ������ ��Ʈ�� ����Ʈ�� ������ �迭 
    bool hit = false;                       //�̻����� ��ǥ�� �����ߴ��� ���θ� ��Ÿ���� ����

    [SerializeField][Range(0f, 1f)] private float t = 0;    //������ ������� �ð� ���� 
    [SerializeField] public float spd = 5;                  //�̻����� �̵��ӵ�
    [SerializeField] public float posA = 0.55f;             //al�̻��� ������ �����ϴ� �Ķ���� A
    [SerializeField] public float posB = 0.45f;             //al�̻��� ������ �����ϴ� �Ķ���� B

    public GameObject master;           //�̻����� ���� ��ġ�� ��Ÿ���� ���� ������Ʈ
    public GameObject enemy;            //�̻����� ��ǥ ��ġ�� ��Ÿ���� ���� ������Ʈ

    public void Start()
    {
        point[0] = master.transform.position;                   //������ġ
        point[1] = PointSetting(master.transform.position);     //���� ��Ʈ��
        point[2] = PointSetting(enemy.transform.position);      //���� ��Ʈ��
        point[3] = enemy.transform.position;                    //��ǥ��ġ

    }
    private void FixedUpdate()
    {
        if (t > 1) return;      //�ð��� 1�� �ʰ��ϸ� �̻��� �̵� ����
        if (hit) return;        //��ǥ�� �����ϸ� �̻��� �̵� ����
        t += Time.deltaTime * spd;      //�ð� ������Ʈ
        DrawTrajectory();           //�̻��� ���� ������Ʈ
    }

    Vector3 PointSetting(Vector3 origin)
    {
        float x, y, z;

        x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.x;
        y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.y;
        z = posA * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) + origin.z;

        return new Vector3(x, y, z);
    }

    void DrawTrajectory()   //�̻����� ���� ��ġ�� ������ ��� ����Ͽ� ������Ʈ 
    {
        transform.position = new Vector3(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
            FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y),
            FourPointBezier(point[0].z, point[1].z, point[2].z, point[3].z)
        );
    }

    private float FourPointBezier(float a, float b, float c, float d) //4���� ��Ʈ�� ����Ʋ�� ����ؼ� ������ ������� ���� ��� 
    {       
        return Mathf.Pow((1 - t), 3) * a + Mathf.Pow((1 - t), 2) * 3 * t * b + Mathf.Pow(t, 2) * 3 * (1 - t) * c + Mathf.Pow(t, 3) * d;
    }

}
