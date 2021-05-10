using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    // Change Scene
    public void Manager(int i) {
        switch (i) {
            case 0:
                SceneManager.LoadScene("GameScene");        //Load the Game Scene
                break;
        }
    }
}
