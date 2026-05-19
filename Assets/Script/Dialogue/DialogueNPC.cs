using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO MyDialogue;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindAnyObjectByType<DialogueManager>();

    }

    void OnMouseDown()
    {
        if (dialogueManager == null) return;
        if (dialogueManager.IsDialogueActive()) return;
        if (MyDialogue == null) return;
        dialogueManager.StartDialogue(MyDialogue);
        
    }
}
