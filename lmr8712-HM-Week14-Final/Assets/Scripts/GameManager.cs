using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Space to store the text that is going to change to display current stats
    public GameObject hungerText;
    public GameObject happyText;
    public GameObject restText;
    public GameObject moneyText;

    // Space to store everything related to the alien visual
    public GameObject alien;
    public GameObject alienPanel;
    public GameObject[] alienList;
    
    // Space to store related to the name of the alien
    public GameObject nameText;
    public GameObject namePanel;
    public GameObject nameInput;
    
    // Space to store everything related to the stage
    public GameObject stagePanel;
    public Sprite[] stageTileSprites;
    public GameObject[] stageTiles;
    public GameObject stage;
    public Sprite[] stageOptions;
    
    // Space to store panels
    public GameObject foodPanel;
    public GameObject toyPanel;
    public GameObject jobPanel;
    
    // Variables to keep track of time
    public float runTime;
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;                                          //make the timer start a Time.time
        
        if (!PlayerPrefs.HasKey("looks")) {                          //if the player has no established preference for alien visual
            PlayerPrefs.SetInt("looks", 0);                          //set the alien visual to the first alien on the list
        }
        createAlien(PlayerPrefs.GetInt("looks"));               //get the players preference and create the alien
        
        if (!PlayerPrefs.HasKey("tiles")) {                          //if the player has no established preference for the tiles
            PlayerPrefs.SetInt("tiles", 0);                          //set the tiles to the first tiles on the list
        }
        changeTiles(PlayerPrefs.GetInt("tiles"));               //get the players preference and create the tiles
        
        if (!PlayerPrefs.HasKey("background")) {                     //if the player has no established preference for background
            PlayerPrefs.SetInt("background", 0);                     //set the background to the first background on the list
        }
        changeBackground(PlayerPrefs.GetInt("background"));     //get the players preference and create the alien
    }

    // Update is called once per frame
    void Update()
    {
        runTime = Time.time - timer;    //make the time run
        Debug.Log(runTime);             //for testing purpose to keep track of the passing of time
        
        // Update the stats so the player can know were they are standing
        happyText.GetComponent<Text>().text = "" + alien.GetComponent<AlienStats>().Happy;
        hungerText.GetComponent<Text>().text = "" + alien.GetComponent<AlienStats>().Hunger;
        restText.GetComponent<Text>().text = "" + alien.GetComponent<AlienStats>().Rest;
        moneyText.GetComponent<Text>().text = "" + alien.GetComponent<AlienStats>().Money;
        nameText.GetComponent<Text>().text = alien.GetComponent<AlienStats>().Name;
    }
    
    // Called when editing the Name of the alien
    public void triggerNamePanel(bool b) {
        namePanel.SetActive(!namePanel.activeInHierarchy);  //activate the panel
        if (b) {
            alien.GetComponent<AlienStats>().Name = nameInput.GetComponent<InputField>().text;  //change the name to the new name
            PlayerPrefs.SetString("name", alien.GetComponent<AlienStats>().Name);               //save name as preference 
        }
    }
    
    // When pressing a button
    public void buttonBehaviour(int i) {
        switch (i) {
            case 0:                                                     //if first button pressed
                alienPanel.SetActive(!alienPanel.activeInHierarchy);    //active or deactivate alien panel
                break;
            
            case 1:                                                     //if second button pressed
                stagePanel.SetActive(!stagePanel.activeInHierarchy);    //active or deactivate stage panel
                break;
            
            case 2:                                                     //if third button pressed
                foodPanel.SetActive(!foodPanel.activeInHierarchy);      //active or deactivate food panel
                break;
            
            case 3:                                                     //if forth button pressed
                toyPanel.SetActive(!toyPanel.activeInHierarchy);        //active or deactivate toy panel
                break;
            
            case 4:                                                     //if fifth button pressed
                jobPanel.SetActive(!jobPanel.activeInHierarchy);        //active or deactivate job panel
                break;
            
            case 5:                                                     //if sixth button pressed
                //todo add music
            
            case 6:                                                      //if seventh button pressed
                alien.GetComponent<AlienStats>().OnApplicationQuit();    //run the OnApplicationQuit function on the alien stats script
                Application.Quit();                                      //quit the application
                Debug.Log("bye");                                 //for testing purpose 
                break;
        }
    }
    
    // Function to change alien
    public void createAlien(int i) {
        if (alien) {                                                    //if an alien exists
            Destroy(alien);                                             //destroy it
        }
        alien = Instantiate(alienList[i], Vector3.zero, Quaternion.identity);   //create new alien according to alien selected
        
        toggle(alienPanel);                                             //turn off the panel
        PlayerPrefs.SetInt("looks", i);                                 //set this alien as the alien visual preference
    }
    
    // Function to change ground tiles
    public void changeTiles(int t) {
        for (int i = 0; i < stageTiles.Length; i++) {                //for every tile
            stageTiles[i].GetComponent<SpriteRenderer>().sprite = stageTileSprites[t]; //get the sprite component and change it to the tile selected
        }

        toggle(stagePanel);                                         //turn off the panel
        PlayerPrefs.SetInt("tiles", t);                             //set this tiles as the tiles preference
    }
    
    // Function to change background
    public void changeBackground(int i) {
        stage.GetComponent<SpriteRenderer>().sprite = stageOptions[i];  //get the sprite component and change it to the background selected
        toggle(stagePanel);                                             //turn off the panel
        PlayerPrefs.SetInt("background", i);                            //set this tiles as the background preference
    }

    // Function for food selection
    public void selectFood(int i) {
        toggle(foodPanel);                                                //turn on the panel
        switch (i) {
            case 0:                                                       //if first button pressed
                if(alien.GetComponent<AlienStats>().Money >= 3){          //and player has more than 3 moneys
                    alien.GetComponent<AlienStats>().UpdateHunger(1);   //raise happy by 1
                    alien.GetComponent<AlienStats>().UpdateMoney(-3);   //lower money by 3
                }
                break;

            case 1:                                                       //if second button pressed
                if(alien.GetComponent<AlienStats>().Money >= 10){         //and player has more than 10 moneys
                    alien.GetComponent<AlienStats>().UpdateHunger(3);   //raise happy by 3
                    alien.GetComponent<AlienStats>().UpdateMoney(-10);  //lower money by 10
                }
                break;

            case 2:                                                       //if third button pressed
                if(alien.GetComponent<AlienStats>().Money >= 20){         //and player has more than 20 moneys
                    alien.GetComponent<AlienStats>().UpdateHunger(5);   //raise happy by 5
                    alien.GetComponent<AlienStats>().UpdateMoney(-20);  //lower money by 20
                }
                break;
        }
    }

    // Function for toy selection
    public void selectToy(int i) {
        toggle(toyPanel);                                                //turn on the panel
        switch (i) {
            case 0:                                                      //if first button pressed
                if(alien.GetComponent<AlienStats>().Rest >= 3){          //and player has more than 3 rest
                    alien.GetComponent<AlienStats>().UpdateHappy(3);   //raise happy by 3
                    alien.GetComponent<AlienStats>().UpdateRest(-3);   //lower rest by 3
                }
                break;

            case 1:                                                      //if second button pressed
                if(alien.GetComponent<AlienStats>().Rest >= 10){         //and player has more than 10 rest
                    alien.GetComponent<AlienStats>().UpdateHappy(5);   //raise happy by 5
                    alien.GetComponent<AlienStats>().UpdateRest(-10);  //lower rest by 10
                }
                break;
        }
    }
    
    // Function for toy selection
    public void selectJob(int i)
    {
        toggle(jobPanel);                                                 //turn on the panel
        switch (i) {
            case 0:                                                       //if first button pressed
                if(alien.GetComponent<AlienStats>().Rest >= 6){           //and player has more than 10 moneys
                    alien.GetComponent<AlienStats>().UpdateMoney(3);    //raise happy by 3
                    alien.GetComponent<AlienStats>().UpdateRest(-6);    //lower money by -6
                }
                break;

            case 1:                                                       //if second button pressed
                if(alien.GetComponent<AlienStats>().Rest >= 10){          //and player has more than 10 rest
                    alien.GetComponent<AlienStats>().UpdateMoney(5);    //raise money by 5
                    alien.GetComponent<AlienStats>().UpdateRest(-10);   //lower rest by -10
                }
                break;
        }
    }
    
    // Function to turn off panels
    public void toggle(GameObject panel) {
        if (panel.activeInHierarchy){     //if panel active
            panel.SetActive(false);       //turn it off
        }  
    }
    
}
