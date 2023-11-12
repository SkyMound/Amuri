using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DreamManager : MonoBehaviour
{
    
    Vector2 init;

    public GameObject DreamObject;
    public GameObject RealityObject;

    public GameObject OpenedDoor;
    public GameObject ClosedDoor; 
    public GameObject Locker;

    [HideInInspector] public bool inDoor = false;

    [HideInInspector] public bool isDreaming = false;
    [HideInInspector] public bool isSmall = false;
    [HideInInspector] public bool isSleeping = false;
    [SerializeField] private Animator animate;
    [SerializeField] private TarodevController.PlayerController control;
    private TrailRenderer trail;

    [SerializeField] public int numberOfKey=0;

    public string niveau_suivant;
    public string actualScene;
    [SerializeField] private bool isTuto;
    private bool dialogStarted;
    [SerializeField] private TutoManager tutoManager;



    private float baseJumpForce;
    public float fireJumpBoost = 2.3f;
    public float delayBeforeDie = 0.1f;
    private bool canDie = true;

    void Start() {
        control = GetComponent<TarodevController.PlayerController>();
        trail = GetComponent<TrailRenderer>();
        baseJumpForce = control._jumpHeight;
        init = transform.position;
        dialogStarted = false;
        initLevel();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDreaming && !isSleeping && control.Grounded){
            StartCoroutine(fallAsleep());
        }

        /*if (Input.GetKeyDown(KeyCode.K) && isDreaming){
            StartCoroutine(wakeUp());
        }*/

        if (Input.GetKeyDown(KeyCode.E) && inDoor){
            StartCoroutine(EnterDoor());}

        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(actualScene);}

        if(numberOfKey<=0){
            Locker.SetActive(false);
        }

    }

    public void initLevel()
    {
        isDreaming=false;
        RealityObject.SetActive(true);
        foreach (Transform child in DreamObject.transform)
        {
            if (child.gameObject.tag == "Ground")
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.2584905f,0.3666043f,1f,0.1843138f);
                child.gameObject.SetActive(true);
                child.gameObject.layer = 7;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator wakeUp()
    {
        animate.SetBool("poisoned",false);
        isDreaming=false;
        RealityObject.SetActive(true);
        foreach (Transform child in DreamObject.transform)
        {
            if (child.gameObject.tag == "Ground")
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.2584905f,0.3666043f,1f,0.1843138f);
                child.gameObject.SetActive(true);
                child.gameObject.layer = 7;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        isSleeping = true;
        animate.SetBool("wakeUp",true);
        yield return new WaitForSeconds(1.2f);
        animate.SetBool("wakeUp",false);
        isSleeping=false;
    }

    public IEnumerator fallAsleep()
    {
        isSleeping = true;
        animate.SetBool("fallAsleep",true);
        yield return new WaitForSeconds(2.5f);
        animate.SetBool("fallAsleep",false);
        isSleeping = false;

        isDreaming=true;
        RealityObject.SetActive(false);
        canDie = false;
        StartCoroutine(dyingDelay());
        foreach (Transform child in DreamObject.transform)
        {
            if (child.gameObject.tag == "Ground")
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
                child.gameObject.SetActive(false);
                child.gameObject.layer = 6;
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }

private IEnumerator dyingDelay()
{
    yield return new WaitForSeconds(delayBeforeDie);
    canDie = true;
}

public IEnumerator EnterDoor(){
    isSleeping=true;
    OpenedDoor.SetActive(true);
    yield return new WaitForSeconds(1.5f);
    transform.position = transform.position + new Vector3(300,300,0);
    yield return new WaitForSeconds(1f);
    OpenedDoor.SetActive(false);

    yield return new WaitForSeconds(1f);

    SceneManager.LoadScene(niveau_suivant);
}

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && canDie)
        {

            if(isDreaming==false || collision.gameObject.tag=="Void"){
                if(isTuto)
                {
                    StartCoroutine(tutoManager.hasFall());
                }
                StartCoroutine(wakeUp());
                transform.position = init;
            }

            if(isDreaming==true && collision.gameObject.tag=="Anvil"){
                minimize();
                StartCoroutine(wakeUp());
            }
            
            
            if(isDreaming==true && collision.gameObject.tag=="Fire"){
                trail.emitting = true;
                if(control._jumpHeight == baseJumpForce)
                {
                    control._jumpHeight*=fireJumpBoost;
                }
                StartCoroutine(wakeUp());
            }

            if(isDreaming==true && collision.gameObject.tag=="Spike"){
                if (isSmall)
                {
                    grow();
                }
                control._jumpHeight = baseJumpForce;
                trail.emitting = false;
                StartCoroutine(wakeUp());
            }

            
        }
        
        
            if(collision.gameObject.tag=="end"){
                SceneManager.LoadScene("Generique");
            }

            if(collision.gameObject.tag == "Door" && numberOfKey==0){
                if(isTuto && !dialogStarted)
                {
                    dialogStarted = true;
                    StartCoroutine(tutoManager.inFrontOfDoor());
                }
               inDoor=true;
        }

            if(collision.gameObject.tag=="Key"){
                numberOfKey--;
                collision.gameObject.transform.position += new Vector3(-300,-300,0);
            }
    }


public IEnumerator DeathByPoison(){
    animate.SetBool("poisoned",true);

    yield return new WaitForSeconds(3f);
    animate.SetBool("poisoned",false);
    if(isDreaming){
    StartCoroutine(wakeUp());}

}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(isDreaming==true && collision.gameObject.tag=="Poison"){
                
                StartCoroutine(DeathByPoison());
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Door"){
               inDoor=false;      
            }
        }



    private void minimize()
    {
        isSmall = true;
        transform.localScale=new Vector3(0.5f,0.5f,1f);
        control._characterBounds.size=new Vector3(0.34f,0.5f,0);
    }

    private void grow()
    {
        isSmall = false;
        transform.localScale=new Vector3(1,1,1);
        control._characterBounds.size=new Vector3(0.68f,1f,0);
        //Hop on le met à peine au-dessus de la où il était comme ça il est pas stuck dans le sol comme un c*n
        transform.Translate(new Vector3(0,0.34f,0),Space.World);
    }
}
