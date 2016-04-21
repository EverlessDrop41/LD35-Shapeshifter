﻿using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ShapeController : MonoBehaviour
{
    public bool beingControlled = false;

    public float MoveSpeed = 10f;
    public float ScaleSpeed = 5f;
    public float MinScale = 0.1f;
    public float MaxScale = 10f;

    public float MaxEnergy = 10f;
    public float EnergyRegenRate = 1f;
    public float MoveCost = 1f;
    public float ScaleCost = 2f;
    public float JumpCost = 4f;
    public Text EnergyDisplay;
    private string EnergyDisplayFormat = "Energy: {0: 00.0;-00.0}";

    public float JumpForce = 10f;

    public float GroundCheckDistance = .1f;
    public LayerMask WhatIsGround;
    public LayerMask WhatStopsScaling;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private float _moveInput;
    private float _scaleInput;
    private bool _wantsToJump;

    private float _currentEnergy;

    public void Awake()
    {
        _currentEnergy = MaxEnergy;
    }

    void Update()
    {
        _moveInput = Input.GetAxis("Move");
        _scaleInput = Input.GetAxis("Scale");
        _wantsToJump = Input.GetButtonDown("Jump");
    }

    public void FixedUpdate()
    {
        if (beingControlled)
        {
            UpdateGUI();
            Vector2 moveVector = _rb.velocity;

			//Check if grounded
			var s = transform.lossyScale;
			float halfWidth = s.x / 2;
			float halfHeight = s.y / 2;

			Vector2 lPos = new Vector2 (transform.position.x - halfWidth, transform.position.y);
			Vector2 rPos = new Vector2 (transform.position.x + halfWidth, transform.position.y);

			RaycastHit2D groundCheckLeft = Physics2D.Raycast(lPos, -Vector2.up, halfHeight + GroundCheckDistance, WhatIsGround);
			RaycastHit2D groundCheckMiddle = Physics2D.Raycast(transform.position, -Vector2.up, halfHeight + GroundCheckDistance, WhatIsGround);
			RaycastHit2D groundCheckRight = Physics2D.Raycast(rPos, -Vector2.up, halfHeight + GroundCheckDistance, WhatIsGround);

			bool grounded = groundCheckLeft || groundCheckMiddle || groundCheckRight;

			Debug.DrawLine(transform.position, 
				new Vector2(transform.position.x, transform.position.y) + (-Vector2.up * (halfHeight + GroundCheckDistance)), 
				groundCheckMiddle ? Color.red : Color.green
			);
			Debug.DrawLine (new Vector3 (lPos.x, lPos.y), 
				new Vector2 (transform.position.x - halfWidth, transform.position.y) + (-Vector2.up * (halfHeight + GroundCheckDistance)), 
				groundCheckLeft ? Color.red : Color.green
			);
			Debug.DrawLine(new Vector3(rPos.x, rPos.y), 
				new Vector2(transform.position.x + halfWidth, transform.position.y) + (-Vector2.up * (halfHeight + GroundCheckDistance)),
				groundCheckRight ? Color.red : Color.green
			);

            if (_currentEnergy > 0)
            {
                float moveAmount = _moveInput * MoveSpeed;
                moveVector = new Vector2(moveAmount, _rb.velocity.y);
                moveVector.x = Mathf.Clamp(moveVector.x, -MoveSpeed, MoveSpeed);

                if (moveAmount != 0)
                {
                    _currentEnergy -= MoveCost*Time.deltaTime;
                }
            }

            //Scale

            //Check Up
            bool canScaleUp = !Physics2D.Raycast(transform.position, Vector2.up, halfHeight + GroundCheckDistance, WhatStopsScaling);
            //Check Down
            bool canScaleDown = !Physics2D.Raycast(transform.position, Vector2.down, halfHeight + GroundCheckDistance, WhatStopsScaling);
            //Check Left
            bool canScaleLeft = !Physics2D.Raycast(transform.position, Vector2.left, halfWidth + GroundCheckDistance, WhatStopsScaling);
            //Check Right
            bool canScaleRight = !Physics2D.Raycast(transform.position, Vector2.right, halfWidth + GroundCheckDistance, WhatStopsScaling);

            bool canIncreaseScale = (canScaleUp || canScaleDown) && (canScaleLeft || canScaleRight);

            if ((_scaleInput < 0 || canIncreaseScale) && _currentEnergy > 0)
            {
                float scaleAmount = _scaleInput * ScaleSpeed;
                Vector3 newScale = transform.localScale + (new Vector3(scaleAmount, scaleAmount) * Time.fixedDeltaTime);
                newScale.x = Mathf.Clamp(newScale.x, MinScale, MaxScale);
                newScale.y = Mathf.Clamp(newScale.y, MinScale, MaxScale);
                transform.localScale = newScale;

                
                if (_scaleInput != 0)
                {
                    Debug.Log("Scaling");
                    _currentEnergy -= ScaleCost * Time.fixedDeltaTime;
                }
                
            }

			//Actually Move
			_rb.velocity = moveVector;

            //Jump
            if (_wantsToJump && grounded && _currentEnergy > JumpCost)
            {
                _currentEnergy -= JumpCost;
                _rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                _wantsToJump = false;
            }
        }

        //Energy Regen
        if (_currentEnergy < MaxEnergy)
        {
            _currentEnergy += EnergyRegenRate * Time.fixedDeltaTime;
        }
        else
        {
            _currentEnergy = MaxEnergy;
        }
    }

    public void AddEnergy(float amount)
    {
        _currentEnergy += amount;
        if (_currentEnergy > MaxEnergy) _currentEnergy = MaxEnergy;
    } 

    private void UpdateGUI()
    {
        if (EnergyDisplay)
        {
            EnergyDisplay.text = string.Format(EnergyDisplayFormat, _currentEnergy);
        }
    }
}