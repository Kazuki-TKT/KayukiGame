using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{

    [SerializeField]
    CanvasGroup _canvasGroup;

    [SerializeField]
    GameManager _gameManager;

    [SerializeField]
    GameObject _titleObj;

    [SerializeField]
    GameObject _gamingObj;

    [SerializeField]
    GameObject _playerObj;
    public void FadeINtoGame()
    {
        _canvasGroup.DOFade(1, 0.7f).OnComplete(FadeOUTtoGame);
    }

    public void FadeOUTtoGame()
    {
        _titleObj.SetActive(false);
        _gamingObj.SetActive(true);
        _canvasGroup.DOFade(0, 0.7f).SetDelay(1f).OnComplete(()=> {
            _gameManager._GameState.Value = GameState.GameCount;
            
        });
    }

    public void FadeINtoTitle()
    {
        _canvasGroup.DOFade(1, 0.7f).OnComplete(FadeOUTtoTitle);
    }

    public void FadeOUTtoTitle()
    {
        _titleObj.SetActive(true);
        _gamingObj.SetActive(false);
        _playerObj.transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
        _canvasGroup.DOFade(0, 0.7f).SetDelay(1f).OnComplete(() => {
            _gameManager._GameState.Value = GameState.Title;

        });
    }

    public void ReStartGameFadeIN()
    {
        _canvasGroup.DOFade(1, 0.7f).OnComplete(ReStartGameFadeOUT);
    }

    public void ReStartGameFadeOUT()
    {
        _playerObj.transform.DOLocalMove(new Vector3(- 3, 0, 0), 0.5f);
        _canvasGroup.DOFade(0, 0.7f).SetDelay(1f).OnComplete(() => {
            _gameManager._GameState.Value = GameState.GameCount;

        });
    }
}
