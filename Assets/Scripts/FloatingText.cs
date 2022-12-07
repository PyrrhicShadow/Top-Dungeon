using UnityEngine;
using UnityEngine.UI; 

public class FloatingText
{
    [SerializeField] bool _active;
    public bool active { get { return _active; } set { _active = value; } }
    [SerializeField] GameObject _go; 
    public GameObject go { get { return _go; } set { _go = value; } }
    [SerializeField] Text _txt;
    public Text txt { get { return _txt; } set { _txt = value; } }
    [SerializeField] Vector3 _motion;
    public Vector3 motion { get { return _motion; } set { _motion = value; } }
    [SerializeField] float _duration;
    public float duration { get { return _duration; } set { _duration = value; } }
    [SerializeField] float _lastShown; 
    public float lastShown { get { return _lastShown; } set { _lastShown = value; } }

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active); 
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active); 
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return; 
        }

        if (Time.time - lastShown > duration)
        {
            Hide(); 
        }

        go.transform.position += motion * Time.deltaTime; 
    }
}
