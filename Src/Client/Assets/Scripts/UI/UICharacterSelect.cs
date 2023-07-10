using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Models;
using Services;
using SkillBridge.Message;
using Common.Data;

public class UICharacterSelect : MonoBehaviour {

    public GameObject panelCreate;
    public GameObject panelSelect;

    public GameObject btnCreateCancel;

    public InputField charName;
    CharacterClass charClass;

    public Transform uiCharList;
    public GameObject uiCharInfo;

    public List<GameObject> uiChars = new List<GameObject>();

    public Image[] titles;

    public Text[] descs;

    private int selectCharacterIdx = -1;

    public UICharacterView characterView;

    // 创建游戏角色按钮
    private Button ButtonPlay;
    // 创建游戏角色输入框组件
    private InputField inputName;

    // Use this for initialization
    void Start()
    {
        InitCharacterSelect(true);
        // 初始化角色选择列表
        UserService.Instance.OnCharacterCreate = OnCharacterCreate;
        // 获取创建角色按钮组件
        ButtonPlay = transform.Find("PanelCreate/ButtonPlay").GetComponent<Button>();
        // 获取游戏角色输入框组件
        inputName = transform.Find("PanelCreate/InputName").GetComponent<InputField>();
        // 监听按钮
        ButtonPlay.onClick.AddListener(OnClickCreate);
    }


    public void InitCharacterSelect(bool init)
    {
        panelCreate.SetActive(false);
        panelSelect.SetActive(true);

        if (init)
        {
            foreach (var old in uiChars)
            {
                Destroy(old);
            }
            uiChars.Clear();

            for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
            {

                GameObject go = Instantiate(uiCharInfo, this.uiCharList);
                UICharInfo chrInfo = go.GetComponent<UICharInfo>();
                chrInfo.info = User.Instance.Info.Player.Characters[i];

                Button button = go.GetComponent<Button>();
                int idx = i;
                button.onClick.AddListener(() => {
                    OnSelectCharacter(idx);
                });

                uiChars.Add(go);
                go.SetActive(true);
            }
        }
    }

    public void InitCharacterCreate()
    {
        panelCreate.SetActive(true);
        panelSelect.SetActive(false);
        OnSelectClass(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickCreate()
    {
        // 首先判断输入游戏角色名称是否为空
        if (string.IsNullOrEmpty(inputName.text)) {
            MessageBox.Show("请先输入游戏角色名称吧！");
            return;
        }
        // 进行游戏角色创建逻辑
        UserService.Instance.sendCreateCharacter(inputName.text, charClass);
        // 返回注册选择页面先初始化
        InitCharacterSelect(true);
    }

    public void OnSelectClass(int charClass)
    {
        this.charClass = (CharacterClass)charClass;
        characterView.CurrentCharacter = charClass - 1;

        for (int i = 0; i < titles.Length; i++) {
            titles[i].gameObject.SetActive(i == charClass - 1);
            descs[i].text = DataManager.Instance.Characters[charClass].Description;
        }
    }


    void OnCharacterCreate(Result result, string message)
    {
        if (result == Result.Success)
        {
            InitCharacterSelect(true);

        }
        else
            MessageBox.Show(message, "错误", MessageBoxType.Error);
    }

    public void OnSelectCharacter(int idx)
    {
        this.selectCharacterIdx = idx;
        var cha = User.Instance.Info.Player.Characters[idx];
        Debug.LogFormat("Select Char:[{0}]{1}[{2}]", cha.Id, cha.Name, cha.Class);
        User.Instance.CurrentCharacter = cha;
        characterView.CurrentCharacter = idx;
        for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
        {
            UICharInfo ci = uiChars[i].GetComponent<UICharInfo>();
            ci.Selected = idx == i;
        }
    }
    public void OnClickPlay()
    {
        if (selectCharacterIdx >= 0)
        {
            MessageBox.Show("进入游戏", "进入游戏", MessageBoxType.Confirm);
        }
    }
}
