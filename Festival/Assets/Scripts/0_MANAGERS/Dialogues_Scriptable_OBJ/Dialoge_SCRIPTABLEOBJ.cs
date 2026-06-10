using UnityEngine;

[CreateAssetMenu(fileName = "Dialoge_OBJ", menuName = "Dialogue_OBJ")]
public class Dialoge_SCRIPTABLEOBJ : ScriptableObject
{
    [Header("NORMAL DIALOGUE")]

    [Header("Dialogue")]
    [SerializeField] public string character_Name;
    [SerializeField] public string[] dialogue_Text;

    [Header("Effects")]
    [Range(0.04f, 0.1f)]
    [SerializeField] public float TypeWritter_Speed;

    [Header("CHOICES DIALOGUE")]

    [Header("Bool_Choices")]
    [SerializeField] public bool can_Make_Choices;

    [Header("Choices_dialogue")]
    [SerializeField] public string option_Yes;
    [SerializeField] public string option_No;
}
