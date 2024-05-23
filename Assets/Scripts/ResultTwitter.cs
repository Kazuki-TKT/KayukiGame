using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTwitter : MonoBehaviour
{
    // ※ここの初期値ではなく、インスペクタ上の値を変えてください
    public string gameID = "kayuki_buruaka"; // unityroom上で投稿したゲームのID
    public string tweetText1 = "獲得した青輝石の数は";
    public string tweetText2 = "個でした。";
    public string hashTags = "#unityroom #蚊ユキ"; //#unity1week";

    // ※ スコアマネージャーというものが存在すると仮定します
    // 貴方の作成しているゲームの事情に合わせてください
    // インスペクタ上でスコアマネージャーコンポーネントを持つゲームオブジェクトをセット
    public AokisekiModel AokisekiModel;

    // ツイートボタンから呼び出す公開メソッド
    public void Tweet()
    {
        int score = AokisekiModel.GetScore(); // ※ 何らかの方法でスコアの値を取得(貴方のゲーム事情に合わせてください)
        naichilab.UnityRoomTweet.Tweet("kayuki_buruaka", "獲得した青輝石の数は" + score + "個でした。" + "#unityroom #蚊ユキ");
    }
}
