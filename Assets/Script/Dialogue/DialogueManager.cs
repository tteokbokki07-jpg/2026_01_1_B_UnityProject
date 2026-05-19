using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소")]
    public GameObject DialoguePanal;
    public Image characterImage;
    public TextMeshProUGUI characternameText;
    public TextMeshProUGUI DialogueText;
    public Button nextButton;

    [Header("기본 설정")]
    public Sprite defaultCharacterImage;
    [Header("타이핑 효과 설정")]
    public float typingSpeed = 0.05f;
    public bool skipTypingOnClick = true;

    //내부 변수
    private DialogueDataSO currentDialogue;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        DialoguePanal.SetActive(false);
        nextButton.onClick.AddListener(HandleNextInput);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleNextInput();
        }
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        DialogueText.text = "";

        for (int i = 0; i < textToType.Length; i++)
        {
            DialogueText.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        
    }

    private void CompleteTyping()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        isTyping = false;

        if(currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            DialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        }
    }
    void ShowCurrentLine()
    {
        if(currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            string currentText = currentDialogue.dialogueLines[currentLineIndex];
            typingCoroutine = StartCoroutine(TypeText(currentText));
        }
    }
    public void ShowNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }
    void EndDialogue()
    {
        if (typingCoroutine == null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        isDialogueActive = false;
        isTyping = false;
        DialoguePanal.SetActive(false);
        currentLineIndex = 0;
    }
    public void HandleNextInput()
    {
        if (isTyping && skipTypingOnClick)
        {
            CompleteTyping();
        }
        else if (!isTyping)
        {
            ShowNextLine();
        }
    }
    public void SkipDialogue()
    {
        EndDialogue() ;
    }
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
    public void StartDialogue(DialogueDataSO dialogueData)
    {
        if (dialogueData == null || dialogueData.dialogueLines.Count == 0) return;

        currentDialogue = dialogueData;
        currentLineIndex = 0;
        isDialogueActive = true;
        
        DialoguePanal.SetActive(true);
        characternameText.text = dialogueData.characterName;
        if (characterImage.sprite != null )
        {
            if (dialogueData.characterImage != null)
            {
                characterImage.sprite = dialogueData.characterImage;
            }
            else
            {
                characterImage.sprite = defaultCharacterImage;
            }
        }
        ShowCurrentLine();
    }
}
