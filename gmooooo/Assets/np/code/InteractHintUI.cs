using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class InteractHintUI : MonoBehaviour
{
    public Text hintText;
    public InteractableObject[] allObjects;

    void Update()
    {
        if (hintText == null || allObjects == null) return; // ป้องกัน null

        bool found = false;

        foreach (var obj in allObjects)
        {
            if (obj == null) continue; // ป้องกัน element ว่าง

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