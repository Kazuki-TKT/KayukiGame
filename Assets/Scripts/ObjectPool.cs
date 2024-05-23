using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public GameManager _gameManager;

    [SerializeField]
    int createCount = 3;
    [SerializeField]
    float _createTime = 2.5f;


    public GameObject poolObj;  // �v�[������Q�[���I�u�W�F�N�g�̃v���n�u
    public int poolSize = 10;  // �v�[������I�u�W�F�N�g�̏����T�C�Y

    private ObjectPool<GameObject> objectPool;  // �Q�[���I�u�W�F�N�g�̃v�[��

    // 0�ɂȂ�����Block�I�u�W�F�N�g�𐶐�
    float _timer = 0;
    // �g�[�^���̌o�ߎ��Ԃ�ێ�
    float _totalTime = 0;
    // �@�u���b�N������
    int _cnt = 0;

    private Vector3 defaultPosition;

    [SerializeField]
    float setSpeed = 100;
    private void Start()
    {
        defaultPosition = transform.position;
        // �I�u�W�F�N�g�v�[���̍쐬
        objectPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(poolObj),          // �v�[������̂Ƃ��ɐV�����C���X�^���X�𐶐����鏈��
            actionOnGet: OnTakeFromPool,        // �C���X�^���X���v�[��������o���ꂽ�Ƃ��ɌĂяo����鏈��
            actionOnRelease: OnReturnedToPool,   // �C���X�^���X���v�[���ɖ߂����Ƃ��ɌĂяo����鏈��
            actionOnDestroy: null,                          // �v�[����maxSize�ɒB�����ہA�v�f���v�[���ɖ߂��Ȃ������Ƃ��ɌĂяo����鏈��
            collectionCheck: false,                         // �v�[���ɖ߂��ۂɊ��ɓ���C���X�^���X���o�^����Ă��邩���ׁA����Η�O�𓊂���B�G�f�B�^�ł̂ݎ��s����邱�Ƃɒ���
            defaultCapacity: poolSize,                      // �f�t�H���g�̗e��
            maxSize: 10);                                  // �v�[���̍ő�T�C�Y

    }

    private void Update()
    {
        if (_gameManager._GameState.Value == GameState.Gaming)
        {
            // �o�ߎ��Ԃ���������
            _timer -= Time.deltaTime;
            // �A�g�[�^�����Ԃ����Z
            _totalTime += Time.deltaTime;

            if (_timer < 0)
            {
                Vector3 position = transform.position;
                position.y = Random.Range(-3, 4);
                GameObject obj = objectPool.Get();
                obj.transform.position = new Vector3(defaultPosition.x, position.y, defaultPosition.z);
                BlockSpeedIO blockScript = obj.GetComponentInChildren<BlockSpeedIO>();
                blockScript.SetSpeed(-setSpeed); // �������Ȃ̂Ń}�C�i�X
                _timer += _createTime;

                _cnt++;
                if (_cnt % 10 < createCount)
                {
                    _timer += 0.2f;
                }
                else
                {
                    _timer += 1;
                }
            }
        }
       
    }


    // �v�[������I�u�W�F�N�g���擾���鎞�ɌĂяo����܂�
    private void OnTakeFromPool(GameObject gameObject)
    {
        // �v�[������擾�����I�u�W�F�N�g���A�N�e�B�u�ɂ��܂�
        gameObject.SetActive(true);
        
        // �v�[������擾�����I�u�W�F�N�g�� 2 �b��Ƀv�[���ɖ߂��R���[�`��
        IEnumerator Process()
        {
            yield return new WaitForSeconds(11);
            objectPool.Release(gameObject);
        }

        // �v�[������擾�����I�u�W�F�N�g�� 2 �b��Ƀv�[���ɖ߂��R���[�`�������s���܂�
        StartCoroutine(Process());
    }
    // �v�[���ɃI�u�W�F�N�g��߂����ɌĂяo����܂�
    private void OnReturnedToPool(GameObject gameObject)
    {
        // �v�[���ɖ߂��I�u�W�F�N�g�͔�A�N�e�B�u�ɂ��܂�
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // �Փ˂����I�u�W�F�N�g���v�[���ɕԂ�
        if (other.CompareTag("Poolable"))
        {
            // �f�t�H���g�̈ʒu�Ƀ��Z�b�g
            other.transform.position = defaultPosition;
            objectPool.Release(other.gameObject);
            
        }
    }

}
