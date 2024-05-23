using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour,BlockSpeedIO
{
    [SerializeField]
    Rigidbody2D _rigidbody;
    float _speed; // �ړ����x

    void Start()
    {
        // �͂�������
        _rigidbody.AddForce(new Vector2(_speed, 0));
    }

    private void OnEnable()
    {
        // �͂�������
        _rigidbody.AddForce(new Vector2(_speed, 0));
    }

    void Update()
    {
        //Vector2 position = transform.position;
        //if (position.x < GetLeft())
        //{
        //    Destroy(gameObject); // ��ʊO�ɏo���̂ŏ���.
        //}
    }

    float GetLeft()
    {
        // ��ʂ̍����̃��[���h���W���擾����
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        return min.x;
    }

    // ���x��ݒ�
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

}
