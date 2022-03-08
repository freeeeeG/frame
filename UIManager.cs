using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject healthbar;

    public GameObject gameover;
    public GameObject pausemenu;
    [Header("UI Elements")] 
    public GameObject pauseMenu;
    public Slider bosSlider;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void healthupdate(float health)
    {
        switch (health)
        {
            case 3:
                healthbar.transform.GetChild(0).gameObject.SetActive(true);
                healthbar.transform.GetChild(1).gameObject.SetActive(true);
                healthbar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                healthbar.transform.GetChild(0).gameObject.SetActive(true);
                healthbar.transform.GetChild(1).gameObject.SetActive(true);
                healthbar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                healthbar.transform.GetChild(0).gameObject.SetActive(true);
                healthbar.transform.GetChild(1).gameObject.SetActive(false);
                healthbar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                healthbar.transform.GetChild(0).gameObject.SetActive(false);
                healthbar.transform.GetChild(1).gameObject.SetActive(false);
                healthbar.transform.GetChild(2).gameObject.SetActive(false);
                break;

        }
    }

    public void PauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetbossHealth(float health)
    {
        bosSlider.maxValue = health;
    }

    public void updateBossHealth(float health)
    {
        bosSlider.value = health;
    }

    public void GamerOverUI(bool player)
    {
        gameover.SetActive(player);
        if (player)
            Time.timeScale = 0;
    }
}
