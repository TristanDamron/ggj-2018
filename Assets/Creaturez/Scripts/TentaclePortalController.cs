using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclePortalController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _crackAnim;
    [SerializeField]
    private GameObject _tentacleHolder;
    [SerializeField]
    private GameObject _headHolder;
    [SerializeField]
    private GameObject _featherParticles;
    [SerializeField]
    private int _index;
    [SerializeField]
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (Manager.creaturezHP <= 0) {
            Destroy(gameObject);
        }

        if (Manager.tentaclesFed >= 3) {
            Manager.tentaclesFed = 0;
            Invoke("EnableHeadHolder", 2f);
        } else {
            _renderer.sprite = _crackAnim[_index];
            Invoke("CrackAnimation", 0.25f);
        }
    }

    public void CrackAnimation() {
        InvokeRepeating("IncreaseFrameIndex", 0f, 0.25f);
        Invoke("EnableTentacleHolder", 0.25f * _crackAnim.Length);
    }

    public void IncreaseFrameIndex() {
        if (_index <= _crackAnim.Length)
            _index++;
        
        _renderer.sprite = _crackAnim[_index];
    }

    public void EnableTentacleHolder()
    {
        _tentacleHolder.SetActive(true);
        Tips.current = Tips.Events.PickedUpBird;
    }

    public void EnableHeadHolder()
    {
        _headHolder.SetActive(true);
        Tips.current = Tips.Events.FightingMonster;
        Invoke("InvokeDestroy", 10f);
    }

    public void Fed()
    {
        Invoke("InvokeDestroy", 2f);
        Instantiate(_featherParticles, transform.localPosition, transform.localRotation);
        Manager.tentaclesFed += 1;
    }

    void InvokeDestroy()
    {
        Tips.current = Tips.Events.Patrolling;        
        Destroy(gameObject);
    }
}

