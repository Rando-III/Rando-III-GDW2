using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyCount : MonoBehaviour
{
    public FloatSO dummy;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = dummy.value.ToString();
    }
}
