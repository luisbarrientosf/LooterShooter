using TMPro;
using UnityEngine;

public class NameTagController : MonoBehaviour
{
    public TextMeshProUGUI debugText; 
    private string lastMoveDir;


    void Update()
    {
        
        bool up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        // Detect last direction PRESSED (used only for animation)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) lastMoveDir = "up";
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) lastMoveDir = "down";
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) lastMoveDir = "left";
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) lastMoveDir = "right";
        
        string directions = "";
        if (up) { directions += " up "; }
        if (down) { directions += " down "; }
        if (left) { directions += " left "; }
        if (right) { directions += " right "; }
        debugText.text = "Last Move: " + lastMoveDir + "\n Keypress: " + directions;
    }
}
