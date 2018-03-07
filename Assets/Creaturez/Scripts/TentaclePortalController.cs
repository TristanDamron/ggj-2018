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
    private SpriteMask _mask;
    
    private void Start()
    {        
        if (Manager.creaturezHP <= 0) {
            Destroy(gameObject);
        }

        if (Manager.tentaclesFed >= 3) {
            Manager.tentaclesFed = 0;
            Invoke("CrackAnimation", 0.1f);            
            Invoke("EnableHeadHolder", 0.1f * _crackAnim.Length);
        } else {
            _mask.sprite = _crackAnim[_index];
            Invoke("CrackAnimation", 0.1f);
            Invoke("EnableTentacleHolder", 0.1f * _crackAnim.Length);        
        }
    }

    public void CrackAnimation() {
        InvokeRepeating("IncreaseFrameIndex", 0f, 0.1f);
    }
    
    public void CloseAnimation() {
        //InvokeRepeating("DecreaseFrameIndex", 0f, 0.1f);
    }

    public void IncreaseFrameIndex() {
        if (_index <= _crackAnim.Length)
            _index++;
        
        _mask.sprite = _crackAnim[_index];
    }

    public void DecreaseFrameIndex() {
        if (_index >= 0)
            _index--;

        _mask.sprite = _crackAnim[_index];        
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
        Invoke("InvokeDestroy", 20f);
    }

    public void Fed()
    {
        Invoke("InvokeDestroy", 2f);
        //CloseAnimation();
        Instantiate(_featherParticles, transform.localPosition, transform.localRotation);
        Manager.tentaclesFed += 1;
    }

    void InvokeDestroy()
    {        
        Tips.current = Tips.Events.Patrolling;        
        Destroy(gameObject);
    }
}

