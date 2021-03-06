﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	private Animator _animator;
	private CanvasGroup _canvasGroup;



	public void Awake() {
		_animator = GetComponent<Animator> ();
		_canvasGroup = GetComponent<CanvasGroup> ();

		RectTransform rect = GetComponent<RectTransform> ();
		rect.offsetMax = rect.offsetMin = new Vector2 (0, 0);
	}

	public void Update() {
		if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("Open"))
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		else
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
	}

	public bool IsOpen
	{
		get { return _animator.GetBool ("IsOpen"); }
		set { _animator.SetBool ("IsOpen", value); }
	}

}
