using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Planets
{
    public class ObjectController : MonoBehaviour
    {
        private RectTransform _canvas;
        private RectTransform _progressBar;

        private SpriteRenderer _spriteRenderer;
        private bool _selected;

        private Image _progressBarImage;
        private float _timeLeft;
        private bool _isEnabled;

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
            _progressBar = _canvas.Find("Progress Bar").GetComponent<RectTransform>();
            _progressBarImage = _progressBar.gameObject.GetComponent<Image>();
            _progressBarImage.fillAmount = 0.0f;
            _timeLeft = 3f;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_selected)
            {
                return;
            }

            // var mousePos = Input.mousePosition - _canvas.localPosition -
            //                new Vector3(Screen.width / 2f, Screen.height / 2f);
            // _progressBar.localPosition = mousePos;

            // if (Input.GetMouseButtonDown(1))
            // {
            //     Enable();
            // }
            //
            // if (Input.GetMouseButtonUp(1))
            // {
            //     Disable();
            // }

            if (_isEnabled)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime;
                    _progressBarImage.fillAmount = Mathf.Lerp(1.0f, 0.0f, _timeLeft / 3.0f);

                    if (_timeLeft <= 0)
                    {
                        Disable();
                        OnTimer();
                        _isEnabled = false;
                    }
                }
            }
        }

        private void Enable()
        {
            _isEnabled = true;
            _progressBarImage.enabled = true;
        }

        private void Disable()
        {
            _isEnabled = false;
            _progressBarImage.enabled = false;
        }

        private void OnMouseEnter()
        {
            _spriteRenderer.color = Color.green;
            _selected = true;

            // if (Input.GetMouseButton(1))
            // {
            //     Enable();
            // }
        }

        private void OnMouseExit()
        {
            _spriteRenderer.color = Color.white;
            _selected = false;
            Disable();
        }

        private void OnTimer()
        {
            if (_selected)
            {
                Destroy(gameObject);
            }
        }
    }
}
