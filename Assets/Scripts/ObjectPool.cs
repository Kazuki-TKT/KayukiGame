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


    public GameObject poolObj;  // プールするゲームオブジェクトのプレハブ
    public int poolSize = 10;  // プールするオブジェクトの初期サイズ

    private ObjectPool<GameObject> objectPool;  // ゲームオブジェクトのプール

    // 0になったらBlockオブジェクトを生成
    float _timer = 0;
    // トータルの経過時間を保持
    float _totalTime = 0;
    // ①ブロック生成回数
    int _cnt = 0;

    private Vector3 defaultPosition;

    [SerializeField]
    float setSpeed = 100;
    private void Start()
    {
        defaultPosition = transform.position;
        // オブジェクトプールの作成
        objectPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(poolObj),          // プールが空のときに新しいインスタンスを生成する処理
            actionOnGet: OnTakeFromPool,        // インスタンスがプールから取り出されたときに呼び出される処理
            actionOnRelease: OnReturnedToPool,   // インスタンスがプールに戻されるときに呼び出される処理
            actionOnDestroy: null,                          // プールがmaxSizeに達した際、要素をプールに戻せなかったときに呼び出される処理
            collectionCheck: false,                         // プールに戻す際に既に同一インスタンスが登録されているか調べ、あれば例外を投げる。エディタでのみ実行されることに注意
            defaultCapacity: poolSize,                      // デフォルトの容量
            maxSize: 10);                                  // プールの最大サイズ

    }

    private void Update()
    {
        if (_gameManager._GameState.Value == GameState.Gaming)
        {
            // 経過時間を差し引く
            _timer -= Time.deltaTime;
            // ②トータル時間を加算
            _totalTime += Time.deltaTime;

            if (_timer < 0)
            {
                Vector3 position = transform.position;
                position.y = Random.Range(-3, 4);
                GameObject obj = objectPool.Get();
                obj.transform.position = new Vector3(defaultPosition.x, position.y, defaultPosition.z);
                BlockSpeedIO blockScript = obj.GetComponentInChildren<BlockSpeedIO>();
                blockScript.SetSpeed(-setSpeed); // 左方向なのでマイナス
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


    // プールからオブジェクトを取得する時に呼び出されます
    private void OnTakeFromPool(GameObject gameObject)
    {
        // プールから取得したオブジェクトをアクティブにします
        gameObject.SetActive(true);
        
        // プールから取得したオブジェクトを 2 秒後にプールに戻すコルーチン
        IEnumerator Process()
        {
            yield return new WaitForSeconds(11);
            objectPool.Release(gameObject);
        }

        // プールから取得したオブジェクトを 2 秒後にプールに戻すコルーチンを実行します
        StartCoroutine(Process());
    }
    // プールにオブジェクトを戻す時に呼び出されます
    private void OnReturnedToPool(GameObject gameObject)
    {
        // プールに戻すオブジェクトは非アクティブにします
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトをプールに返す
        if (other.CompareTag("Poolable"))
        {
            // デフォルトの位置にリセット
            other.transform.position = defaultPosition;
            objectPool.Release(other.gameObject);
            
        }
    }

}
