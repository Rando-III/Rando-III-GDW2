using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{

    [SerializeField] LevelManager levelManager;
    public GameObject player;
    public string level;
    public GameObject PortalAnim;
    public GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        fade.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelManager.savetimes();
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
        yield return new WaitForSeconds(1);
        fade.GetComponent<Animator>().Play("fade out");
        yield return new WaitForSeconds (2);
        End();
    }
    
}
