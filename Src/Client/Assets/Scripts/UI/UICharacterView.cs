using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterView : MonoBehaviour
{

    // ����һ������洢��ɫ
    public GameObject[] characters;
    // Ĭ�ϵ�ǰսʿ
    private int currentCharacter = 0;
    public int CurrentCharacter
    {
        get
        {
            return currentCharacter;
        }
        set 
        { 
            currentCharacter = value;
            UpdateCharacter();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCharacter() {
        for (int i = 0; i < characters.Length; i++) {
            characters[i].SetActive(i == currentCharacter);
        }
    }
}
