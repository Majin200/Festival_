using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public static Player_Controller Player_Instance;

    [Header("Player_Stats")]
    [SerializeField] public float player_Speed;
    [SerializeField] public float player_RunSpeed;

    [Header("Player_Components")]
    [SerializeField] public BoxCollider player_Collider;
    [SerializeField] public Rigidbody player_RB;
    [SerializeField] public Animator player_Anim;
    [SerializeField] public InputActionAsset player_Input;


    float scale_X;

    public void Awake()
    {
        Player_Instance = this;

        player_Input.Enable();
    }


    public void Start()
    {
        player_Collider = GetComponent<BoxCollider>();
        player_RB = GetComponent<Rigidbody>();
        player_Anim = GetComponent<Animator>();

        player_Anim.SetFloat("Walking", 0);

        scale_X = transform.localScale.x;
    }

    public void Update()
    {
        if (player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x != 0)
        {
            if(player_Input.actionMaps[0].actions[1].IsPressed())
            {
                transform.Translate(new Vector3(player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x * player_Speed * 2 * Time.deltaTime, 0, 0));
                player_Anim.SetFloat("Walking", 2);

                if (player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x < 0)
                {
                    transform.localScale = new Vector3(scale_X * -1f, transform.localScale.y, transform.localScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(scale_X * 1f, transform.localScale.y, transform.localScale.z);

                }

                Debug.Log("Corriendo");
            }

            else
            {
                transform.Translate(new Vector3(player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x * player_Speed * Time.deltaTime, 0, 0));
                player_Anim.SetFloat("Walking", 2);

                if (player_Input.actionMaps[0].actions[0].ReadValue<Vector2>().x < 0)
                {
                    transform.localScale = new Vector3(scale_X * -1f, transform.localScale.y, transform.localScale.z);
                }

                else
                {
                    transform.localScale = new Vector3(scale_X * 1f, transform.localScale.y, transform.localScale.z);

                }

                Debug.Log("Caminando");

            }

        }

        else
        {
            player_Anim.SetFloat("Walking", 0);
        }
    }

    public void FixedUpdate()
    {
        
    }


}
