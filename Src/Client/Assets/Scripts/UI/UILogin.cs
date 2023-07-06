using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;
using SkillBridge.Message;

public class UILogin : MonoBehaviour {


    private InputField username;
    private InputField password;
    private Button buttonLogin;

    // Use this for initialization
    void Start () {
        UserService.Instance.OnLogin = OnLogin;
        // 获取各组件
        username = transform.Find("Username/username").GetComponent<InputField>();
        password = transform.Find("PassWord/password").GetComponent<InputField>();
        buttonLogin = transform.Find("Login").GetComponent<Button>();
        // 监听按钮事件
        buttonLogin.onClick.AddListener(OnClickLogin);
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void OnClickLogin()
    {
        if (string.IsNullOrEmpty(this.username.text))
        {
            MessageBox.Show("请输入账号");
            return;
        }
        if (string.IsNullOrEmpty(this.password.text))
        {
            MessageBox.Show("请输入密码");
            return;
        }
        // Enter Game
        UserService.Instance.SendLogin(this.username.text,this.password.text);

    }

    void OnLogin(Result result, string message)
    {
        if (result == Result.Success)
        {
            //登录成功，进入角色选择
            //MessageBox.Show("登录成功,准备角色选择" + message,"提示", MessageBoxType.Information);
            SceneManager.Instance.LoadScene("CharSelect");

        }
        else
            MessageBox.Show(message, "错误", MessageBoxType.Error);
    }
}
