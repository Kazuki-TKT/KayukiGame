using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour
{
    public GameStateReactiveProperty _GameState;
    public Timer timer;
    public AokisekiModel aokiseki;
    public Text timerText;
    public Text[] aokisekiText;
    const string ItemText="Clone";
    // Start is called before the first frame update

    private void Awake()
    {
        _GameState.Value = GameState.Title;
    }

    void Start()
    {
        //�c�莞��
        timer.TimerObservable.Subscribe(UpdateTimerUI).AddTo(this);
        //�l���P��
        aokiseki._aokisekiNum.
            Where(x => x>0).
            Subscribe(x
                => AokisekiUI(x)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTimerUI(float timeRemaining)
    {
        // �^�C�}�[��UI���X�V���鏈���������ɒǉ�
        timerText.text = "�c�莞��:" + timeRemaining+ "�b";
        //Debug.Log("�c�莞��: " + timeRemaining);
    }

    private void AokisekiUI(int value)
    {
        aokisekiText[0].text =  value+"��";
        aokisekiText[1].text = value + "��";
    }
    public void ResetItem()
    {
        // �V�[�����̂��ׂĂ�GameObject���擾
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(ItemText))
            {
                // �I�u�W�F�N�g�̖��O�ɓ���̕����񂪊܂܂�Ă���ꍇ
                obj.SetActive(false); // �I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�؂�ւ���
            }
        }
    }

}
