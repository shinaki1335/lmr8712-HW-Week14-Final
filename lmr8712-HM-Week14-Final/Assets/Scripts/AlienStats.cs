using UnityEngine;

public class AlienStats : MonoBehaviour
{
    // Variable to control the stats of the player
    private int hunger;             //control the hunger level
    private int maxHunger = 100;    //establish maximum amount of hunger 
    private int happy;              //control the happy of the player
    private int maxHappy = 100;     //establish maximum amount of happy 
    private int rest;               //control the rest of the player
    private int maxRest = 100;      //establish maximum amount of rest 
    private int money;              //control the money of the player
    private int startMoney = 25;    //determine the starting money of the player for the first time they open the game
    private int clickCount;         //keeps tracks of how many time the players clicks the alien
    private string name;            //keeps track of the name of the alien

    // Property for the hunger to allow access to the private variable 
    public int Hunger {
        get {return hunger;}        //get the hunger variable
        set {hunger = value;}       //set it to value
    }
    
    // Property for the happy to allow access to the private variable 
    public int Happy {
        get {return happy;}         //get the happy variable
        set {happy = value;}        //set it to value
    }
    
    // Property for the rest to allow access to the private variable    
    public int Rest {
        get {return rest;}          //get the rest variable
        set {rest = value;}         //set it to value
    }

    // Property for the money to allow access to the private variable  
    public int Money {
        get {return money;}          //get the money variable
        set {money = value;}         //set it to value
    }

    // Property for the name to allow access to the private variable  
    public string Name {
        get {return name;}          //get the name variable
        set {name = value;}         //set it to value
    }
    
    // Start is called before the first frame update
    void Start() {
        UpdateStatus();                                            //call the UpdateStatus to establish the stats for the alien 
        InvokeRepeating("Hungry", 2, 2);    //wait 2 seconds and call the Hungry function every 2 seconds
        InvokeRepeating("Bored",5,5);       //wait 5 seconds and call the Bored function every 5 seconds
        InvokeRepeating("Tired", 10,10);    //wait 10 seconds and call the Tired function every 10 seconds

        if (!PlayerPrefs.HasKey("name")) {                         //if the player has no established preference for name
            PlayerPrefs.SetString("name", "alien");                //set the name to alien
        }
        else {                                                     //if the player has a name preference
            name = PlayerPrefs.GetString("name");               //set the name to the preference
        }
    }

