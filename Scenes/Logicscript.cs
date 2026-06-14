using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logicscript : MonoBehaviour
{
    [SerializeField] GameObject Restartscreen;
    [SerializeField] Text score_UI;
    public int currentscore = 0;
    int numberH = 3;
    [SerializeField] Image heart1;
    [SerializeField] Image heart2;
    [SerializeField] Image heart3;

    public void Restart()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void restartscreen()
    {
        Restartscreen.SetActive(true);
    }
    public void Addscore(int addscore)
    {
        currentscore += addscore;
        score_UI.text = "Score: " + (currentscore);
    }
    public int hplogic()
    {
        if (numberH == 3)
        {

            heart1.enabled = false;
            numberH--;
            return numberH;
        }
        else if (numberH == 2)
        {

            heart2.enabled = false;
            numberH--;
            return numberH;
        }
        else if (numberH == 1)
        {

            heart3.enabled = false;
            numberH--;
            Debug.Log("Player Died");
            return numberH;
        }
        else
        {
            return numberH;
        }
    }

   
}
