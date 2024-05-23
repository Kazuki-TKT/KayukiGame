using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : MonoBehaviour,GetItemIO
{
    [SerializeField]
    float addTime;

    Timer _timer;
    void Start()
    {
        _timer = GameObject.Find("GameManager").GetComponent<Timer>();
    }
    public void GetItem()
    {
        _timer.AddTime(addTime);
    }
}
