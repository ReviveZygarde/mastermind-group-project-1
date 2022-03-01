using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerDetector : MonoBehaviour
{
    //Portions of the Answer Detector Code is from https://canvas.colum.edu/courses/26780/files/3736111?wrap=1

    /// <summary>
    /// Create four public GameObject arrays
    /// currentRow holds the four positions that the Game Player clicks for their answers
    /// answerKey holds the four positions that contains the secret code.
    /// pins holds the two prefabs for correct position/color, or correct color only
    /// hintGrid refers to the parent object of the HintGrid.
    /// </summary>
    public GameObject[] currentRow;
    public GameObject[] answerKey;
    public GameObject[] pins;
    public GameObject hintGrid;

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

    void InstantiateCorrectPin(Transform transform)
    {
        GameObject pin = Instantiate(pins[0]);
        pin.transform.position = transform.position;
    }
    void InstantiateWrongPin(Transform transform)
    {
        GameObject pin = Instantiate(pins[1]);
        pin.transform.position = transform.position;
    }

    void Report(Material[] answerMats, Material[] currentRowMats) // begin report
    {
        int[] answerValues = new int[currentRowMats.Length];
        List<Material> compMats = answerMats.ToList();
        List<Color> colorAnswers = new List<Color>();
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

        if(currentRowMats[i].color == answerMats[i].color)
        {
            answerValues[i] = 1;
            InstantiateCorrectPin(hintGrid.transform.GetChild(i).transform);
        }
        else if (colorAnswers.Contains(currentRowMats[i].color))
        {
            answerValues[i] = 0;
            InstantiateWrongPin(hintGrid.transform.GetChild(i).transform);
        }
        else
        {
            answerValues[i] = 1;
        }
    } //end report
}
