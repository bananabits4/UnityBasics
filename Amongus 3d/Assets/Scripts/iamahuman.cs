using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iamahuman : MonoBehaviour
{
    public GameObject Fail;
    public GameObject Pass;
    public GameObject stage2;
    public void validate_info(string output)
    {
        if (output.Length >= 2)
        {
            if (output.ToLower() == "no")
            {
                Fail.SetActive(true);
                stage2.SetActive(false);
            }
            else if (output.ToLower() == "yes")
            {
                Pass.SetActive(true);
                stage2.SetActive(false);
            }
        }
    }
}
