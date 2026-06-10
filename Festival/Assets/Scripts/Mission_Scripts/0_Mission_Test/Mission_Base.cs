using UnityEngine;

public abstract class Mission_Base : MonoBehaviour
{
    [Header("Mission Variables")]
    [SerializeField] public string mission_Name;
    [SerializeField] public string mission_Description;

    [Header("Mission activate")]
    [SerializeField] public bool mission_Activate;
    [SerializeField] public bool mission_isFinish;

    public void on_Start_Mission()
    {
        mission_Activate = true;
        mission_isFinish = false;

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
