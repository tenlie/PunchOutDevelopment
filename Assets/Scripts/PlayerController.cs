using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb2D { get; set; }
    public Vector3 moveVector { get; set; }
    public float moveSpeed;

    public GameObject ClearUI_BG;
    public GameObject GameOverUI_BG;
    public GameObject WarningMark;

    public Joystick joystick;

    public static float startFadeTime = 1.0f;

	public Timer timer;

    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip gameOverSound;

    public FieldOfView view;

    public static int punchOutCnt { get; set; }

    void Awake()
    {
        System.GC.Collect();

        rb2D = GetComponent<Rigidbody2D>();
        moveVector = new Vector3(0, 0, 0);
    }

    void Start()
    {
        //zFoxFadeFilter.instance.FadeIn(Color.black, startFadeTime);
        //startFadeTime = 2.0f;
        SaveData.LoadOption();
        moveSpeed = SaveData.PlayerSensitivity * 10;
        Debug.Log("PlayerSensitivity : "+ moveSpeed);
    }

    void Update()
    {
        HandleInput();
    }

    // 모든기기에 동일한 타이밍으로 주기적으로 발생하는 함수
    void FixedUpdate()
    {
        Move();
        EaseVelocity();
    }

    void HandleInput()
    {
        moveVector = PoolInput();

        if (Application.platform == RuntimePlatform.Android)    //Android Platform
        {
            if (Input.GetKey(KeyCode.Menu))
            {
                //Menu Button
            }
            else if (Input.GetKey(KeyCode.Home))
            {
                //Home Button
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                //Back Button
                if (!LevelManager.Instance.isPaused)
                {
                    LevelManager.Instance.PauseApplication(true);
                }
            }
        }
        else                                                    //Other Platform
        {
            if (Input.GetKey(KeyCode.Menu))
            {
                //Menu Button
            }
            else if (Input.GetKey(KeyCode.Home))
            {
                //Home Button
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                //Back Button
                if (!LevelManager.Instance.isPaused)
                {
                    LevelManager.Instance.PauseApplication(true);
                }
            }
        }
    }

    void Move()
    {
        rb2D.AddForce(moveVector * moveSpeed);
    }

    void EaseVelocity()
    {
        Vector3 easeVelocity = rb2D.velocity;
        easeVelocity.x *= 0.75f;
        easeVelocity.y *= 0.75f;
        easeVelocity.z = 0.0f;
        rb2D.velocity = easeVelocity;
    }

    Vector3 PoolInput()
    {
        Vector3 direction = Vector3.zero;

        direction.x = joystick.GetHorizontalValue();
        direction.y = joystick.GetVerticalValue();

        if (direction.magnitude > 1)
            direction.Normalize();

        return direction;
    }

    public void Kill()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Exit"))
        {
            Debug.Log(col.gameObject.name);
			timer.finnished = true;
            GameClear();
			return;
        }else if (col.CompareTag("Enemy"))
        {
            Debug.Log("넌 이미 뒤져있다");
            GameOver();
			return;
        }
        else if (col.CompareTag("Print"))
        {
            Debug.Log("잠깐 쉬러왔어요");
            PrintOut();
			return;
        }
    }

    void GameClear()
    {
        ClearUI_BG.SetActive(true);
        Time.timeScale = 0;
    }
    void PrintOut()
    {
        Debug.Log("데스넘버 초기화");
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemy.Length);
        for(int i=0; i<enemy.Length; i++)
        {
            GameObject enemies = enemy[i];
            enemies.GetComponent<FieldOfView>().deathNumber = 0;
            Debug.Log(enemies.GetComponent<FieldOfView>().deathNumber);
        }
    }
    void GameOver()
    {
        GameOverUI_BG.SetActive(true);
        Time.timeScale = 0;
    }
}
