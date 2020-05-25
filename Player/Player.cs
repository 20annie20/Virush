using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Creature, IDamageable
{
    private Rigidbody2D playerbody;
    [SerializeField]
    private readonly float jumpforce = 5.0f;
    private bool resetJumpNeeded = false;
    public float speed = 4.0f;
    private PlayerAnimation _playerAnim;
    public SpriteRenderer _playerSprite;
    public SpriteRenderer _bulletSprite;
    public GameObject BulletPrefab;

    public int Level { get; set; } = 1;

    private int pickedCoins;
    public PauseMenu menu;

    public int MaxHealth { get; set; }  

    private int damageValue;
    public int DamageValue { 
        get => damageValue; 
        set => damageValue = value; 
    }

    private float speedRate;
    public float SpeedRate
    {
        get => speedRate;
        set => speedRate = value;
    }

    private void Start()
    {
        playerbody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _bulletSprite = BulletPrefab.GetComponentInChildren<SpriteRenderer>();
        isAlive = true;
        LoadPlayer();
        //Level = 1;
        Health = ResetHealth();
        UIManager.Instance.RenewPanelCoins(Coins);
        UIManager.Instance.RenewPanelHealth(Health);
        Debug.Log("Health: " + Health + ", coins: " + Coins);
        if(speedRate == 0)
        {
            Debug.Log("speed rate = " + SpeedRate);
            speedRate = 1;        
        }
        if(damageValue == 0)
        {
            Debug.Log("damage value = " + DamageValue);
            damageValue = 1;
        }
        if (MaxHealth == 0)   // add value 10 because on start it is 0
        {
            Debug.Log("max health = " + MaxHealth);
            MaxHealth = 10;
        }
   
 
    }

    private void Update()
    {
        if (isAlive && PauseMenu.isGamePaused == false)
        {
            Jump();
            Movement();
            Attack();
        }
    }

    public void GetDamage(int power)
    {
        Health--;
        UIManager.Instance.RenewPanelHealth(Health);
        if (Health < 1)
        {

            Debug.Log("Player dies");
            isAlive = false;
            _playerAnim.Death();
            WaitAMomentRoutine();
            UIManager.Instance.OpenGameOver();
            pickedCoins = 0;
            Health = MaxHealth;
            
        }
    }

    public void AddCoins(int amount)
    {
        pickedCoins += amount;
        UIManager.Instance.RenewPanelCoins(Coins + pickedCoins);
    }

    public void Movement()
    {
        MoveHorizontally();
    }
    private void MoveHorizontally()
    {
        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        if (move > 0)
        {
            _playerSprite.flipX = false;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
        }

        playerbody.velocity = new Vector2(move * speed * speedRate, playerbody.velocity.y);
        _playerAnim.Move(move);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() || CrossPlatformInputManager.GetButtonDown("JumpButton") && IsGrounded())
        {
            playerbody.velocity = new Vector2(playerbody.velocity.x, jumpforce);
            _playerAnim.Jump(true);
            StartCoroutine(ResetJumpRoutine());
        }
        else
            _playerAnim.Jump(false);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, 1 << 8); //nie pomijać informacji o wykrywanej warstwie
        Debug.DrawRay(transform.position, Vector2.down, Color.red);
        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }

    public void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Q) || CrossPlatformInputManager.GetButtonDown("ShootButton"))
        {
            if (_playerSprite.flipX == true)
            {

                _bulletSprite.flipX = true;
            }
            else
            {
                _bulletSprite.flipX = false;
            }

            _playerAnim.Attack();
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        }
    }
    public int ResetHealth()
    {
        return MaxHealth;
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Level = data.level;
        MaxHealth = data.maxHealth;
        Coins = data.coins;
        SpeedRate = data.speedRate;
        DamageValue = data.damageValue;
    }

    public void FinishLevel()
    {
        coins += pickedCoins;
        if(Level < 3)
        {
            Debug.Log("Level passed");
            UIManager.Instance.DisplayCongratulations();
            Level++;
        }
        else
        {
            //display congrats, finished game
            UIManager.Instance.DisplayFinishedGame();
        }
        
        SavePlayer();
    }

    IEnumerator WaitAMomentRoutine()
    {
        yield return new WaitForSeconds(1.5f);
    }
}

