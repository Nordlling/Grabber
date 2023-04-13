using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private int linkCount;
    [SerializeField] private AudioSource slowMotionEffect;
    [SerializeField] private float timeShiftDuration = 0.12f;
    [SerializeField] private float timeToWin = 3f;
    [SerializeField] private float slowedTime = 0.01f;
    
    private const float _effectDurationBeforeTimeShift = 0.6f;
    private List<HingeJoint2D> _joints = new List<HingeJoint2D>();
    private LevelResultDisplay _levelResultDisplay;

    private void Start()
    {
        _levelResultDisplay = FindObjectOfType<LevelResultDisplay>();
    }

    public void TimeShift()
    {
        StartCoroutine(TimeShifter());
    }

    private IEnumerator TimeShifter()
    {
        slowMotionEffect.Play();
        yield return new WaitForSeconds(_effectDurationBeforeTimeShift);
        Time.timeScale = slowedTime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(timeShiftDuration * Time.timeScale);
        slowMotionEffect.pitch = -1;
        slowMotionEffect.time = slowMotionEffect.clip.length - 0.01f;
        slowMotionEffect.Play();
        yield return new WaitForSeconds(slowMotionEffect.clip.length * Time.timeScale - 0.002f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(timeToWin);
        LevelStatus.LevelCompleted();
        _levelResultDisplay.Success();
    }

    public void OnSelected(HingeJoint2D joint)
    {
        if (!joint.enabled && !_joints.Contains(joint))
        {
            _joints.Add(joint);
            if (_joints.Count == 2)
            {
                CreateRope(_joints[0], _joints[1]);
                _joints.Clear();
            }
        }
    }

    private void CreateRope(HingeJoint2D firstBody, HingeJoint2D secondBody)
    {
        firstBody.enabled = true;
        secondBody.enabled = true;
        List<GameObject> rope = new List<GameObject>();
        Vector3 firstObjectPosition = firstBody.connectedAnchor;
        Vector3 secondObjectPosition = secondBody.connectedAnchor;
        Vector3 vectorDistance = secondObjectPosition - firstObjectPosition;
        Vector3 vectorDistanceNormalized = vectorDistance.normalized;
        float distance = vectorDistance.magnitude;
        float linkWidth = distance / linkCount;
        Vector3 linkScale = vectorDistanceNormalized * linkWidth;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, vectorDistanceNormalized);
        Vector3 firstPosition = firstObjectPosition + (linkScale * 0.5f);
        Vector3 prefabScale = ropePrefab.transform.localScale;
        ropePrefab.transform.localScale = new Vector3(linkWidth + 0.1f, prefabScale.y, prefabScale.z);
        
        for (int i = 0; i < linkCount; i++)
        {
            Vector3 instantiatePosition = firstPosition + linkScale * i;
            var link = Instantiate(ropePrefab, instantiatePosition, rotation);
            rope.Add(link);
            if (i > 0)
            {
                link.GetComponent<HingeJoint2D>().connectedBody = rope[i - 1].GetComponent<Rigidbody2D>();
            }
        }

        rope.First().GetComponent<HingeJoint2D>().enabled = false;
        firstBody.connectedBody = rope.First().GetComponent<Rigidbody2D>();
        secondBody.connectedBody = rope.Last().GetComponent<Rigidbody2D>();
    }
}
