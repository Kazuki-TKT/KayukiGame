using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTwitter : MonoBehaviour
{
    // �������̏����l�ł͂Ȃ��A�C���X�y�N�^��̒l��ς��Ă�������
    public string gameID = "kayuki_buruaka"; // unityroom��œ��e�����Q�[����ID
    public string tweetText1 = "�l�������P�΂̐���";
    public string tweetText2 = "�ł����B";
    public string hashTags = "#unityroom #�჆�L"; //#unity1week";

    // �� �X�R�A�}�l�[�W���[�Ƃ������̂����݂���Ɖ��肵�܂�
    // �M���̍쐬���Ă���Q�[���̎���ɍ��킹�Ă�������
    // �C���X�y�N�^��ŃX�R�A�}�l�[�W���[�R���|�[�l���g�����Q�[���I�u�W�F�N�g���Z�b�g
    public AokisekiModel AokisekiModel;

    // �c�C�[�g�{�^������Ăяo�����J���\�b�h
    public void Tweet()
    {
        int score = AokisekiModel.GetScore(); // �� ���炩�̕��@�ŃX�R�A�̒l���擾(�M���̃Q�[������ɍ��킹�Ă�������)
        naichilab.UnityRoomTweet.Tweet("kayuki_buruaka", "�l�������P�΂̐���" + score + "�ł����B" + "#unityroom #�჆�L");
    }
}
