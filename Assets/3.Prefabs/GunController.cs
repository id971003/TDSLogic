using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Hero hero;
    [SerializeField] private GameManager _gameManager;


    [SerializeField] private GameObject Anim;
    [SerializeField] Vector3 MousPosition;
    [SerializeField] bool b_OnDrag;

    public void Start()
    {
        b_OnDrag = false;
        _gameManager = GameManager.Instance;
        SetAim(false);
        _gameManager.gameEnd+=()=>
        {
            SetAim(false);
        };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_gameManager.B_GameStart)
            return;
        b_OnDrag = true;
        OnPoint(eventData);
        SetAim(true);
        // Handle pointer down event if needed
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_gameManager.B_GameStart)
            return;
        b_OnDrag = false;
        SetAim(false);
        // Handle pointer up event if needed
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!_gameManager.B_GameStart)
            return;
        OnPoint(eventData);
        
    }
    void SetAim(bool ta)
    {
        Anim.SetActive(ta);
    }
    void OnPoint(PointerEventData eventdata)
    {
        MousPosition = eventdata.position;
        Vector3 mousePosition = MousPosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        hero.SetRotate(worldPosition);
    }
    public void Update()
    {

        if (_gameManager.B_GameStart && !b_OnDrag)
        {
            var target = _gameManager.GetCloseMonster();
            if (target == null) return;
            hero.SetRotate(_gameManager.GetCloseMonster().transform.position);
        }
    }
}
