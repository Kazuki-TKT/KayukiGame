using System;
using UniRx;
using UnityEngine;

public class AokisekiModel : MonoBehaviour
{
    public GameManager _gameManager;
    const int MIN = 0;

    public ReactiveProperty<int> _aokisekiNum = new ReactiveProperty<int>(0);

    void Start()
    {
        _aokisekiNum.Value = MIN;
    }

    public void AddAokiseki(int value)
    {
        if(_gameManager._GameState.Value == GameState.Gaming)
        {
             _aokisekiNum.Value += value;
            if (_aokisekiNum.Value < 0) _aokisekiNum.Value = 0;
        }
        
    }

    public void ResetAokiseki()
    {
        _aokisekiNum.Value = MIN;
    }

    public int GetScore()
    {
        int score = _aokisekiNum.Value;
        return score;
    }

}
