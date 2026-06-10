using UnityEngine;

public class Call_Activate_Mission : MonoBehaviour
{
    public static Call_Activate_Mission call_instance;

    [Header("References")]
    [SerializeField] public Mission_Base ref_Mission;

    public void Awake()
    {
        call_instance = this;
    }
    public void call_Mission()
    {

        ref_Mission.on_Start_Mission();
    }
}
