using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;

    [SerializeField]
    Button[] _button;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager._GameState.DistinctUntilChanged().
            Where(x => x == GameState.GameOver).
            Subscribe(_ => ScoreWall())
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._GameState.Value == GameState.Gaming) CloseWall();
    }

    void ScoreWall()
    {
        this.gameObject.SetActive(true);
        this.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 1f).OnComplete(()=> {
            foreach (Button button in _button)
            {
                button.interactable = true;
            }
        });

    }

    public void ResetWall()
    {
        foreach (Button button in _button)
        {
            button.interactable = false;
        }
        this.transform.DOLocalMove(new Vector3(0f, 700f, 0f), 0.5f).OnComplete(()=> {
            this.gameObject.SetActive(false);
        });
    }

    void CloseWall()
    {
        foreach (Button button in _button)
        {
            button.interactable = false;
        }
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        this.transform.DOLocalMove(new Vector3(0f, 700f, 0f), 0f);
    }
}
