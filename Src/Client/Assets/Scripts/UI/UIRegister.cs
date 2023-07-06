using Services;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRegister : MonoBehaviour
{
    // ��������
    private InputField UserName;
    private InputField Password;
    private InputField RePassWord;
    private Button OnClickRegister;
    public GameObject uiLogin;
    void Start()
    {
        // ��ȡ�����
        UserName = transform.Find("Username/username").GetComponent<InputField>();
        Password = transform.Find("Password/password").GetComponent<InputField>();
        RePassWord = transform.Find("RePassword/repassword").GetComponent<InputField>();
        OnClickRegister = transform.Find("Register").GetComponent<Button>();
        // ������ť�¼�
        OnClickRegister.onClick.AddListener(onClickRegister);
        // ��ȡ�������˷�����Ϣ���
        UserService.Instance.OnRegister = onRegister;
    }

    void OnRegister(SkillBridge.Message.Result result, string msg) {
        MessageBox.Show(string.Format("���:{0} msg:{1}", result, msg));
    }

    void Update()
    {
        
    }

    // ����ע�ᰴť�¼�
    void onClickRegister() {
        if (string.IsNullOrEmpty(UserName.text)) {
            MessageBox.Show("�û�������Ϊ�գ�");
            return;
        }
        if (string.IsNullOrEmpty(Password.text)) {
            MessageBox.Show("���벻��Ϊ�գ�");
            return;
        }
        if (string.IsNullOrEmpty(RePassWord.text)) {
            MessageBox.Show("ȷ�����벻��Ϊ�գ�");
            return;
        }
        if (!Password.text.Equals(RePassWord.text)) {
            MessageBox.Show("�����������벻һ��");
            return;
        }
        // ����UserService����ע��ʵ��
        UserService.Instance.SendRegister(UserName.text, Password.text);
    }

    void onRegister(Result result, string message)
    {
        if (result == Result.Success)
        {
            //��¼�ɹ��������ɫѡ��
            MessageBox.Show("ע��ɹ�,���¼", "��ʾ", MessageBoxType.Information).OnYes = this.CloseRegister;
        }
        else
            MessageBox.Show(message, "����", MessageBoxType.Error);
    }

    void CloseRegister()
    {
        this.gameObject.SetActive(false);
        uiLogin.SetActive(true);
    }
}
