using UnityEngine;

public class Test_Mission_Object : MonoBehaviour
{
    [Header("Bools")]
    [SerializeField] public bool can_Grab;

    [Header("References")]
    [SerializeField] public Mission_Obstacle ref_Obstacle;

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {

            if(ref_Obstacle.npc_On_Alert == false && (ref_Obstacle.npc_convinced_ || ref_Obstacle.npc_disctracted_))
            {
                can_Grab = true;

            }

            else
            {
                can_Grab = false;

            }
        }

        
    }

    public void OnTriggerExit(Collider other)
    {
        can_Grab = false;
    }

    public void Update()
    {
        if(can_Grab && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        {
            Player_Controller.Player_Instance.missionObject_Pocket = gameObject;

            gameObject.SetActive(false);

        }

        if(!can_Grab && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        {
            ref_Obstacle.on_Talk();
        }
    }
}
