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
    float JUMP_VELOCITY = 1000; // �@�W�����v�͂̒�`

    [SerializeField]
    float ANTI_MASHING_TIME = 500;//0.5�b

    Rigidbody2D _rigidbody; // �A���������R���|�[�l���g�ێ��p

    KeyCode Jump = KeyCode.Space;

    [SerializeField]
    AudioManager _audioManager;

    [SerializeField] AudioSource jumpAudioSource;
    [SerializeField] AudioSource kayukiAudioSource;
    // �J�n����
    void Start()
    {

        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        // �B���������R���|�[�l���g���擾
        _rigidbody = GetComponent<Rigidbody2D>();
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(Jump) && _gameManager._GameState.Value == GameState.Gaming)
            .ThrottleFirst(TimeSpan.FromMilliseconds(ANTI_MASHING_TIME))
            .Subscribe(_ =>
             {
                 JumpAction();//�Ȃ񂩂̏���
             });
    }
    // �X�V
    void Update()
    {
    }

    // �Œ�t���[���X�V
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
        // �@���W���擾
        Vector3 position = transform.position;
        // �A��ʊO�ɏo�Ȃ��悤�ɂ���
        float y = transform.position.y;
        float vx = _rigidbody.velocity.x;
        if (y > GetTop())
        {
            _rigidbody.velocity = Vector2.zero; // ���x����x���Z�b�g����
            position.y = GetTop(); // �B�����߂�����
        }
        if (y < GetBottom())
        {
            // �C���ɗ�������I�[�g�W�����v
            _rigidbody.velocity = Vector2.zero; // �������x����x���Z�b�g����
            _rigidbody.AddForce(new Vector2(0, JUMP_VELOCITY));
            position.y = GetBottom(); // �����߂�����
        }

        // �D���W�𔽉f����
        transform.position = position;
    }

    void JumpAction()
    {
        jumpAudioSource.Play();
        kayukiAudioSource.Play();
        _rigidbody.velocity = Vector2.zero; // �������x����x���Z�b�g����
        _rigidbody.AddForce(new Vector2(0, JUMP_VELOCITY)); // ������ɗ͂�������
    }

    // ��ʏ���擾����
    float GetTop()
    {
        // ��ʂ̉E��̃��[���h���W���擾����
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        return max.y;
    }

    // ��ʉ����擾����
    float GetBottom()
    {
        // ��ʂ̍����̃��[���h���W���擾����
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
