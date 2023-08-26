using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOutScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private RawImage image;

    [SerializeField] private Submarine sub;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        image.enabled = true;
        animator.SetTrigger("FadeIn");
    }

    public void FadeInFinished()
    {
        image.enabled = false;
    }

    public void FadeOut()
    {
        image.enabled = true;
        animator.SetTrigger("FadeOut");
    }

    public void FadeOutFinished()
    {
        sub.isDead = true;
        SceneManager.LoadScene(0);
    }

    public void IsDrowning(bool isDrowning)
    {
        image.enabled = true;
        animator.SetBool("Drowning", isDrowning);
    }

    public void Drowned()
    {
        SoundManager.PlaySound(SoundManager.Sound.Drown);
        sub.isDead = true;
        Invoke(nameof(FadeOutFinished), 1);
    }

}
