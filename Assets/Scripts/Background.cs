using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    private Material _material;

    [SerializeField]
    private int XVelocity, YVelocity;

    private Vector2 offset;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Start()
    {
        offset = new Vector2(XVelocity ,YVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        _material.mainTextureOffset += (offset * Time.deltaTime) / 2;
    }
}
