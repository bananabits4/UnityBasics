using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iamahuman : MonoBehaviour
{
    public GameObject Fail;
    public GameObject Pass;
    public GameObject stage2;
    public GameObject icon;

    [SerializeField] int length;
    [SerializeField] string correct;
    [SerializeField] string wrong;
    public void validate_info(string output)
    {
        if (output.Length >= 2)
        {
            if (output.ToLower() == wrong)
            {
                Fail.SetActive(true);
                stage2.SetActive(false);

            }
            else if (output.ToLower() == correct)
            {
                Pass.SetActive(true);
                stage2.SetActive(false);
                icon.SetActive(false);
            }
        }
    }
}
