using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerDetector : MonoBehaviour
{
    public GameObject[] currentRow;
    public GameObject[] answerKey;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evaluate();
        }
    }
    private void Evaluate()
    {
        Material[] answerMats = new Material[answerKey.Length];
        for (int i = 0; i < answerKey.Length; i++)
        {
            Material temp = answerKey[i].GetComponent<MeshRenderer>().material;
            answerMats[i] = temp;
        }

        Material[] currentRowMats = new Material[answerKey.Length];
        for (int i = 0; i < currentRow.Length; i++)
        {
            Material temp = currentRow[i].GetComponent<MeshRenderer>().material;
            answerMats[i] = temp;
        }

        Report(answerMats, currentRowMats);
    }

    void Report(Material[] answerMats, Material[] currentRowMats) // begin report
    {
        for (int i = 0; i < answerMats.Length; i++)
        {
            int[] answers = new int[answerMats.Length];

            if (answerMats[i] == currentRow[i])
            {
                answers[i] = 1;
                Debug.Log($"{answerMats[i]} is the same in both");
            }
            else
            {
                answers[i] = -1;
                Debug.Log($"{answerMats[i]} does not match {currentRowMats[i]}.");
                if (currentRowMats.Contains(answerMats[i]))
                {
                    answers[i] = 0;
                }
                else
                {
                    answers[i] = -1;
                }
            }
        }
        for (int i = 0; i < answerMats.Length; i++)
        {
            Debug.Log($" At index No. {i}, nums is {answerMats[i]}, and num2 is {currentRowMats[i]}.");
        }
    } //end report
}
