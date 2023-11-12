using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{

    
    [SerializeField] private DreamManager dream;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator dialogAnimator;
    [SerializeField] private GameObject tipsSHIFT;
    [SerializeField] private GameObject tipsE;
    [SerializeField] private GameObject tipsMouvement;

    private int fallCount;
    private bool shouldSleep;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
        fallCount = 0;
        shouldSleep = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldSleep)
        {
            dream.isSleeping= true;
        }
    }

    public IEnumerator inFrontOfDoor()
    {

        tipsSHIFT.SetActive(false);

        shouldSleep = true;
        dialogAnimator.SetTrigger("step11");
        yield return new WaitForSeconds(4);
        dialogAnimator.SetTrigger("step12");
        dream.isSleeping = false;
        tipsE.SetActive(true);
    }

    public IEnumerator hasFall()
    {
        fallCount++;
        if(fallCount == 1)
        {
            tipsMouvement.SetActive(false);
            shouldSleep = true;
            dialogAnimator.SetTrigger("step5");
            yield return new WaitForSeconds(2);
            dialogAnimator.SetTrigger("step6");
            yield return new WaitForSeconds(1);
            shouldSleep = false;
            dream.isSleeping = false;

        }
        else if(fallCount == 2)
        {
            shouldSleep = true;
            dialogAnimator.SetTrigger("step7");
            yield return new WaitForSeconds(2);
            dialogAnimator.SetTrigger("step8");
            yield return new WaitForSeconds(1);
            dialogAnimator.SetTrigger("step9");
            yield return new WaitForSeconds(4);
            dialogAnimator.SetTrigger("step10");
            yield return new WaitForSeconds(1);
            dream.isSleeping = false;
            shouldSleep = false;

            tipsSHIFT.SetActive(true);
        }
    }

    IEnumerator Fade() {
        shouldSleep = true;
        yield return new WaitForSeconds(20);
        fadeAnimator.SetTrigger("fade");

        yield return new WaitForSeconds(2);
        dialogAnimator.SetTrigger("step1");
        yield return new WaitForSeconds(4);
        dialogAnimator.SetTrigger("step2");
        yield return new WaitForSeconds(1);
        cameraAnimator.SetTrigger("zoomout");
        yield return new WaitForSeconds(3);
        dialogAnimator.SetTrigger("step3");
        yield return new WaitForSeconds(4);
        dialogAnimator.SetTrigger("step4");
        dream.isSleeping = false;
        shouldSleep = false;
        tipsMouvement.SetActive(true);
    }


}
