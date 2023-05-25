using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMessage : Message
{
    public TMP_Text Text;
    private int _clickCount = 0;

    public void OnButtonClick()
    {
        _clickCount++;
        if(_clickCount == 1)
        {
            Text.text = "���������� ��������� ������� �� ������ ������� � ��������� ������.\n������ � �������. ��������, ����� ������! ������� ���?";
        }
        else
        {
            //CloseMessage();
        }
    }
}
