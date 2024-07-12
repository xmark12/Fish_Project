using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header ("Scripts")]
    public UIManager ui;
    public HitPause hitPause;
    public GameManager gm;
    public MainMenuManager mmm;

    [Header("Movement")]
    public float movementSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 0.25f;
    [SerializeField] private TrailRenderer tr;

    [Header("Health")]
    public float maxHP = 3;
    public float currentHP;
    public Image healthBar;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Audio")]
    public AudioSource moveSound;
    public AudioSource dashSound;
    public AudioSource hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt("EasyBool") != 0)
        {
            maxHP = 5;
        }
        if (PlayerPrefs.GetInt("MediumBool") != 0)
        {
            maxHP = 4;
        }
        if (PlayerPrefs.GetInt("HardBool") != 0)
        {
            maxHP = 3;
        }

        currentHP = maxHP;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        
        if ((Input.GetKeyDown(KeyCode.LeftShift) && canDash) || (Input.GetButtonDown("Jump") && canDash))
        {
            if (ui.pauseMenu.activeSelf || ui.gameOverMenu.activeSelf)
                dashSound.Stop();
            else
                dashSound.Play();
            StartCoroutine(Dash());
        }

        if (!ui.pauseMenu.activeSelf && !ui.gameOverMenu.activeSelf && Input.GetKeyDown(KeyCode.P))
        {
            ui.EnablePauseMenu();
        }
        if (ui.pauseMenu.activeSelf && !ui.gameOverMenu.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            ui.Continue();
        }
        if ((ui.pauseMenu.activeSelf || ui.gameOverMenu.activeSelf) && Input.GetKeyDown(KeyCode.R))
        {
            ui.Retry();
        }
        if ((ui.pauseMenu.activeSelf || ui.gameOverMenu.activeSelf) && Input.GetKeyDown(KeyCode.Q))
        {
            mmm.SwitchMainMenu();
        }

        if (!moveSound.isPlaying && currentHP > 0)
        {
            moveSound.Play();
        }
        if (currentHP <= 0)
        {
            moveSound.Stop();
            dashSound.Stop();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (currentHP > 0)
            {
                hurtSound.Play();
                hitPause.Freeze();
                currentHP -= 1;
                healthBar.fillAmount = currentHP / maxHP;

                if (PlayerPrefs.GetInt("HardBool") == 0)
                {
                    gm.speedMultiplier /= 1.5f;
                }

                StartCoroutine(Invulnerability());
            }
            if (currentHP <= 0)
            {
                hurtSound.Play();
                currentHP = 0;
                healthBar.fillAmount = currentHP / maxHP;

                ui.EnableGameOverMenu();
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, speedY);
        tr.emitting = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}