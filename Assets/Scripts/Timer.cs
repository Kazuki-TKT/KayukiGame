using System;
using UniRx;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager _gameManager;

    public float initialTime = 60.0f; // �����̐������ԁi�b�j
    public float currentTime; // ���݂̐������ԁi�b�j

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
        timerSubject.OnNext(currentTime); // �������Ԃ�UI�ɔ��f
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
                // �������ԏI�����̏����������ɒǉ�
                _gameManager._GameState.Value = GameState.GameOver;
                timerSubject.OnNext(0); // �^�C�}�[�̒l��0�ɐݒ�
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
