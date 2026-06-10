using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using System.Net;
using UnityEngine.UI;

public class UI_Dialoge_Manager : MonoBehaviour
{
    [SerializeField] public static UI_Dialoge_Manager instance_Dialogue;

    [Header("UI_Dialogue_Components")]
    [SerializeField] public TextMeshProUGUI characterName_Text;
    [SerializeField] public TextMeshProUGUI dialogue_Text;
    [SerializeField] public GameObject dialogue_Panel;
    [SerializeField] public int index_;
    [SerializeField] public string save_Text;
    [SerializeField] public int character_Visibles;
    [SerializeField] public int character_Save;
    bool next = false;

    [Header("Panel_Choices")]
    [SerializeField] public GameObject choice_Panel;
    [SerializeField] public Button button_Yes;
    [SerializeField] public Button button_No;


    [Header("Reference")]
    [SerializeField] public Dialoge_SCRIPTABLEOBJ objDialogue_Ref;

    //Referenciar el lugar del cual se sacara la info del dialogo
    public void Awake()
    {
        instance_Dialogue = this;
    }
    public void Start()
    {
        //dialogue_Panel.SetActive(false);
        //choice_Panel.SetActive(false);
    }

    public void Update()
    {

    }

    //Referenciar el obj/script que tenga el dialogo del personaje
    public void on_OpenDialogue(Dialoge_SCRIPTABLEOBJ obj_Ref)
    {

        objDialogue_Ref = obj_Ref;

        dialogue_Panel.SetActive(true);

        Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Move);

        characterName_Text.text = obj_Ref.character_Name.ToString();

        save_Text = obj_Ref.dialogue_Text[0];

        character_Save = save_Text.Length;

        StartCoroutine(TypeWriter_Effect());

        Cursor_Manager.Cursor_instance.on_Cursor_Unlock();

        choice_Panel.SetActive(false);

        button_Yes.GetComponentInChildren<TextMeshProUGUI>().text = null;
        button_No.GetComponentInChildren<TextMeshProUGUI>().text = null;

    }

    public void on_CloseDialogue()
    {

        //dialogue_Panel.SetActive(false);

        Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Move);

        Player_Controller.Player_Instance.ref_Camera.fieldOfView = 60f;

        characterName_Text.text = null;
        dialogue_Text.text = null;

        index_ = 0;

        save_Text = "";

        next = false;

        gameObject.transform.position = new Vector3(0, -8, 0);

        Cursor_Manager.Cursor_instance.on_Cursor_Lock();


    }

    public void on_ManualCloseDialogue()
    {
        if (Player_Controller.Player_Instance.player_Input.actionMaps[1].actions[0].WasPressedThisFrame())
        {
            dialogue_Panel.SetActive(false);

            Player_Controller.Player_Instance.change_Status(Player_Controller.playerStatus.status_Move);

            characterName_Text.text = null;
            dialogue_Text.text = null;

            index_ = 0;

        }

    }


    public void on_NextLine()
    {
        //No puedes elegir opciones
        if (Player_Controller.Player_Instance.player_Input.actionMaps[1].actions[1].WasPressedThisFrame() && index_ < objDialogue_Ref.dialogue_Text.Length
            && objDialogue_Ref.can_Make_Choices == false)
        {
            if (dialogue_Text.maxVisibleCharacters != character_Save)
            {
                dialogue_Text.maxVisibleCharacters = character_Save;


            }

            //Si la cantidad de caracteres visible es igual al largo del texto
            if (dialogue_Text.maxVisibleCharacters == character_Save /* && index_ < objDialogue_Ref.dialogue_Text.Length*/)
            {
                //index_ += 1;

                //save_Text = objDialogue_Ref.dialogue_Text[index_].ToString();

                //character_Save = save_Text.Length;

                //StartCoroutine(TypeWriter_Effect());

                if (index_ < objDialogue_Ref.dialogue_Text.Length)
                {
                    index_ += 1;

                    save_Text = objDialogue_Ref.dialogue_Text[index_].ToString();

                    character_Save = save_Text.Length;

                    StartCoroutine(TypeWriter_Effect());
                }

                //if(index_ == objDialogue_Ref.dialogue_Text.Length)
                //{
                //    on_CloseDialogue();
                //}

            }

            //Si el index es _IGUAL_ a la cantidad de lineas de dialogo



        }

        if (Player_Controller.Player_Instance.player_Input.actionMaps[1].actions[1].WasPressedThisFrame() && index_ == objDialogue_Ref.dialogue_Text.Length
            && objDialogue_Ref.can_Make_Choices == false)
        {
            on_CloseDialogue();

        }


        //Puedes elegir opciones
        if (Player_Controller.Player_Instance.player_Input.actionMaps[1].actions[1].WasPressedThisFrame() && index_ < objDialogue_Ref.dialogue_Text.Length
            && objDialogue_Ref.can_Make_Choices == true)
        {
            if (dialogue_Text.maxVisibleCharacters != character_Save)
            {
                dialogue_Text.maxVisibleCharacters = character_Save;


            }

            //Si la cantidad de caracteres visible es igual al largo del texto
            if (dialogue_Text.maxVisibleCharacters == character_Save /* && index_ < objDialogue_Ref.dialogue_Text.Length*/)
            {

                if (index_ < objDialogue_Ref.dialogue_Text.Length)
                {
                    index_ += 1;

                    save_Text = objDialogue_Ref.dialogue_Text[index_].ToString();

                    character_Save = save_Text.Length;

                    StartCoroutine(TypeWriter_Effect());
                }

                //else if (index_ == objDialogue_Ref.dialogue_Text.Length)
                //{
                //    //on_CloseDialogue();

                //    choice_Panel.SetActive(true);

                //    button_Yes.GetComponentInChildren<TextMeshProUGUI>().text = objDialogue_Ref.option_Yes;
                //    button_No.GetComponentInChildren<TextMeshProUGUI>().text = objDialogue_Ref.option_No;
                //}

            }

            //Si el index es _IGUAL_ a la cantidad de lineas de dialogo



        }

        if (Player_Controller.Player_Instance.player_Input.actionMaps[1].actions[1].WasPressedThisFrame() && index_ == (objDialogue_Ref.dialogue_Text.Length - 1)
            && objDialogue_Ref.can_Make_Choices == true)
        {
            choice_Panel.SetActive(true);

            button_Yes.GetComponentInChildren<TextMeshProUGUI>().text = objDialogue_Ref.option_Yes;
            button_No.GetComponentInChildren<TextMeshProUGUI>().text = objDialogue_Ref.option_No;
        }

    }

    IEnumerator TypeWriter_Effect()
    {
        dialogue_Text.text = save_Text;

        dialogue_Text.maxVisibleCharacters = 0;

        foreach (char c in save_Text)
        {
            dialogue_Text.maxVisibleCharacters++;

            yield return new WaitForSeconds(objDialogue_Ref.TypeWritter_Speed);

        }
    }
}
