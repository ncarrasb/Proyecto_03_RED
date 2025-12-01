using UnityEngine;
using System.Xml;

public class PlayerController : MonoBehaviour

{
    
    public enum Estados{IDLE, WALK, PRESS};
    public float speed;
    private Animator myanimator;
    public Estados mystate;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

     myanimator =  GetComponent<Animator>();
    mystate = Estados.IDLE;

    }

    // Update is called once per frame
    void Update()
    {

  
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    {
        mystate = Estados.WALK;
    }
    else if (Input.GetKeyDown(KeyCode.X))
    {
        mystate = Estados.PRESS;
    }
    else
    {
        mystate = Estados.IDLE;
    }

    switch(mystate)
    {
        case Estados.IDLE: Idle(); break;
        case Estados.WALK: Walk(); break;
        case Estados.PRESS: Press(); break;
    }
        
    }



public void Idle()
    {

        if(!Input.anyKey)
       {
      
        myanimator.Play("P1_Idle_Down");

        }

    }


    public void Walk()
    {

       
        if(Input.GetKey(KeyCode.A)) 
        {
            myanimator.Play("P1_Walk_Left");
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
            transform.eulerAngles = new Vector3(0, 180, 0);

        }

        if(Input.GetKey(KeyCode.D))
        {
            myanimator.Play("P1_Walk_Right");
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
            transform.eulerAngles = new Vector3(0, 0, 0);

        }

        if(Input.GetKey(KeyCode.S))
        {
            myanimator.Play("P1_Walk_Down");
             transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
        }

        if(Input.GetKey(KeyCode.W))
        {
             myanimator.Play("P1_Walk_Up");
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);


    

    
        }

    }



    public void Press()
    {

        if (Input.GetKeyDown(KeyCode.X))

        {

             myanimator.Play("P1_Press_Down");

        } 

    }

}