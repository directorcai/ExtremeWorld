using Services;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRegister : MonoBehaviour
{
    // 定义各组件
    private InputField UserName;
    private InputField Password;
    private InputField RePassWord;
    private Button OnClickRegister;
    public GameObject uiLogin;
    void Start()
    {
        // 获取各组件
        UserName = transform.Find("Username/username").GetComponent<InputField>();
        Password = transform.Find("Password/password").GetComponent<InputField>();
        RePassWord = transform.Find("RePassword/repassword").GetComponent<InputField>();
        OnClickRegister = transform.Find("Register").GetComponent<Button>();
        // 监听按钮事件
        OnClickRegister.onClick.AddListener(onClickRegister);
        // 获取服务器端返回消息结果
        UserService.Instance.OnRegister = onRegister;
    }

    void OnRegister(SkillBridge.Message.Result result, string msg) {
        MessageBox.Show(string.Format("结果:{0} msg:{1}", result, msg));
    }

    void Update()
    {
        
    }

    // 监听注册按钮事件
    void onClickRegister() {
        if (string.IsNullOrEmpty(UserName.text)) {
            MessageBox.Show("用户名不能为空！");
            return;
        }
        if (string.IsNullOrEmpty(Password.text)) {
            MessageBox.Show("密码不能为空！");
            return;
        }
        if (string.IsNullOrEmpty(RePassWord.text)) {
            MessageBox.Show("确认密码不能为空！");
            return;
        }
        if (!Password.text.Equals(RePassWord.text)) {
            MessageBox.Show("两次输入密码不一致");
            return;
        }
        // 调用UserService进行注册实现
        UserService.Instance.SendRegister(UserName.text, Password.text);
    }

    void onRegister(Result result, string message)
    {
        if (result == Result.Success)
        {
            //登录成功，进入角色选择
            MessageBox.Show("注册成功,请登录", "提示", MessageBoxType.Information).OnYes = this.CloseRegister;
        }
        else
            MessageBox.Show(message, "错误", MessageBoxType.Error);
    }

    void CloseRegister()
    {
        this.gameObject.SetActive(false);
        uiLogin.SetActive(true);
    }
}
