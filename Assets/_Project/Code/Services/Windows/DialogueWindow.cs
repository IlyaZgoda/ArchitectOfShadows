using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Services.Windows
{
    public class DialogueWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] string[] lines;
        [SerializeField] private float speed;
        private int index;

        private void Start()
        {
            text.text = string.Empty;
            StartDialogue();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (text.text == lines[index])
                    NextLine();

                else
                {
                    StopAllCoroutines();
                    text.text = lines[index];
                }           
            }
        }

        private void StartDialogue()
        {
            index = 0;
            StartCoroutine(TypeLine());
        }

        private IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                text.text += c;
                yield return new WaitForSeconds(speed);
            }
        }

        private void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                text.text =string.Empty;

                StartCoroutine(TypeLine());
            }

            else
                gameObject.SetActive(false);
        }
    }
}
