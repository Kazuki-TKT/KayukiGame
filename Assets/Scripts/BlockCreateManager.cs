using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreateManager : MonoBehaviour
{
    // ��������Block�I�u�W�F�N�g
    public GameObject block;

    // 0�ɂȂ�����Block�I�u�W�F�N�g�𐶐�
    float _timer = 0;
    // �g�[�^���̌o�ߎ��Ԃ�ێ�
    float _totalTime = 0;
    // �@�u���b�N������
    int _cnt = 0;

    [SerializeField]
    float setSpeed = 100;
    void Start()
    {
    }

    void Update()
    {
        // �o�ߎ��Ԃ���������
        _timer -= Time.deltaTime;
        // �A�g�[�^�����Ԃ����Z
        _totalTime += Time.deltaTime;

        if (_timer < 0)
        {
            // 0�ɂȂ����̂�Block����
            // BlockMgr�̏ꏊ���琶��
            Vector3 position = transform.position;

            // ���㉺(�}3)�̃����_���Ȉʒu�ɏo��������
            position.y = Random.Range(-4, 4);
            // �v���n�u�����Ƃ�Block����
            GameObject obj = Instantiate(block, position, Quaternion.identity);
            // �BBlock�I�u�W�F�N�g�́uBlock�v�X�N���v�g���擾����
            BlockSpeedIO blockScript = obj.GetComponentInChildren<BlockSpeedIO>();
            // �C���x���v�Z���Đݒ�
            // ��{���x100�ɁA�o�ߎ���x10��������
            float speed = setSpeed + (_totalTime * 10);
            blockScript.SetSpeed(-speed); // �������Ȃ̂Ń}�C�i�X
                                          // 1�b��ɂ܂���������
            _timer += 1;

            // �A�����񐔂��J�E���g�A�b�v
            _cnt++;
            if (_cnt % 10 < 3)
            {
                // 0.1�b��ɂ܂���������
                _timer += 0.1f;
            }
            else
            {
                // 1�b��ɂ܂���������
                _timer += 1;
            }
        }
    }
}
