using UnityEngine;

public class KeyTrigger : MonoBehaviour, ISaveable
{
    private float rotatingSpeed=3f;

    private bool _isExist;
    private bool isExist { 
        get { return _isExist; }
        set { 
            gameObject.SetActive(value);
            _isExist = value;
        }
    }

    private string isExistStr {
        get { 
            return isExist ? "1":"0";
        }
        set {
            if (value.Length < 1) {
                isExist = false;
                return;
            }

            if (value[0] == '1') {
                isExist = true;
                return;
            }
            isExist = false;

        }
    }

    public void Awake()
    {
        isExist = true;
    }

    public void Update()
    {
        Rotating();
    }

    public void Rotating() {
        transform.Rotate(
            0,
            0,
            rotatingSpeed * Time.deltaTime,
            Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            GameplayManager.Instance.AddKey();
            SoundUI.Instance.TryPlayPickUp();
            isExist = false;
//            Destroy(gameObject);
        }
    }

    public string OnSave() {

        string str = isExistStr 
            + "%" + (gameObject.active ? "1":"0");
        return str;
    }

    public void OnLoad(string data) {
        string[] datas = data.Split('%');
        if (datas.Length < 2) {
            Debug.LogError("there is problerm with load Key");
        }

        isExistStr = datas[0];
        gameObject.SetActive(datas[1][0] =='1');
    }
}
