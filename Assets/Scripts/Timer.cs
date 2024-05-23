using System;
using UniRx;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager _gameManager;

    public float initialTime = 60.0f; // 初期の制限時間（秒）
    public float currentTime; // 現在の制限時間（秒）

    private Subject<float> timerSubject = new Subject<float>();

    public IObservable<float> TimerObservable => timerSubject.AsObservable();

    private IDisposable timerDisposable;

    private void Start()
    {
        ResetTimer();
    }

    public void ResetTimer()
    {
        currentTime = initialTime;
        timerSubject.OnNext(currentTime); // 初期時間をUIに反映
        //StartCountdown();
    }

    public void AddTime(float time)
    {
        if (_gameManager._GameState.Value == GameState.Gaming)
            currentTime += time;
    }

    public void StartCountdown()
    {
        if (timerDisposable != null)
        {
            timerDisposable.Dispose();
        }

        timerDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
            .TakeWhile(_ => currentTime > 0)
            .Subscribe(_ =>
            {
                currentTime -= 1;
                timerSubject.OnNext(currentTime);
            }, () =>
            {
                // 制限時間終了時の処理をここに追加
                _gameManager._GameState.Value = GameState.GameOver;
                timerSubject.OnNext(0); // タイマーの値を0に設定
            });
    }

    private void OnDestroy()
    {
        if (timerDisposable != null)
        {
            timerDisposable.Dispose();
        }
        timerSubject.Dispose();
    }

}
