using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public GameObject F1;
    public GameObject F2;
    public GameObject F3;

    [SerializeField] bool isEnd;


    public GameObject Player;



    void Start()
    {

        if(isEnd){
            StartCoroutine(enddad());
        }
        else{
        StartCoroutine(gene());}
    }

 public IEnumerator gene(){



    yield return new WaitForSeconds(1.95f);
    
    F1.SetActive(true);
    yield return new WaitForSeconds(1.5f);
    F2.SetActive(true);
    yield return new WaitForSeconds(1.5f);
    F3.SetActive(true);
    yield return new WaitForSeconds(4f);

    SceneManager.LoadScene("Menu");
}

public IEnumerator enddad(){
    //Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

    yield return new WaitForSeconds(1f);
F1.SetActive(true);
    yield return new WaitForSeconds(0.3f);
F2.SetActive(true);
    yield return new WaitForSeconds(3f);
//Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
F1.SetActive(false);
F2.SetActive(false);

}
 
}