    // Update is called once per frame
    void Update() {
        
        // Get the animator component when and use the jump animation when the aliens positions is greater than -2.9
        GetComponent<Animator>().SetBool("jump", gameObject.transform.position.y > -2.9f); 
        
        // Called pressing the left mouse button, check if clicking on the alien
        if (Input.GetMouseButtonUp(0)) {
            Vector2 alien = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D touch = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(alien), Vector2.zero);
            
            // Called when clicking the alien
            if (touch) {
                if (touch.transform.gameObject.tag == "alien") {  
                    clickCount++;                                       //raise the click count
                    if (clickCount >= 3) {                              //if click count is 3 or higher
                        clickCount = 0;                                 //reset click count
                        UpdateHappy(1);                                //call the UpdateHappy function with a value of 1
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000000));  //add force to the alien to make it jump
                    }
                }
            }
        }
        
        //This is for testing purpose, it reset the stats to the values of the first game they played
        if (Input.GetKeyUp(KeyCode.R)) {
            hunger = maxHunger;
            happy = maxHappy;
            rest = maxRest;
            money = 25;
        }
    }
    // Function to update the stats when called
    void UpdateStatus() {
        if (!PlayerPrefs.HasKey("hunger")) {               //if the player has no established preference for hunger
            hunger = maxHunger;                            //set the hunger to max hunger       
            PlayerPrefs.SetInt("hunger", hunger);          //set this a the new hunger preference
        }
        else {                                             //if players has a established preference for hunger
            hunger = PlayerPrefs.GetInt("hunger");      //get hunger preference
            PlayerPrefs.SetInt("hunger", hunger);          //set hunger to preference
        }

        if (!PlayerPrefs.HasKey("happy")) {                //if the player has no established preference for happy
            happy = maxHappy;                              //set the happy to max happy   
            PlayerPrefs.SetInt("happy", happy);            //set this a the new happy preference
        }
        else {                                             //if players has a established preference for happy
            happy = PlayerPrefs.GetInt("happy");        //get happy preference
        }

        if (!PlayerPrefs.HasKey("rest")) {                 //if the player has no established preference for rest
            rest = maxRest;                                //set the rest to max rest   
            PlayerPrefs.SetInt("rest", rest);              //set this a the new rest preference
        }
        else {                                             //if players has a established preference for rest
            rest = PlayerPrefs.GetInt("rest");          //get rest preference
        }

        if (!PlayerPrefs.HasKey("money")) {                //if the player has no established preference for money
            money = 25;                                    //set the money to 25
            PlayerPrefs.SetInt("money", money);            //set this a the new money preference
        }
        else {                                             //if players has a established preference for money
            money = PlayerPrefs.GetInt("money");        //get money preference
        }
    }
    
    // Function to manipulate hunger and its side effects
    void Hungry() {
        if (hunger > 0) {                                                              //if hunger is higher than 0
            UpdateHunger(-1);                                                         //lower hunger by 1
        }

        if (happy > 0) {                                                               //if happy is higher than 0 
            if (hunger == 90 || hunger == 80 || hunger == 70 || hunger == 60) {        //and hunger is 90, 80, 70 or 60
                UpdateHappy(-2);                                                      //lower happy by 2                           
            }
            
            else if (hunger == 50 || hunger == 40 || hunger == 30 || hunger == 20) {    //if hunger is 50, 40, 30 or 20
                UpdateHappy(-5);                                                      //lower happy by 5
            }
            
            else if (hunger == 10 || hunger == 0) {                                     //if hunger is 10 or 0
                UpdateHappy(-10);                                                      //lower happy by 10
            }
        }
    }
    
    // Function to manipulate happy
    void Bored() {
        if (happy > 0) {            //if happy is higher than 0
            UpdateHappy(-5);       //lower happy by 5
        }
    }
    
    // Function to manipulate rest
    void Tired() {
        if (rest > 0) {         //if happy is higher than 0
            UpdateRest(-2);    //lower rest by 2
        }
    }
    
    // Function to manipulate happy according to the number given
    public void UpdateHappy(int i) {
        happy += i;                 //manipulate happy according to the value given

        if (happy > maxHappy) {     //if happy goes over maximum happy                           
            happy = maxHappy;       //happy is maximum happy
        }

        if (happy < 0) {            //if happy is less than 0
            happy = 0;              // make happy 0
        }
    }

    public void UpdateHunger(int i) {
        hunger += i;                 //manipulate hunger according to the value given
        
        if (hunger > maxHunger) {   //if hunger goes over maximum hunger 
            hunger = maxHunger;     //hunger is maximum hunger
        }

        if (hunger < 0) {            //if hunger is less than 0
            hunger = 0;              // make hunger 0
        }
    }
    
    public void UpdateRest(int i)
    {
        rest += i;                 //manipulate rest according to the value given
        
        if (rest > maxRest) {     //if rest goes over maximum rest    
            rest = maxRest;       //rest is maximum rest
        }

        if (rest < 0) {            //if rest is less than 0
            rest = 0;              // make rest 0
        } 
    }
    
    public void UpdateMoney(int i)
    {
        money += i;                 //manipulate money according to the value given
        
        if (money < 0) {            //if money is less than 0
            money = 0;              // make money 0
        }
    }
    
    // Called when exiting the game
    public void OnApplicationQuit() {
        PlayerPrefs.SetInt("hunger", hunger);   //set current hunger as preference   
        PlayerPrefs.SetInt("happy", happy);     //set current happy as preference  
        PlayerPrefs.SetInt("rest", rest);       //set current rest as preference  
        PlayerPrefs.SetInt("money", money);     //set current money as preference  
    }
}
