using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI tmpNom;
    public TextMeshProUGUI tmpPhrase;
    public List<Dialogue> dialogues;
    private int indexDialogue;
    private int indexPhrase;
    private float timePerCharacter;

    [Header("Debug")]
    [SerializeField]
    Animator animatorFade;
    [SerializeField]
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue ()
    {
        indexDialogue = 0;
        indexPhrase = 0;
        timePerCharacter = 3;
        DrawDialogue();
    }

    private void DrawDialogue()
    {
        string name = dialogues[indexDialogue].name;
        string phrase = dialogues[indexDialogue].sentences[indexPhrase];

        StartCoroutine(DrawSentence(phrase));
        tmpNom.text = name;
        tmpPhrase.text = phrase;
    }

    private IEnumerator DrawSentence(string sentence)
    {
        tmpPhrase.maxVisibleCharacters = 0;

        while(tmpPhrase.maxVisibleCharacters < sentence.Length)
        {
            tmpPhrase.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.1f);
            if (Input.GetMouseButton(0))
            {
                tmpPhrase.maxVisibleCharacters = sentence.Length;
            }
        }
    }

    public void nextDialogue()
    {
        indexPhrase++;
        if (indexPhrase >= dialogues[indexDialogue].sentences.Length)
        {
            indexPhrase = 0;
            indexDialogue++;
            if(indexDialogue >= dialogues.Count)
            {
                EndDialogue();
                return;
            }
        }
        DrawDialogue();
    }

    public void EndDialogue()
    {
        //TODO
        StartCoroutine(EndDialogueCoroutine());
    }

    // debug
    private IEnumerator EndDialogueCoroutine()
    {
        animatorFade.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
