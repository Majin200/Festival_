using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Print_Missions : MonoBehaviour
{
    public static Print_Missions Print_instance;

    [Header("UI_Text")]
    [SerializeField] public TextMeshProUGUI text_Mision_Name;
    [SerializeField] public TextMeshProUGUI text_Mision_Description;

    public void Awake()
    {
        Print_instance = this;
    }

    public void Start()
    {
        text_Mision_Name = GameObject.Find("Text_Mision_Name").GetComponent<TextMeshProUGUI>();
        text_Mision_Description = GameObject.Find("Text_Mision_Description").GetComponent<TextMeshProUGUI>();
    }

    public void on_Set_Text_Name(string name)
    {
        text_Mision_Name.text = name;
    }

    public void on_Set_Text_Description(string description)
    {
        text_Mision_Description.text = description;
    }

    public void on_Set_Text_Null()
    {
        text_Mision_Name.text = null;
        text_Mision_Description.text = null;


    }
}
