using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Player_Instance;

    [Header("Player_Stats")]
    [SerializeField] public float player_Speed;
    [SerializeField] public float player_RunSpeed;

    [Header("Player_Components")]
    [SerializeField] public BoxCollider player_Collider;
    [SerializeField] public Rigidbody player_RB;
    [SerializeField] public Animator player_Anim;
    [SerializeField] public InputActionAsset player_Input;

    [Header("Player_Set_References")]
    [SerializeField] public GameObject player_LookNPC;

    [Header("Player_Phases")]
    [SerializeField] public playerStatus status_;

    [Header("References")]
    [SerializeField] public Camera ref_Camera;

    [Header("Camera_Variables")]
    [SerializeField] public float max_FOV;
    [SerializeField] public float min_FOV;

    [Header("Player_Pockets")]
    [SerializeField] public GameObject missionObject_Pocket;


    float scale_X;

    public void Awake()
    {
        Player_Instance = this;

        player_Input.Enable();
    }

    public enum playerStatus
    {
        status_Move = 0,
        status_Dialogue = 1,
    }

    public void check_Status()
    {
        switch (status_)
        {
            case playerStatus.status_Move:
                on_Move();

                break;

            case playerStatus.status_Dialogue:
                set_Idle();
                on_Dialogue();
                on_Dialogue_Next_Line();

                break;

            default:
                break;
        }
    }

    public void change_Status(playerStatus change)
    {
        switch (change)
        {
            case playerStatus.status_Move:
                break;

            case playerStatus.status_Dialogue:
                break;

            default:
                break;
        }

        status_ = change;
    }

    public void Start()
    {
        player_Collider = GetComponent<BoxCollider>();
        player_RB = GetComponent<Rigidbody>();
        player_Anim = GetComponent<Animator>();

        player_Anim.SetFloat("Walking", 0);

        scale_X = transform.localScale.x;

        ref_Camera = GetComponentInChildren<Camera>();
    }

    public void Update()
    {
        check_Status();
    }

    public void FixedUpdate()
    {
        
    }

    #region status moves

    public void on_Move()
    {
        if (player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x != 0)
        {
            if (player_Input.actionMaps[0].actions[1].IsPressed())
            {
                transform.Translate(new Vector3(player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x * player_Speed * 2 * Time.deltaTime, 0, 0));
                player_Anim.SetFloat("Walk", 2);

                if (player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x < 0)
                {
                    transform.localScale = new Vector3(scale_X * -1f, transform.localScale.y, transform.localScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(scale_X * 1f, transform.localScale.y, transform.localScale.z);

                }

                //Debug.Log("Corriendo");
            }

            else
            {
                transform.Translate(new Vector3(player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x * player_Speed * Time.deltaTime, 0, 0));
                player_Anim.SetFloat("Walk", 2);

                if (player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x < 0)
                {
                    transform.localScale = new Vector3(scale_X * -1f, transform.localScale.y, transform.localScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(scale_X * 1f, transform.localScale.y, transform.localScale.z);

                }

                //Debug.Log("Caminando");

            }

        }

        else
        {
            player_Anim.SetFloat("Walk", 0);
        }
    }

    #endregion

    #region status dialogue

    public void on_Dialogue()
    {
        float npc_Pos = player_LookNPC.transform.position.x;

        if(npc_Pos < transform.position.x)
        {
            transform.localScale = new Vector3(scale_X * -1f, transform.localScale.y, transform.localScale.z);
        }

        else
        {
            transform.localScale = new Vector3(scale_X * 1f, transform.localScale.y, transform.localScale.z);
        }

        //if(player_Input.actionMaps[1].actions[0].WasPerformedThisFrame())
        //{
        //    change_Status(playerStatus.status_Move);

        //    ref_Camera.fieldOfView = 60f;
        //}
    }

    public void on_Dialogue_Next_Line()
    {
        UI_Dialoge_Manager.instance_Dialogue.on_NextLine();
    }


    #endregion


    #region anims

    public void set_Idle()
    {
        player_Anim.SetFloat("Walk", 0);
    }

    #endregion
}
