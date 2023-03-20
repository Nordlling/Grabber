using UnityEngine;
public class SelectablePoint : MonoBehaviour
{
    private HingeJoint2D[] _joints;
    private RopeSpawner ropeSpawner;
    private SpriteRenderer circleSprite;
    private bool _free = true;
    
    private void Awake()
    {
        _joints = GetComponentsInParent<HingeJoint2D>();
    }

    private void Start()
    {
        ropeSpawner = FindObjectOfType<RopeSpawner>();
        circleSprite = GetComponent<SpriteRenderer>();
        circleSprite.color = Color.blue;
    }

    private void Update()
    {
        circleSprite.enabled = !Time.timeScale.Equals(1f);
    }

    public void OnEnter()
    {
        Debug.Log("IN AREA!");
    }

    public void OnClick()
    {
        if (_free)
        {
            Vector3 localPosition = circleSprite.transform.localPosition;
            for (int i = 0; i < _joints.Length; i++)
            {
                if (!_joints[i].enabled)
                {
                    _joints[i].anchor = new Vector2(localPosition.x, localPosition.y);
                    ropeSpawner.OnSelected(_joints[i]);
                    circleSprite.color = Color.green;
                    _free = false; 
                    break;
                }
            }
            
            
        }
        else
        {
            circleSprite.color = Color.blue;
            _free = true;
        }
    }

    public void OnExit()
    {
        Debug.Log("OUT AREA!");
    }
}
