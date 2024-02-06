using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Uianimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;
    public GameObject stage1;
    public GameObject stage2;




    public void Func_PlayUIAnim()
    {
        StartCoroutine(Func_PlayAnimUI());
    }


    IEnumerator Func_PlayAnimUI()
    {
        foreach (Sprite sprite in m_SpriteArray)
        {
            yield return new WaitForSeconds(m_Speed);
            m_Image.sprite = sprite;
            
        }
        m_Image.enabled = false;
        stage1.SetActive(false);
        stage2.SetActive(true);
    }
}
