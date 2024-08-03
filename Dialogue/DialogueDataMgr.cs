using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.Events;

public class DialogueDataMgr : BaseManager<DialogueDataMgr>
{
    private Dictionary<E_Character,Sprite> characterSpriteDic = new Dictionary<E_Character,Sprite>();
    private Dictionary<E_Text, List<DialogueData>> textDic = new Dictionary<E_Text, List<DialogueData>>();

    private List<DialogueData> currentDialogueList;
    private int currentIndex;

    public bool dialogueIsOn = false;

    private UnityAction eventInEnd;
    private UnityAction eventInStart;

    DialogueDataMgr()
    {
        LoadCharacterSprites();
        LoadTextDic();
        MonoMgr.Instance.AddUpdateListener(CheckInput);
    }

    public void AddListenerEnd(UnityAction action)
    {
        eventInEnd += action;
    }
    public void RemoveListenerEnd(UnityAction action)
    {
        eventInEnd -= action;
    }

    public void AddListenerStart(UnityAction action)
    {
        eventInStart += action;
    }
    public void RemoveListenerStart(UnityAction action)
    {
        eventInStart -= action;
    }
    public void NextDialogue()
    {
        #region 查错部分
        if (currentDialogueList.Count == 0)
        {
            Debug.LogError("当前并未读取文本");
            return;
        }
        if (DialogueView.Instance == null)
        {
            Debug.LogError("场景上不存在对话框");
            return;
        }
        if(!DialogueView.Instance.gameObject.activeInHierarchy)
        {
            dialogueIsOn = true;
            eventInStart?.Invoke();
            eventInStart = null;
            DialogueView.Instance.Show();
        }
        #endregion
        if (currentIndex == -1)
        {
            dialogueIsOn = false;
            DialogueView.Instance.Hide();
            eventInEnd?.Invoke();
            eventInEnd = null;
            return;
        }
        DialogueView.Instance.UpdateDialogue(
            currentDialogueList[currentIndex], 
            characterSpriteDic[Enum.Parse<E_Character>(currentDialogueList[currentIndex].characterL)], 
            characterSpriteDic[Enum.Parse<E_Character>(currentDialogueList[currentIndex].characterR)]
            );
        currentIndex = currentDialogueList[currentIndex].nextId;
    }

    public void CheckInput()
    {
        if(dialogueIsOn&&!DialogueView.Instance.IsTyping)
        {
            if(Input.GetMouseButtonDown(0))
            {
                NextDialogue();
            }
        }
    }

    /// <summary>
    /// 根据枚举加载对应立绘
    /// </summary>
    public void LoadCharacterSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("CharacterImage");

        foreach (E_Character character in Enum.GetValues(typeof(E_Character)))
        {
            string spriteName = character.ToString();
            Sprite characterSprite = Array.Find(sprites, sprite => sprite.name == spriteName);
            if (characterSprite != null)
            {
                characterSpriteDic[character] = characterSprite;
                Debug.Log($"加载 {spriteName} 图片成功");
            }
            else
            {
                Debug.LogWarning($"未找到名称为 {spriteName} 的图片");
            }
        }
    }

    public void LoadTextDic()
    {
        // 从Resources/JsonFiles文件夹加载所有TextAsset
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("DialogueData");

        // 遍历所有加载的TextAsset
        foreach (TextAsset jsonFile in jsonFiles)
        {
            E_Text @enum = Enum.Parse<E_Text>(jsonFile.name);
            if (!textDic.ContainsKey(@enum))
            {
                textDic.Add(@enum, JsonMapper.ToObject<List<DialogueData>>(jsonFile.text));
                Debug.Log(textDic.Count);
            }
            else
            {
                Debug.LogError("重复文件加载");
            }
        }
    }
    /// <summary>
    /// 读取文本信息
    /// </summary>
    /// <param name="name">要读取的对话文本</param>
    public void ReadDialogueList(E_Text e_Text)
    {
        if(!textDic.ContainsKey(e_Text))
        {
            Debug.LogError($"无法查找到 {e_Text} 文本");
            return;
        }
        currentDialogueList = textDic[e_Text];
        currentIndex = 0;
        NextDialogue();
    }
}

public class DialogueData
{
    public int id;

    public string characterL;
    public string characterR;

    public bool isLeftSpeak;

    public string text;
    public int nextId;
}
