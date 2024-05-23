using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AokisekiItem : MonoBehaviour, GetItemIO
{
    [SerializeField]
    int _value;

    AokisekiModel _aokiseki;
    void Start()
    {
        _aokiseki = GameObject.Find("GameManager").GetComponent<AokisekiModel>();
    }
    public void GetItem()
    {
        _aokiseki.AddAokiseki(_value);
    }
}
