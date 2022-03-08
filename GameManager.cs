using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private  Player player;
    public static GameManager instance;
    public bool gameover;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<Player>();
    }

    public void Update()
    {
        gameover = player.isDead;
        StartCoroutine(Gameover());
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Gameover()
    {
        yield return new WaitForSeconds(5);
        UIManager.instance.GamerOverUI(gameover);
    }

}
