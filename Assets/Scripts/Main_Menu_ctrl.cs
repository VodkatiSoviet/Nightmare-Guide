using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_ctrl : MonoBehaviour
{
    [Header("Alert_Page")]
    public GameObject blocker;
    public GameObject Alert_Page;
    public Text Alert_Msg;
    public Button yes_Btn;
    public Button no_Btn;

    private void Start()
    {
        Debug.Log("�ʱ�ȭ");
        blocker.SetActive(false);
        Alert_Page.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            string msg = "���â �׽�Ʈ";
            Show_Alert_Page(msg);
        }
    }
<<<<<<< Updated upstream
    //���콺�� ���� �޼ҵ� ���ž�?
    //��ư�� �ȴ����ݾ�
=======
>>>>>>> Stashed changes

    //���� �޴� �޼ҵ�
    public void Click_Start_Btn()
    {
        //SceneManager.LoadScene("");
        Debug.Log("���� ����");
    }

    public void Click_Load_Btn()
    {
        Debug.Log("���̺� ���� �ҷ�����");
        Show_Alert_Page("�����");
    }

    public void Click_Setting_Btn()
    {
        Debug.Log("����");
        Show_Alert_Page("�����");
    }

    public void Click_Exit_Btn()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }

    //���â ������

    public void Show_Alert_Page(string msg)
    {
        blocker.SetActive(true);
        Alert_Page.SetActive(true);

        Alert_Msg.text = msg;
    }

    public void Close_Alert_Page()
    {
        blocker.SetActive(false);
        Alert_Page.SetActive(false);
    }

    public void Click_Alert_Yes_Btn()
    {
        Debug.Log("click Yes");
        Close_Alert_Page();
    }

    public void Click_Alert_No_Btn()
    {
        Debug.Log("click No");
        Close_Alert_Page();
    }
}
