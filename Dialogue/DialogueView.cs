using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : SingletonMono<DialogueView>
{
    private DialogueData dialogueData;
    [SerializeField]
    private TMP_Text nameL;
    [SerializeField]
    private TMP_Text nameR;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private Image CharacterL;
    [SerializeField]
    private Image CharacterR;

    private Color defalutColor;
    private Color greyColor;
    private Color greyColorForImage;

    [SerializeField]
    private float typeSpeed = 1f;
    [SerializeField]
    private float delayClickble = 1f;

    //监测是否正在打字输出
    private bool isTyping;
    public bool IsTyping => isTyping;
    private bool flag = true;

    private Coroutine coroutine;

    protected override void Awake()
    {
        base.Awake();
        defalutColor = nameL.color;
        greyColor = new Vector4(defalutColor.r * 0.5f, defalutColor.g * 0.5f, defalutColor.b * 0.5f, defalutColor.a);
        greyColorForImage = new Vector4(Color.white.r * 0.5f, Color.white.g * 0.5f, Color.white.b * 0.5f, Color.white.a);
        DontDestroyOnLoad(this.gameObject);
        Hide();
    }

    private void Update()
    {
        if(coroutine != null&&isTyping&&Input.GetMouseButtonDown(0)&&!flag)
        {
            StopCoroutine(coroutine);
            StopCoroutine(DelayClickble());
            dialogueText.text = dialogueData.text;
            coroutine = null;
            flag = true;
            StartCoroutine(DelayClickble());
        }
    }

    public void Hide()
    {
        DialogueDataMgr.Instance.dialogueIsOn = false;
        this.gameObject.SetActive(false);
    }
    public void Show()
    {
        DialogueDataMgr.Instance.dialogueIsOn = true;
        this.gameObject.SetActive(true); 
    }
    public void UpdateDialogue(DialogueData dialogueData,Sprite CharacterL,Sprite CharacterR)
    {
        this.dialogueData = dialogueData;
        this.nameL.text = dialogueData.characterL;
        this.nameR.text = dialogueData.characterR;
        coroutine = StartCoroutine(TypeText(this.dialogueText, dialogueData.text));
        if(this.CharacterL.sprite != CharacterL)
        {
            this.CharacterL.sprite = CharacterL;
            this.CharacterL.SetNativeSize();
        }    
        if (this.CharacterL.sprite != CharacterR)
        {
            this.CharacterR.sprite = CharacterR;
            this.CharacterR.SetNativeSize();
        }
        if (dialogueData.isLeftSpeak)
        {
            this.nameR.color = greyColor;
            this.nameL.color = defalutColor;
            this.CharacterL.color = Color.white;
            this.CharacterR.color = greyColorForImage;
        }
        else
        {
            this.nameR.color = defalutColor;
            this.nameL.color = greyColor;
            this.CharacterL.color = greyColorForImage;
            this.CharacterR.color = Color.white;
        }
    }
    /// <summary>
    /// 逐字显示
    /// </summary>
    /// <param name="TmpText">需要逐字显示的文本框</param>
    /// <param name="text">要写入的文本</param>
    /// <returns></returns>
    IEnumerator TypeText(TMP_Text TmpText,string text)
    {
        isTyping = true;
        TmpText.text = "";
        StartCoroutine(DelayTemp());
        foreach (char letter in text.ToCharArray())
        {
            TmpText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
        coroutine = null;
    }

    IEnumerator DelayClickble()
    {
        yield return new WaitForSeconds(delayClickble);
        isTyping = false;
    }

    IEnumerator DelayTemp()
    {
        yield return new WaitForSeconds(0.2f);
        flag = false;
    }
}
