using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class Clipboard : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject clipboard;
    public TMP_Text Clipboard_Text;
    public TMP_Text Timer_Text;
    public TMP_Text Clipboard_Header;




    public void Deactivate_Clipboard()
    {
        clipboard.SetActive(false);
    }

    public void Highscore()
    {
        clipboard.SetActive(true);
        Clipboard_Text.text = (" \n\n\n " + Timer_Text.text);
        Clipboard_Header.text = "Order finished! \n\n Time: \n ";    }

    private void Start()
    {
        Clipboard_Text.text = "1. Select Order and difficulty via the main menu \n\n 2.  Go to the table \n\n 3. Execute order";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
