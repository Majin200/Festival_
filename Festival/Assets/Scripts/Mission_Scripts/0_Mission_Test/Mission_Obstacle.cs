using UnityEngine;

public class Mission_Obstacle : MonoBehaviour
{
    [Header("npc_Canvas")]
    [SerializeField] public GameObject canvas_Point;
    [SerializeField] public Canvas npc_Canvas;

    [Header("Bools")]
    [SerializeField] public bool npc_disctracted_;
    [SerializeField] public bool npc_convinced_;
    [SerializeField] public bool npc_On_Alert;

    [Header("Dialogue_Ref")]
    [SerializeField] public Dialoge_SCRIPTABLEOBJ[] ref_Dialogues_OnAlert;
    [SerializeField] public Dialoge_SCRIPTABLEOBJ[] ref_Dialogues_Convencided;
    [SerializeField] public Dialoge_SCRIPTABLEOBJ[] ref_Dialogues_Distracted;

    public void Start()
    {
        npc_On_Alert = true;

        npc_Canvas = GameObject.FindGameObjectWithTag("Text_Canvas").GetComponent<Canvas>();
    }

    #region solutions
    public void NPC_is_Convinced()
    {
        npc_disctracted_ = false;
        npc_convinced_ = true;

        npc_On_Alert = false;

    }

    public void NPC_is_Distracted()
    {
        npc_disctracted_ = true;
        npc_convinced_ = false;

        npc_On_Alert = false;

    }

    #endregion

    #region dialogues prints

    public void on_Talk()
    {
        if (npc_On_Alert)
        {
            UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogues_OnAlert[0]);

            npc_Canvas.transform.position = canvas_Point.transform.position;

            Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

            Player_Controller.Player_Instance.player_LookNPC = gameObject;

            Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;
        }

        if (npc_convinced_)
        {
            UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogues_Convencided[0]);

            npc_Canvas.transform.position = canvas_Point.transform.position;

            Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

            Player_Controller.Player_Instance.player_LookNPC = gameObject;

            Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;
        }

        if (npc_disctracted_)
        {
            UI_Dialoge_Manager.instance_Dialogue.on_OpenDialogue(ref_Dialogues_Distracted[0]);

            npc_Canvas.transform.position = canvas_Point.transform.position;

            Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Dialogue);

            Player_Controller.Player_Instance.player_LookNPC = gameObject;

            Player_Controller.Player_Instance.ref_Camera.fieldOfView = 42f;
        }
    }



    #endregion
}
