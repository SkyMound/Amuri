using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LoadText : MonoBehaviour
{

    private Text uiText;

    [SerializeField]private float showSpeed = 0.05f;
    [SerializeField]private float showCommaSpeed = 1f;
    [SerializeField]private float showPointSpeed = 2f;

    private string showText, uiTextCopy;

    private bool coroutineProtect, loadText;


    private void Start()
    {
        uiText = GetComponent<Text>();

        TextInformations();
    }

    private void OnEnable() { uiTextCopy = null; }

    private void Update()
    {
        if (loadText && !coroutineProtect)
        {
            StartCoroutine(LoadLetters(uiTextCopy));
            coroutineProtect = true;
        }

        else if (loadText && coroutineProtect) { uiText.text = showText; }

        else if (!loadText && !coroutineProtect)
        {
            if (uiText.text != uiTextCopy) { TextInformations(); }
        }
    }

    private void TextInformations()
    {
        uiTextCopy = uiText.text;
        showText = null;
        uiText.text = null;

        loadText = true;
        coroutineProtect = false;
    }

    private IEnumerator LoadLetters(string completeText)
    {
        int textSize = 0;

        while (textSize < completeText.Length)
        {
            
            showText += completeText[textSize++];
            if(completeText[textSize-1]==',')
            {
                yield return new WaitForSeconds(showCommaSpeed);
            }
            else if(completeText[textSize-1]=='.')
            {
                yield return new WaitForSeconds(showPointSpeed);
            }
            else 
            {
                yield return new WaitForSeconds(showSpeed);
            }
        }

        coroutineProtect = false;
        loadText = false;
    }

}