using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;

    [SerializeField]
    Timer _timer;

    [SerializeField]
    Text _countText;

    [SerializeField]
    AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager._GameState
            .DistinctUntilChanged()
            .Where(x => x == GameState.GameCount)
            .Subscribe(_ => GameStartCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameStartCountDown()
    {
        StartCoroutine(CountDownCoroutin());
    }

    IEnumerator CountDownCoroutin()
    {
        _audioManager.PlaySE(SESoundData.SE.Count);
        _countText.text = "-3-";
        yield return new WaitForSeconds(1);
        _audioManager.PlaySE(SESoundData.SE.Count);
        _countText.text = "-2-";
        yield return new WaitForSeconds(1);
        _audioManager.PlaySE(SESoundData.SE.Count);
        _countText.text = "-1-";
        yield return new WaitForSeconds(1);
        _audioManager.PlaySE(SESoundData.SE.CountStart);
        _countText.text = "-START-";
        yield return new WaitForSeconds(1);
        _countText.text = "";
        _gameManager._GameState.Value = GameState.Gaming;
        _timer.StartCountdown();
    }
}
