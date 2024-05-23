using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour
{
    public GameStateReactiveProperty _GameState;
    public Timer timer;
    public AokisekiModel aokiseki;
    public Text timerText;
    public Text[] aokisekiText;
    const string ItemText="Clone";
    // Start is called before the first frame update

    private void Awake()
    {
        _GameState.Value = GameState.Title;
    }

    void Start()
    {
        //残り時間
        timer.TimerObservable.Subscribe(UpdateTimerUI).AddTo(this);
        //獲得青輝石
        aokiseki._aokisekiNum.
            Where(x => x>0).
            Subscribe(x
                => AokisekiUI(x)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTimerUI(float timeRemaining)
    {
        // タイマーのUIを更新する処理をここに追加
        timerText.text = "残り時間:" + timeRemaining+ "秒";
        //Debug.Log("残り時間: " + timeRemaining);
    }

    private void AokisekiUI(int value)
    {
        aokisekiText[0].text =  value+"個";
        aokisekiText[1].text = value + "個";
    }
    public void ResetItem()
    {
        // シーン内のすべてのGameObjectを取得
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(ItemText))
            {
                // オブジェクトの名前に特定の文字列が含まれている場合
                obj.SetActive(false); // オブジェクトのアクティブ状態を切り替える
            }
        }
    }

}
