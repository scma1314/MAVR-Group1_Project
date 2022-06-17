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
        Clipboard_Header.text = "Auftrag fertig! \n\n Zeit: \n ";    }

    private void Start()
    {
        Clipboard_Text.text = "1. Schwierigkeit und Auftrag über das Hauptmenü auswählen \n\n 2.  Zum Tisch gehen \n\n 3. Auftrag ausführen";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
