using TMPro;
using UnityEngine;

public class KeyScoring : MonoBehaviour
{
    public static KeyScoring Instance;
    public TextMeshProUGUI keyText;

    private int keyCount = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UpdateText();
        Debug.Log("KeyScoring Awake");
    }

    void UpdateText()
    {
        keyText.text = "Keys: " + keyCount;
    }

    public void AddKey()
    {
        //keyCount = 0;
        keyCount++;
        UpdateText();
        Debug.Log("Key collected! Total: " + keyCount);
    }
}
