using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityFramework : MonoBehaviour
{
    private int length = 7;

    // keeps track of cooldowns and inputs for each ability
    private Ability[] abilityList;
    private float[] offcd;
    private bool[] input;
    public Transform[] icons;

    void Start()
    {
        abilityList = new Ability[length];
        offcd = new float[length];
        input = new bool[length];

        for (int i = 0; i < length; i++)
        {
            abilityList[i] = null;
            offcd[i] = 0f;
            input[i] = false;
        }

        AssignAbility(gameObject.GetComponent<Blink>(), 0);
        AssignAbility(gameObject.GetComponent<Pull>(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        // ability is activated;
        input[0] = Input.GetButtonDown("Ability0");
        input[1] = Input.GetButtonDown("Ability1");
        input[2] = Input.GetButtonDown("Ability2");
        input[3] = Input.GetButtonDown("Ability3");
        input[4] = Input.GetButtonDown("Ability4");
        input[5] = Input.GetButtonDown("Ability5");
        input[6] = Input.GetButtonDown("Ability6");

        for (int i = 0; i < length; i++)
        {
            if (abilityList[i] != null)
            {
                InputCheck(i);
            }
        }
    }

    // Performs ability associated with input when valid
    void InputCheck(int i)
    {
        if (abilityList[i].charges > 0)
        {
            if (input[i])
            {
                abilityList[i].UseAbility();

                if (abilityList[i].charges == abilityList[i].maxCharges)
                {
                    offcd[i] = Time.time + abilityList[i].cd;
                }

                abilityList[i].charges -= 1;
                
                icons[i].GetChild(4).GetChild(1).GetComponent<Text>().text = abilityList[i].charges.ToString();
            }
        }

        if (Time.time > offcd[i] && abilityList[i].charges < abilityList[i].maxCharges)
        {
            abilityList[i].charges += 1;

            icons[i].GetChild(4).GetChild(1).GetComponent<Text>().text = abilityList[i].charges.ToString();

            offcd[i] = Time.time + abilityList[i].cd;
        }

        if (abilityList[i].charges < abilityList[i].maxCharges)
        {
            icons[i].GetChild(1).GetComponent<Image>().fillAmount = (offcd[i] - Time.time) / abilityList[i].cd;
            if(abilityList[i].charges < 1)
                icons[i].GetChild(3).GetComponent<Image>().fillAmount = (offcd[i] - Time.time) / abilityList[i].cd;
        }
    }

    void UpdateAbilities()
    {

    }

    public void AssignAbility(Ability a, int i)
    {
        abilityList[i] = a;
    }
}
