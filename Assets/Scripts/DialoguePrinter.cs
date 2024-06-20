using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialoguePrinter : MonoBehaviour
{
    public static DialoguePrinter Instance {  get; private set; }
    [SerializeField] private TMP_Text _dialogueTextMesh;
    [SerializeField] private Canvas examineCanvas;

    void Awake()
    {
        Instance = this;
    }

    public void PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        StartCoroutine(CO_PrintDialogueLine(lineToPrint, charSpeed, finishedCallback));
        Debug.Log("Examinar Ejecutado");
    }

    private IEnumerator CO_PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _dialogueTextMesh.SetText(string.Empty);
        examineCanvas.enabled = true;
        for (int i = 0; i < lineToPrint.Length; i++)
        {
            var character = lineToPrint[i];
            _dialogueTextMesh.SetText(_dialogueTextMesh.text + character);
            yield return new WaitForSeconds(charSpeed);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        examineCanvas.enabled = false;
        _dialogueTextMesh.SetText(string.Empty);
        finishedCallback?.Invoke();
 
        yield return null;
    }
}
