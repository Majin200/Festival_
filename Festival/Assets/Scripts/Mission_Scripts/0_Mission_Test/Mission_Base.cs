using UnityEngine;

public class Mission_Base : MonoBehaviour
{
    [Header("Mission Variables")]
    [SerializeField] public string mission_Name;
    [SerializeField] public string mission_Description;

    [Header("Mission activate")]
    [SerializeField] public bool mission_Activate;
    [SerializeField] public bool mission_isFinish;

    [Header("Objects_4_Mission")]
    [SerializeField] public GameObject mission_Object;

    //[Header("Colliders")]
    //[SerializeField] public Collider collider_;

    public void Start()
    {
        mission_Object.SetActive(false);

    }
    public void on_Start_Mission()
    {
        mission_Activate = true;
        mission_isFinish = false;

        mission_Object.SetActive(true);

        Print_Missions.Print_instance.on_Set_Text_Name(mission_Name);
        Print_Missions.Print_instance.on_Set_Text_Description(mission_Description);
    }

    public void on_Finish_Mission()
    {
        mission_Activate = false;
        mission_isFinish = true;

        Print_Missions.Print_instance.on_Set_Text_Null();
    }
}
