using UnityEngine;
using System.Collections;

public class BinaryRotator : MonoBehaviour , IInteractable {

	public float _openAngle = 90.0f;
	public float _closedAngle = 0;
	public float _angle= 0f;
	public float _duration = 0.5f;
	public float _soundVolume=1;
	public float _audioDelay=0;

	public bool _enabled;
	public bool _animating = false;

	public Transform _rotatingTransform;

	public AudioSource _source;

	public AudioClip _enableSound;
	public AudioClip _disableSound;

	public enum _rotationAxis{
		x,
		y,
		z
	};

	public _rotationAxis _rotAxis;

	private Vector3 _targetRot;

	private float _tAngle =0f;
	private float _velocity = 0f;

	private bool _enter =false;

	private Collider _col = null;

	private LayerMask _interactiveLayer = 1<<11;

	// Use this for initialization
	void Start () {
		_source = GetComponent<AudioSource>();
		_angle = _closedAngle;
		if (_enabled)
			_tAngle = _openAngle;
		else
			_tAngle = _closedAngle;

		
		_col = GetComponent<Collider> ();
		if (_col == null)
			Debug.LogError ("object has no collider");

	}

	void PlaySound(){
		if (_enabled) {
			_source.PlayOneShot (_enableSound, _soundVolume);
		} else {
			_source.PlayOneShot(_disableSound, _soundVolume);
		}
	}

	public void Interact()
	{

		if (_enter)
		{
			_animating = true;
			if (_enabled) {
				_tAngle = _closedAngle;
			} else {
				_tAngle = _openAngle;

			}
			_enabled = !_enabled;

			Invoke ("PlaySound", _audioDelay);
		}else
		{
			return;
		}
	}
		
	void getTargetRotation(){
		switch (_rotAxis) {
		case _rotationAxis.x:
			_targetRot = Vector3.right;
			break;
		case _rotationAxis.y:
			_targetRot = Vector3.up;
			break;
		case _rotationAxis.z:
			_targetRot = Vector3.forward;
			break;

		}

	}

	// Update is called once per frame
	protected void Update () {
		getTargetRotation ();
		if (_animating)
			OpenAnimation ();
		Quaternion r = Quaternion.Euler (_targetRot * _angle);
		_rotatingTransform.transform.localRotation = r;
	}

	private void OpenAnimation (){
		_angle = Mathf.SmoothDampAngle (_angle, _tAngle, ref _velocity, _duration);
		if (Mathf.Abs (_tAngle - _angle) < 0.1f) {
			_angle = _tAngle;
			_animating = false;
		}

	}

	//Activate the Main function when player is near the object
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			_enter = true;
		}
	}

	//Deactivate the Main function when player is away from the object
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			_enter = false;
		}
	}
}