using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    [Header("Joystick control")]
    public FloatingJoystick joystick;
    [SerializeField] 
    private float _sensitive = 1f;
    [SerializeField] 
    private float _speedModifier = 1;
    
    [Header("LineRenderer settings")]
    [SerializeField]
    private float _rayLength;
    
    [Header("Player settings")]
    [SerializeField] 
    private float _jumpForce;
    [SerializeField] 
    private float _longJumpTime;
    [SerializeField] private float _playerSpeed;
    
    [Header("UI")] 
    public GameObject perfectText;
    public GameObject longJumpText;
    public GameObject startGameUI;
    public Slider slider;
    public GameObject joystickObject;
    public GameObject loseText;
    public GameObject sliderObject;
    public GameObject winText;
    public Text coins;
    public GameObject pointsText;
    public Text pointsTextCount;
    public GameObject comboText;
    public Text comboPoints;

    [Header("Effects")] 
    public GameObject repulsionEffect;
    public GameObject firework;
    public GameObject confetti;

    [Header("SkyBox")] 
    public Material skyBox1;
    public Material skyBox2;
    public Material skyBox3;
    public Material skyBox4;

    [Header("SkyBox")] 
    public AudioSource buttonSound;
    public AudioSource jumpSound;
    public AudioSource coinSound;

    
    [Header("Other Settings")] 
    public LevelGenerate level;

    private Animator anim;
    private LineRenderer _lineRenderer;
    private Color startColor;
    private Color secondColor;
    private Rigidbody rb;
    private int _currentPlatform;
    private GameObject lineRendererObject;
    private float _timer;
    private float _deltaX;
    private float _deltaY;
    public SaveData saveData;
    public GameObject camera;
    private ParticleSystem pSystem;
    private UIButtonInfo _buttonMove; 
    public int distanceTraveled;
    public LevelGenerate levelGenerate;
    public List<GameObject> platforms;
    private float combo;
    private bool _lookAt;
    private float losePosition;
    private float points;
    public bool cameraFinish = true;

    void Awake()
    {
        switch (saveData.level)
        {
            case 0:
                losePosition = -176f;
                RenderSettings.skybox = skyBox1;
                break;
            case 1:
                losePosition = -176f;
                RenderSettings.skybox = skyBox2;
                break;
            case 2:
                losePosition = -176f;
                RenderSettings.skybox = skyBox3;
                break;
            case 3:
                losePosition = -290.7f;
                RenderSettings.skybox = skyBox4;
                break;
            case 4:
                losePosition = -176.7f;
                RenderSettings.skybox = skyBox3;
                break;
            case 5:
                losePosition = -176.7f;
                RenderSettings.skybox = skyBox1;
                break;
        }
        
        _buttonMove = joystick.GetComponent<UIButtonInfo>();
        pSystem = gameObject.transform.Find("FireWork").GetComponent<ParticleSystem>();
        lineRendererObject = transform.Find("LineRenderer").gameObject;
        _lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
    }

    void Start()
    {
        
        _deltaX = 274;
        _lookAt = false;
        Time.timeScale = 0;
        _timer = 0;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        startColor = _lineRenderer.startColor;
        secondColor = Color.green;
        secondColor.a = 0.5f;

        float max;
        switch (saveData.level)
        {
            case 0:
                slider.minValue = transform.position.z;
                slider.maxValue = GameObject.Find("Finish").transform.position.z - 10;
                break;
            case 1:
                slider.minValue = transform.position.x;
                max = GameObject.Find("Finish").transform.position.x;
                slider.maxValue = Mathf.Abs(max) - 40;
                break;
            case 2:
                slider.minValue = transform.position.x;
                max = GameObject.Find("Finish").transform.position.x;
                slider.maxValue = Mathf.Abs(max) - 65;
                break;
            case 3:
                slider.minValue = transform.position.x;
                max = GameObject.Find("Finish").transform.position.x;
                slider.maxValue = Mathf.Abs(max) - 75;
                break;
            case 4:
                slider.minValue = transform.position.x;
                max = GameObject.Find("Finish").transform.position.x;
                slider.maxValue = Mathf.Abs(max) - 75;
                break;
            case 5:
                slider.minValue = transform.position.x;
                max = GameObject.Find("Finish").transform.position.x;
                slider.maxValue = Mathf.Abs(max) - 75;
                break;
        }
    }

    void FixedUpdate()
    {
        if (saveData.level == 0)
            slider.value = transform.position.z;
        
        else slider.value = Mathf.Abs(transform.position.x);
        
        transform.Translate(Vector3.forward * joystick.Direction.y/2);
        
        _deltaX += (joystick.Direction.x + 1 ) * (joystick.Direction.x + 1) * _sensitive - _sensitive;
        _deltaY += (joystick.Direction.y + 1) * (joystick.Direction.y + 1) * _sensitive - _sensitive;

        transform.rotation = Quaternion.Euler(0, _deltaX * _speedModifier, 0);

        pSystem.Simulate(Time.unscaledDeltaTime,true,false);
        
        _timer += Time.deltaTime;
    }

    void LateUpdate()
    {
        _lineRenderer.SetPosition(0,transform.position);
        var secondLinePosition = new Vector3(transform.position.x, transform.position.y - _rayLength, transform.position.z);
        _lineRenderer.SetPosition(1,secondLinePosition);

        Ray ray = new Ray(transform.position, Vector3.down * _rayLength);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Trampoline")
                _lineRenderer.SetColors(secondColor, secondColor);
        }
        else _lineRenderer.SetColors(startColor, startColor);
        
        
        if (transform.position.y < losePosition)
        {
            Time.timeScale = 0;
            loseText.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            cameraFinish = false;
            Time.timeScale = 0;
            camera.GetComponent<Animator>().SetTrigger("finish");
            StartCoroutine(FlipTimeFinish());
            winText.SetActive(true);
            firework.SetActive(true);
            coins.text = saveData.coins.ToString();
            saveData.currentLevel = saveData.level;
            //saveData.level++;
        }
        
        if (collision.gameObject.tag == "Trampoline")
        {
            jumpSound.Play();
            if (collision.gameObject.transform.parent.GetComponent<Platform>().pointBool)
                points += 1;
            rb.velocity = Vector3.up * _jumpForce;
            anim.SetTrigger("flip");
            Instantiate(repulsionEffect, transform.position, Quaternion.identity);
            repulsionEffect.SetActive(true);

            if (_timer >= _longJumpTime)
            {
                _timer = 0;
                longJumpText.SetActive(true);
                StartCoroutine(ShutdownAnimation(longJumpText));
            }

            if (collision.gameObject.name == "Bull's-eye")
            {
                if (_timer < _longJumpTime)
                {
                    _timer = 0;
                    //Time.timeScale = 0.4f;
                    //StartCoroutine(PerfectJumpSlowTime());
                    //StartCoroutine(ShutdownAnimation(perfectText));
                }

                if (collision.gameObject.transform.parent.GetComponent<Platform>().perfect && collision.gameObject.transform.parent.GetComponent<Platform>().pointBool)
                {
                    combo += 1;
                    points += (10 * combo);
                    perfectText.SetActive(true);
                    comboText.SetActive(true);
                    comboPoints.text = combo.ToString();
                    Instantiate(confetti, transform.position, Quaternion.identity);
                    StartCoroutine(ShutdownAnimation(perfectText));
                    StartCoroutine(ShutdownAnimation(comboText));
                    collision.gameObject.transform.parent.GetComponent<Platform>().perfect = false;
                }
            }
            else combo = 0;

            collision.gameObject.transform.parent.GetComponent<Platform>().pointBool = false;
            pointsTextCount.text = points.ToString();
            collision.gameObject.transform.parent.GetComponent<Platform>().destruction++;
            //int.TryParse(collision.transform.parent.Find("Text").GetComponent<TextMeshPro>().text, out var helpfulOut);
            //distanceTraveled = helpfulOut;
        }

        if (collision.gameObject.tag == "Trampoline2")
        {
            jumpSound.Play();
            rb.velocity = Vector3.up * _jumpForce*3;
            anim.SetTrigger("flip");
            Instantiate(repulsionEffect, transform.position, Quaternion.identity);
            repulsionEffect.SetActive(true);
        }
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            coinSound.Play();
            Destroy(collider.gameObject);
            saveData.coins++;
        }
    }
    public void StartGame()
    {
        startGameUI.SetActive(false);
        camera.gameObject.GetComponent<Animator>().SetTrigger("lookAround");
        StartCoroutine(TimeNormalization());
    }

    public void RestartGame()
    {
        buttonSound.Play();
        SceneManager.LoadScene("Menu");
    }

    IEnumerator PerfectJumpSlowTime()
    {
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;
    }

    IEnumerator FlipTimeFinish()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        anim.SetTrigger("flip");
    }

    IEnumerator TimeNormalization()
    {
        joystickObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.50f);
        sliderObject.SetActive(true);
        camera.GetComponent<Animator>().SetTrigger("default");
        Time.timeScale = 1;
    }
    
    IEnumerator ShutdownAnimation(GameObject UI)
    {
        yield return new WaitForSeconds(1f);
        UI.SetActive(false);
    }
    
}
