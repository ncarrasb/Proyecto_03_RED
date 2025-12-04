using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem.Interactions;

public class PlayerController_NB : NetworkBehaviour
{
    public enum Estados
    {
        IDLE,
        WALK,
        PRESS
    };

    public float speed = 5f;

    private Animator myanimator;
    public Estados mystate;

    private string prefix = "P1_";  //Esto es para cambiar entre las animaciones del Player1 y Player2.

    public RuntimeAnimatorController animPlayer1;
    public RuntimeAnimatorController animPlayer2;

    private SpriteRenderer spriteRenderer;

    //Network Inicio Spawneo
    public override void OnNetworkSpawn()
    {
        myanimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Esto es en el Host
        if (IsServer)
        {
            if (OwnerClientId == NetworkManager.Singleton.LocalClientId)
            {
                prefix = "P1_";
                myanimator.runtimeAnimatorController = animPlayer1;
            }
            else
            {
                prefix = "P2_";
                myanimator.runtimeAnimatorController = animPlayer2;
            }
        }

        //Esto en el cliente.
        else
        {
            if (OwnerClientId == NetworkManager.Singleton.LocalClientId)
            {
                prefix = "P2_";
                myanimator.runtimeAnimatorController = animPlayer2;
            }
            else
            {
                prefix = "P1_";
                myanimator.runtimeAnimatorController = animPlayer1;
            }
        }
        Debug.Log($"[OnNetworkSpawn] IsServer:{IsServer} IsOwner:{IsOwner} LocalClientId:{NetworkManager.Singleton.LocalClientId} OwnerClientId:{OwnerClientId} prefix:{prefix}");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Esto es para solo controlar al tu propio jugador.
        if (!IsOwner)
        {
            return;
        }

        //Esto para detectar los estados:
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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

        //Ejecucion de estados:
        switch (mystate)
        {
            case Estados.IDLE:
                Idle();
                break;

            case Estados.WALK:
                Walk();
                break;

            case Estados.PRESS:
                Press();
                break;
        }
    }

    //ESTADO IDLE
    public void Idle()
    {
        if (!Input.anyKey)
        {
            //Debug.Log($"[Idle] prefix={prefix}");
            myanimator.Play(prefix + "Idle_Down");
        }
    }

    //ESTADO WALK
    public void Walk()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            myanimator.Play(prefix + "Walk_Left");
            movement += Vector3.left;

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            myanimator.Play(prefix + "Walk_Right");
            movement += Vector3.right;

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            myanimator.Play(prefix + "Walk_Down");
            movement += Vector3.down;

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            myanimator.Play(prefix + "Walk_Up");
            movement += Vector3.up;

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (movement.sqrMagnitude > 0f)
        {
            movement.Normalize();
            transform.Translate(movement * Time.deltaTime * speed, Space.World);
        }
    }

    //ESTADO PRESS:

    public void Press()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            myanimator.Play(prefix + "Press_Down");
        }
    }
}
