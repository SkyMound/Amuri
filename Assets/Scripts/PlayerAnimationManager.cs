using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator anime;
    //[SerializeField] private DreamManager dream;
    // [SerializeField] private DeathManager death;
    [SerializeField] private TarodevController.PlayerController control;
    private bool facingRight = false;


    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (control.Velocity[0]>0 && !facingRight)
        {
            Flip();
        }
        else if (control.Velocity[0]<0 && facingRight)
        {
            Flip();
        }
        anime.SetFloat("Speed",Mathf.Abs(control.Velocity[0]));
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.localPosition = new Vector3 (-transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);

        Vector3 currScale =  transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
    }
}
