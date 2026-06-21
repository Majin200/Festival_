using UnityEngine;

public class PickUp_Rock : MonoBehaviour
{
    [Header("Bools")]
    [SerializeField] public bool can_PickUp;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            can_PickUp = true;

            //Player_Controller.Player_Instance.missionObject_Pocket = gameObject;

            //gameObject.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
         can_PickUp = false;

    }

    public void OnTriggerStay(Collider other)
    {
        if(can_PickUp && Player_Controller.Player_Instance.player_Input.actionMaps[0].actions[2].WasPerformedThisFrame())
        {
            Player_Controller.Player_Instance.missionObject_Pocket = gameObject;

            gameObject.SetActive(false);
        }
    }
}
