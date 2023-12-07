using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public Animator animator;

    public GameObject victoryZone;
    public float waitToShowExit;

    public int bossMusic, bossDeath, bossDeathShout, bossHit;

    public enum BossPhase
    {
        intro,
        phase1,
        phase2,
        phase3,
        end,
    };

    public BossPhase currentPhase = BossPhase.intro;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public void OnEnable()
    {
        AudioManager.instance.PlayMusic(bossMusic);
    }

    void Update()
    {
        if (GameManager.Instance.isRespawning)
        {
            currentPhase = BossPhase.intro;

            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);
            animator.SetBool("Phase3", false);

            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

            gameObject.SetActive(false);

            BossActivator.instance.gameObject.SetActive(true);
            BossActivator.instance.entrance.SetActive(true);

            GameManager.Instance.isRespawning = false;

        }
    }

    public void DamageBoss()
    {
        AudioManager.instance.PlaySFX(bossHit);

        currentPhase++;

        if(currentPhase != BossPhase.end)
        {
            animator.SetTrigger("Hurt");
        }

        switch(currentPhase)
        {
            case BossPhase.phase1:
                animator.SetBool("Phase1", true); 
                break;
            case BossPhase.phase2:
                animator.SetBool("Phase2", true);
                animator.SetBool("Phase1", false);
                break;
            case BossPhase.phase3:
                animator.SetBool("Phase3", true);
                animator.SetBool("Phase2", false);
                break;
            case BossPhase.end:
                animator.SetTrigger("End");
                StartCoroutine(EndBoss());
                break;
        }
    }

    IEnumerator EndBoss()
    {
        AudioManager.instance.PlaySFX(bossDeath);
        AudioManager.instance.PlaySFX(bossDeathShout);
        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);

        yield return new WaitForSeconds(waitToShowExit);
        victoryZone.SetActive(true);

    }
}
