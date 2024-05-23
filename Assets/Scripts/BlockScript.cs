using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour,BlockSpeedIO
{
    [SerializeField]
    Rigidbody2D _rigidbody;
    float _speed; // 移動速度

    void Start()
    {
        // 力を加える
        _rigidbody.AddForce(new Vector2(_speed, 0));
    }

    private void OnEnable()
    {
        // 力を加える
        _rigidbody.AddForce(new Vector2(_speed, 0));
    }

    void Update()
    {
        //Vector2 position = transform.position;
        //if (position.x < GetLeft())
        //{
        //    Destroy(gameObject); // 画面外に出たので消す.
        //}
    }

    float GetLeft()
    {
        // 画面の左下のワールド座標を取得する
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        return min.x;
    }

    // 速度を設定
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

}
