using UnityEngine;

public class ThrowRock_point : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] public GameObject throw_GameObject;

    [Header("Bools")]
    [SerializeField] public bool entry_;

    [Header("References")]
    [SerializeField] public Mission_Obstacle ref_Obstacle;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entry_ = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        entry_ = false;
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && entry_ && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        {
            if(Player_Controller.Player_Instance.missionObject_Pocket == throw_GameObject)
            {
                Debug.Log("Lazando piedra a la ventana");

                Player_Controller.Player_Instance.missionObject_Pocket = null;

                ref_Obstacle.NPC_is_Distracted();
            }

            else
            {
                Debug.Log("No hay objeto que lanzar");
            }

            //Debug.Log("Lazando piedra a la ventana");

            //Player_Controller.Player_Instance.missionObject_Pocket = null;
        }

        //else if(other.CompareTag("Player") && Player_Controller.Player_Instance.missionObject_Pocket != throw_GameObject && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        //{
        //    Debug.Log("No hay objeto que lanzar");

        //}
    }
}
