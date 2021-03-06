using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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
    private UnityEvent eventPlayer;

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
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                tmpPhrase.maxVisibleCharacters = sentence.Length;
            }
        }
        yield return null;
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        yield return null;
        nextDialogue();
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
        StopAllCoroutines();
        eventPlayer.Invoke();
    }

    private IEnumerator EndDialogueCoroutine(string nomScene)
    {
        animatorFade.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nomScene);
    }

    public void LoadScene(string nomScene)
    {
        StartCoroutine(EndDialogueCoroutine(nomScene));
    }
}
