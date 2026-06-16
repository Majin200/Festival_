using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPC_Control : MonoBehaviour
{
    [Header("NPC_Components")]
    [SerializeField] public SphereCollider npc_Collider;

    [Header("Bool")]
    [SerializeField] public bool can_Talk;
    [SerializeField] public bool had_Mission;

    [Header("Debug_Variables")]
    [SerializeField] public float radius_Wire;
    [SerializeField] public float direction_X;
    [SerializeField] public float dif_Direction;
    [SerializeField] public float scale_X;

    [Header("NPC_Canvas")]
    [SerializeField] public Canvas npc_Canvas;
    [SerializeField] public GameObject canvas_Point;

    [Header("Dialogue")]
    [SerializeField] public Dialoge_SCRIPTABLEOBJ[] ref_Dialogue;

    [Header("Mission")]
    [SerializeField] public Mission_Base ref_Mission;

    public void Start()
    {
        npc_Collider = GetComponent<SphereCollider>();

        scale_X = transform.localScale.x;

        npc_Canvas = GameObject.FindGameObjectWithTag("Text_Canvas").GetComponent<Canvas>();

        if(had_Mission)
        {
            ref_Mission = GetComponent<Mission_Base>();
        }
    }
    public void Update()
    {
        look_At_player();

        //on_Talk();
    }

    public void look_At_player()
    {
        direction_X = transform.position.x;

        dif_Direction = transform.position.x - Player_Controller.Player_Instance.transform.position.x;


        if (dif_Direction > 0)
        {

            transform.localScale = new Vector3(scale_X * -1, transform.localScale.y, transform.localScale.z);

            //Debug.Log("Mirando a la izquierda");
        }

        else if(dif_Direction < 0)
        {

            transform.localScale = new Vector3(scale_X * 1, transform.localScale.y, transform.localScale.z);

            //Debug.Log("Mirando a la derecha");

        }
    }

    public void on_Talk()
    {

        //Entrar a hablar con el npc
        if (can_Talk && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        {
            Debug.Log("Hablando con el jugador");

            Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

            Player_Controller.Player_Instance.player_LookNPC = gameObject;

            Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;

            npc_Canvas.transform.position = canvas_Point.transform.position;

            npc_Canvas.gameObject.SetActive(true);

            UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogue[0]);

        }

        

        //Salir de hablar con el npc
        //if (can_Talk && Player_Controller.Player_Instance.player_Input.actionMaps[1].actions[0].WasPerformedThisFrame())
        //{
        //    Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Move);

        //    Player_Controller.Player_Instance.player_LookNPC = null;

        //    Player_Controller.Player_Instance.ref_Camera.fieldOfView = 60f;

        //    npc_Canvas.transform.position = new Vector3(0, 0, 0);

        //    npc_Canvas.gameObject.SetActive(false);

        //    can_Talk = false;

        //    UI_Dialoge_Manager.instance_Dialogue.on_CloseDialogue();

        //}
    }

    #region colliders
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Player_Controller.Player_Instance.GetComponent<Player_Controller>().status_ == Player_Controller.playerStatus.status_Move 
            && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame() && had_Mission)
        {

            if(Player_Controller.Player_Instance.missionObject_Pocket != ref_Mission.mission_Object && !ref_Mission.mission_Activate)
            {
                Call_Activate_Mission.call_instance.set_Mission(gameObject.GetComponent<Mission_Base>());

                UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogue[0]);

                npc_Canvas.transform.position = canvas_Point.transform.position;

                Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

                Player_Controller.Player_Instance.player_LookNPC = gameObject;

                Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;
            }

            if (Player_Controller.Player_Instance.missionObject_Pocket != ref_Mission.mission_Object && ref_Mission.mission_Activate)
            {
                UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogue[1]);

                npc_Canvas.transform.position = canvas_Point.transform.position;

                Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

                Player_Controller.Player_Instance.player_LookNPC = gameObject;

                Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;
            }

            if(Player_Controller.Player_Instance.missionObject_Pocket == ref_Mission.mission_Object && ref_Mission.mission_Activate)
            {
                UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogue[2]);

                npc_Canvas.transform.position = canvas_Point.transform.position;

                Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

                Player_Controller.Player_Instance.player_LookNPC = gameObject;

                Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;

                ref_Mission.on_Finish_Mission();
            }
        }

        if(other.CompareTag("Player") && Player_Controller.Player_Instance.GetComponent<Player_Controller>().status_ == Player_Controller.playerStatus.status_Move
            && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        {
            UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogue[0]);

            npc_Canvas.transform.position = canvas_Point.transform.position;

            Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

            Player_Controller.Player_Instance.player_LookNPC = gameObject;

            Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;
        }
    }

    #endregion



    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radius_Wire);
    }
}
