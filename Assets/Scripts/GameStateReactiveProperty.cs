using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum GameState
{
    None,
    Title,
    Gaming,
    GameOver,
    Result,
    GameCount
}

[System.Serializable]

public class GameStateReactiveProperty : ReactiveProperty<GameState>

{

    public GameStateReactiveProperty() { }

    public GameStateReactiveProperty(GameState initialValue) : base(initialValue) { }

}
