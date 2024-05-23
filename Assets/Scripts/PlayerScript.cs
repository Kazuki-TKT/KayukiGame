using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerScript : MonoBehaviour
{
    public GameManager _gameManager;

    [SerializeField]
    float JUMP_VELOCITY = 1000; // ①ジャンプ力の定義

    [SerializeField]
    float ANTI_MASHING_TIME = 500;//0.5秒

    Rigidbody2D _rigidbody; // ②物理挙動コンポーネント保持用

    KeyCode Jump = KeyCode.Space;

    [SerializeField]
    AudioManager _audioManager;

    [SerializeField] AudioSource jumpAudioSource;
    [SerializeField] AudioSource kayukiAudioSource;
    // 開始処理
    void Start()
    {

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        // ③物理挙動コンポーネントを取得
        _rigidbody = GetComponent<Rigidbody2D>();
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(Jump) && _gameManager._GameState.Value == GameState.Gaming)
            .ThrottleFirst(TimeSpan.FromMilliseconds(ANTI_MASHING_TIME))
            .Subscribe(_ =>
             {
                 JumpAction();//なんかの処理
             });
    }
    // 更新
    void Update()
    {
    }

    // 固定フレーム更新
    private void FixedUpdate()
    {
        if(_gameManager._GameState.Value == GameState.Gaming)
        {
            _rigidbody.simulated = true;
        }
        else
        {
            _rigidbody.simulated = false;
            if(kayukiAudioSource.isPlaying)
            {
                kayukiAudioSource.Stop();
            }
        }
        // ①座標を取得
        Vector3 position = transform.position;
        // ②画面外に出ないようにする
        float y = transform.position.y;
        float vx = _rigidbody.velocity.x;
        if (y > GetTop())
        {
            _rigidbody.velocity = Vector2.zero; // 速度を一度リセットする
            position.y = GetTop(); // ③押し戻しする
        }
        if (y < GetBottom())
        {
            // ④下に落ちたらオートジャンプ
            _rigidbody.velocity = Vector2.zero; // 落下速度を一度リセットする
            _rigidbody.AddForce(new Vector2(0, JUMP_VELOCITY));
            position.y = GetBottom(); // 押し戻しする
        }

        // ⑤座標を反映する
        transform.position = position;
    }

    void JumpAction()
    {
        jumpAudioSource.Play();
        kayukiAudioSource.Play();
        _rigidbody.velocity = Vector2.zero; // 落下速度を一度リセットする
        _rigidbody.AddForce(new Vector2(0, JUMP_VELOCITY)); // 上方向に力を加える
    }

    // 画面上を取得する
    float GetTop()
    {
        // 画面の右上のワールド座標を取得する
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        return max.y;
    }

    // 画面下を取得する
    float GetBottom()
    {
        // 画面の左下のワールド座標を取得する
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        return min.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item") && _gameManager._GameState.Value == GameState.Gaming)
        {
            Debug.Log($"<color=GreenYellow>{collision.name}</color>");
            if (collision.name.Contains("Yuka")|| collision.name.Contains("Noa"))
            {
                _audioManager.PlaySE(SESoundData.SE.Down);
            }else if (collision.name.Contains("Aokiseki"))
            {
                _audioManager.PlaySE(SESoundData.SE.Aokiseki);
            }


            GetItemIO getItemIO = collision.GetComponentInChildren<GetItemIO>();
            getItemIO.GetItem();
            collision.gameObject.SetActive(false);
        }
    }
}
