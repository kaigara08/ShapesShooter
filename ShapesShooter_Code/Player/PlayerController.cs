using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject ShotPrefub;
    public GameObject ShotSpawnPosition;

    public PlayerUIScript uiScript;

    private Rigidbody rigidBody;
    private Transform transForm;
    private Vector3 velocity;
    public float speed = 5.0f;
    private Vector3 aim;
    private Quaternion playerRotation;
    private Vector2 v;

    private bool ShotFlag;
    private int ShotTime;

    private List<EnemyController> enemyList = new List<EnemyController>();

    [SerializeField]
    private int aimLockNum = 0;
    [SerializeField]
    private int aimTargetNmb = 0;

    private GameDirector gameDirector;

    public GameObject mesh;

    private PlayerInput playerInput;
    private InputAction fire;
    private InputAction move;
    private InputAction aimlock;
    private InputAction timestop;
    private InputAction exit;
    private InputAction pose;

    private bool poseFlag = true;

    private int HP = 5;
    private int MaxHP = 5;

    [SerializeField]
    public float energy = 0;
    public int MaxEnergy = 120;

    public bool TimeFlag = false;

    private bool invincibleFlag = false;
    private float invincibleTime = 0.0f;
    private float maxInvincibleTime = 2.0f;

    public State state = State.Nomal;

    private AudioSource audioSource;
    public AudioClip damageClip;
    public AudioClip attackClip;
    public AudioClip recoveryClip;
    public AudioClip deathblowClip;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        fire = playerInput.actions["Fire"];
        move = playerInput.actions["Move"];
        aimlock = playerInput.actions["AimLock"];
        timestop = playerInput.actions["TimeStop"];
        exit = playerInput.actions["exit"];
        pose = playerInput.actions["Pose"];
        
        playerInput.SwitchCurrentActionMap("Player");

        state = State.Nomal;

        

        energy = 120;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rigidBody = GetComponent<Rigidbody>();
        transForm = GetComponent<Transform>();
        uiScript = GameObject.Find("StetasPanel").GetComponent<PlayerUIScript>();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        audioSource = GetComponent<AudioSource>();

        ShotFlag = false;

    }

    private void OnEnable()
    {
        
        if (playerInput == null) return;

        fire.started += OnFireStart;
        fire.canceled += OnFireCanceled;
        move.performed += OnMovePerformed;
        move.canceled += OnMoveCanceled;
        aimlock.started += OnAimLock;
        timestop.started += OnTimeStop;
        exit.started += OnExit;
        pose.started += OnPose;
    }
    private void OnDisable()
    {
        if (playerInput == null) return;

        fire.started -= OnFireStart;
        fire.canceled -= OnFireCanceled;
        move.performed -= OnMovePerformed;
        move.canceled -= OnMoveCanceled;
        aimlock.started -= OnAimLock;
        timestop.started -= OnTimeStop;
        exit.started -= OnExit;
        pose.started -= OnPose;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(state == State.Non)
        {
            mesh.SetActive(false);
        }
        

        for (int i = 0; i < enemyList.Count; ++i)
        {
            if (enemyList[i] == null)
            {
                EnemyListRemove(enemyList[i]);
            }
        }

        if (enemyList.Count == 1)
        {
            aimTargetNmb = enemyList[0].GetSetEnemyNmb;

        }
        if (enemyList.Count == 0)
        {
            aimTargetNmb = -1;
        }

        if(ShotFlag)
        {
            Shot();
        }

        Move(v);

        if (enemyList.Count != 0)
        {
            AimLock();
        }
        else
        {
            Rotate(v);
        }
       
        gameDirector.CallCangeAim(aimTargetNmb);
        
        if(invincibleFlag)
        {
            InvincibleMethod();
        }

        if(TimeFlag)
        {
            TimeMethod();
        }

        //invincibleFlag = true;
    }

    #region 入力判定
    public void OnPose(InputAction.CallbackContext c)
    {
        if(poseFlag)
        {
            Time.timeScale = 0;
            poseFlag = false;
        }
        else if(!poseFlag)
        {
            Time.timeScale = 1;
            poseFlag = true;
        }
    }

    public void OnFireStart(InputAction.CallbackContext c)
    {
        ShotFlag = true;
    }

    public void OnFireCanceled(InputAction.CallbackContext c)
    {
        ShotFlag = false;
        ShotTime = 0;
    }

    public void OnMovePerformed(InputAction.CallbackContext cct)
    {
        v = cct.ReadValue<Vector2>();
    }

    public void OnMoveCanceled(InputAction.CallbackContext cct)
    {
        v = cct.ReadValue<Vector2>();
    }

    public void OnAimLock(InputAction.CallbackContext cct)
    {
        
            int MaxNmb = enemyList.Count;
        if(MaxNmb == 0) { return; }
            aimLockNum++;
            if(aimLockNum >= MaxNmb)
            {
                aimLockNum = 0;
            }
            aimTargetNmb = enemyList[aimLockNum].GetSetEnemyNmb;
            Debug.Log("aimTargetNmb:" + aimTargetNmb);
        
    }

    public void OnExit(InputAction.CallbackContext cct)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    public void OnTimeStop(InputAction.CallbackContext cct)
    {
        if (energy >= MaxEnergy || TimeFlag)
        {
            if (!TimeFlag)
            {
                TimeFlag = true;
                gameDirector.TimeStopMesod(TimeFlag);
                audioSource.PlayOneShot(deathblowClip);
                audioSource.loop = true;
            }
            else if(TimeFlag)
            {
                TimeFlag = false;
                gameDirector.TimeStopMesod(TimeFlag);
                audioSource.Stop();
                audioSource.loop = false;
            }
            
        }
    }

    #endregion

    public void TimeMethod()
    {
        if(energy <= 0)
        {
            energy = 0;
            TimeFlag = false;
            gameDirector.TimeStopMesod(TimeFlag);
            audioSource.loop = false;
        }
        energy -= 24 * Time.deltaTime;
    }

    public void InvincibleMethod()
    {
        if (invincibleTime >= maxInvincibleTime)
        {
            invincibleFlag = false;
            mesh.SetActive(true);
            invincibleTime = 0;
            return;
        }
        
        var cycleVar = Mathf.Repeat(invincibleTime, 0.5f);

        if(cycleVar >=0.25f)
        {
            mesh.SetActive(false);
        }
        else
        {
            mesh.SetActive(true);
        }

        invincibleTime += Time.deltaTime;
    }

    private void AimLock()
    {
        //エネミーとプレイヤーのベクトルを計算（Yは0にしておく）
        var direction = enemyList[aimLockNum].transform.position - transForm.position;
        direction.y = 0;

        //LookRotationで上のベクトルをQuaternionに変換
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        //Quaternion.Lerp(AからBまでの角度をCで回転していく)で回転させる。
        transForm.rotation = Quaternion.Lerp(transForm.rotation, lookRotation, 1.0f);
    }

    private void Move(Vector2 value)
    {
        velocity = new Vector3(value.x, 0, value.y).normalized;

        rigidBody.velocity = velocity * speed;
    }

    private void Rotate(Vector2 value)
    {
        aim = new Vector3(value.x, 0, value.y).normalized;

        if (aim.magnitude > 0.5f)
        {
            playerRotation = Quaternion.LookRotation(aim, Vector3.up);
        }
        transForm.rotation = Quaternion.Lerp(transForm.rotation, playerRotation, 0.6f);
    }

    private void Shot()
    {
        if(ShotTime <= 0)
        {
            GameObject shots = Instantiate(ShotPrefub);
            shots.transform.position = ShotSpawnPosition.transform.position;
            shots.GetComponent<ShotController>().Fire(ShotSpawnPosition.transform.forward);
            audioSource.PlayOneShot(attackClip);
            ShotTime = 10;
        }
        ShotTime--;
    }



    public void EnemySearchHit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController controller = other.gameObject.GetComponent<EnemyController>();
            enemyList.Add(controller);
            gameDirector.CallGenerateMarker(other.gameObject.transform, controller.GetSetEnemyNmb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyListRemove(other.gameObject.GetComponent<EnemyController>());
        }
    }

    private void EnemyListRemove(EnemyController e)
    {

        int EnemyId = e.GetSetEnemyNmb;
        for(int i=0;i<enemyList.Count;++i)
        {
            if(EnemyId == enemyList[i].GetSetEnemyNmb)
            {
                enemyList.RemoveAt(i);
            }
        }
        gameDirector.CallRemoveMarkers(EnemyId);
        if ((aimTargetNmb == EnemyId || aimLockNum >= enemyList.Count )&& enemyList.Count != 0)
        {
            aimLockNum--;
            if (aimLockNum < 0)
            {
                aimLockNum = 0;
            }
        }
        if(enemyList.Count == 0) { return; }
        aimTargetNmb = enemyList[aimLockNum].GetSetEnemyNmb;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyShot" || collision.gameObject.tag=="Enemy")
        {
            if (invincibleFlag)
            {
                Debug.Log("無敵中");
                return;
            }
            invincibleFlag = true;
            ReduceHP();
        }
        if(collision.gameObject.tag=="HPItem")
        {
            if (HP != MaxHP)
            {
                HP++;
                uiScript.SetHPImage(1, 1);
                audioSource.PlayOneShot(recoveryClip);
            }
            
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag=="EnergyItem")
        {
            energy += 10;
            if (energy >= MaxEnergy)
            {
                energy = MaxEnergy;
            }
            Destroy(collision.gameObject);
        }
    }

    private void ReduceHP()
    {
        audioSource.PlayOneShot(damageClip);
        HP--;
        uiScript.SetHPImage(-1, 1);
        if (HP == 0)
        {
            state = State.Non;
        }
    }

    public void ResurrectionPlayer()
    {
        HP = MaxHP;
        uiScript.SetHPImage(1, HP);
        mesh.SetActive(true);
        invincibleFlag = true;
        state = State.Nomal;
    }

    public int GetSetaimTargetNmb
    {
        get { return aimTargetNmb; }
    }
}


