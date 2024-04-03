using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    
    
    public GameObject player;
    public string level;
    public GameObject PortalAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(SceneTransition());
        }
    }
    void StartEnd()
    {
        Instantiate(PortalAnim, transform.position - new Vector3 (0,3,0), Quaternion.identity);
        player.SetActive(false);
        
    }
    void End()
    {

        SceneManager.LoadScene(level);
    }
    public IEnumerator SceneTransition()
    {
        StartEnd ();
        yield return new WaitForSeconds (3);
        End();
    }
    
}
