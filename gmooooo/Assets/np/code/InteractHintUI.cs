using UnityEngine;
using UnityEngine.UI;

public class InteractHintUI : MonoBehaviour
{
    public Text hintText;           // ลาก Text UI มาใส่
    public InteractableObject[] allObjects;

    void Update()
    {
        bool found = false;

        foreach (var obj in allObjects)
        {
            if (obj.IsPlayerInRange())
            {
                hintText.text = obj.GetHint();
                hintText.gameObject.SetActive(true);
                found = true;
                break;
            }
        }

        if (!found)
        {
            hintText.gameObject.SetActive(false);
        }
    }
}