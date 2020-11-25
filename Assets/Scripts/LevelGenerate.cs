using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerate : MonoBehaviour
{
    private Vector3 pole;
    
    [Header("Spiral setup")] 
    [SerializeField] private float _a = 60;
    [SerializeField] private float phi;
    
    [SerializeField] private int numOfPlatforms;
    
    public GameObject platform;
    public List<GameObject> platforms;
    private Vector3 point;

    private TextMeshPro text;

    void Start()
    {
        /*platforms.Add(Instantiate(platform, point, Quaternion.identity));
        platforms[i-6].transform.Find("Text").GetComponent<TextMeshPro>().text = (i-5).ToString();*/
        
    }
}
