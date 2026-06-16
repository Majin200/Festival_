using UnityEngine;

public class Test_Mission_Object : MonoBehaviour
{
    [Header("Bools")]
    [SerializeField] public bool can_Grab;

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            can_Grab = true;
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
    }
}
