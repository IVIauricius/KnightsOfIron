using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI: MonoBehaviour
{
    public Slider LeftTroopHealth;
    public Slider RightTroopHealth;
    public SoldierHealth MySoldierHealth;
    public Text OnesPlace;
    public Text TensPlace;
    public Text HundredsPlace;
    public AccuracyController MyAccuracyController;
    public Slider MyLoadingSlider;
    public Slider MyHealthSlider;
    public PlayerHealth WhateverHealth;
    public GameObject Enemy_Health;
    public GameObject Enemy_Loading;
    public Image enemySight;
    public Text EnemyLoadingSliderValueText;
    public Text PlayerLoadingSliderValueText;
    public Text EnemyHealthSliderValueText;
    public Image PauseScreen;
    public Image Crosshairs;
    

	public void SceneTransition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Awake()
    {
        //MyHealthSlider.maxValue = WhateverHealth.maxHealth;
        //MyHealthSlider.value = WhateverHealth.currentHealth;



        //LeftTroopHealth.maxValue = MySoldierHealth.maxHealth;
        //LeftTroopHealth.value = MySoldierHealth.currentHealth;

        //RightTroopHealth.maxValue = MySoldierHealth.maxHealth;
        //RightTroopHealth.value = MySoldierHealth.currentHealth;


    }

    public void PauseToggle()
    {
        PauseScreen.enabled = !PauseScreen.enabled;
    }

    public void CrosshairsToggle()
    {
        print("WORK");
        if(Crosshairs.enabled)
        {
            Crosshairs.enabled = true;
        }
        else
        {
            Crosshairs.enabled = false;
        }
    }

    //void Update()
    //{
    //    MyLoadingSlider.value = MyAccuracyController.GetTimer();
    //    MyHealthSlider.value = WhateverHealth.currentHealth;
    //    //PlayerHealthSliderValueText.text = WhateverHealth.currentHealth.ToString();
    //    PlayerLoadingSliderValueText.text = MyAccuracyController.GetTimer().ToString();

    //    if(Input.GetKey(KeyCode.J))
    //    {
    //        enemySight.enabled = false;
    //        Enemy_Health.SetActive(true);
    //        Enemy_Loading.SetActive(true);

    //    }
    //    else if (Input.GetKey(KeyCode.K))
    //    {
    //        enemySight.enabled = true;
    //        Enemy_Health.SetActive(false);
    //        Enemy_Loading.SetActive(false);
    //    }


    //    int ones = ((int)WhateverHealth.currentHealth) % 10;
    //    int tens = ((int)(WhateverHealth.currentHealth % 100)) / 10;
    //    int hundreds = ((int)WhateverHealth.currentHealth) / 100;

    //    HundredsPlace.text = hundreds.ToString();
    //    TensPlace.text = tens.ToString();
    //    OnesPlace.text = ones.ToString();

    //}

    public void OnEnemyLoadingSliderValueChanged(Slider enemyLoadingSlider)
    {
        EnemyLoadingSliderValueText.text = enemyLoadingSlider.value.ToString();
    }

    public void OnPlayerLoadingSliderValueChanged(Slider playerLoadingSlider)
    {
        PlayerLoadingSliderValueText.text = playerLoadingSlider.value.ToString();
    }

    public void OnEnemyHealthSliderValueChanged(Slider enemyHealthSlider)
    {
        EnemyHealthSliderValueText.text = enemyHealthSlider.value.ToString();
    }

    public void OnPlayerHealthSliderValueChanged(Slider playerHealthSlider)
    {
        //PlayerHealthSliderValueText.text = playerHealthSlider.value.ToString();
    }

}
