using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mouseFollow : MonoBehaviour
{

    [SerializeField] Camera mainCam;

    [SerializeField] Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouse(InputValue value)
    {
        var mouse_pos = value.Get<Vector2>();
        var mouse = mainCam.ScreenToWorldPoint(mouse_pos);
        
        _transform.position = new Vector3(mouse.x, mouse.y);
        

    }
}
