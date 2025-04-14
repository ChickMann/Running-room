using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
public class Loader : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float Timetrans;
    [SerializeField] private GameObject image;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(GameManager_Instance.CoolDown(() => { image.gameObject.SetActive(false); }, 1.3f));
    }
    IEnumerator AnimationLoader(int Index)
    {
        anim.SetTrigger("Transition");
        yield return new WaitForSeconds(Timetrans);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Index);

    }
    public void Loading(int Index)
    {
        image.gameObject.SetActive(true);
        StartCoroutine(AnimationLoader(Index));

    }
}
