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
        #region �����
        if (currentDialogueList.Count == 0)
        {
            Debug.LogError("��ǰ��δ��ȡ�ı�");
            return;
        }
        if (DialogueView.Instance == null)
        {
            Debug.LogError("�����ϲ����ڶԻ���");
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
    /// ����ö�ټ��ض�Ӧ����
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
                Debug.Log($"���� {spriteName} ͼƬ�ɹ�");
            }
            else
            {
                Debug.LogWarning($"δ�ҵ�����Ϊ {spriteName} ��ͼƬ");
            }
        }
    }

    public void LoadTextDic()
    {
        // ��Resources/JsonFiles�ļ��м�������TextAsset
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>("DialogueData");

        // �������м��ص�TextAsset
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
                Debug.LogError("�ظ��ļ�����");
            }
        }
    }
    /// <summary>
    /// ��ȡ�ı���Ϣ
    /// </summary>
    /// <param name="name">Ҫ��ȡ�ĶԻ��ı�</param>
    public void ReadDialogueList(E_Text e_Text)
    {
        if(!textDic.ContainsKey(e_Text))
        {
            Debug.LogError($"�޷����ҵ� {e_Text} �ı�");
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
