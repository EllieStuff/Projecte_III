using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxModifierScript : MonoBehaviour
{
    bool destroy;
    float timer = 2;

    [SerializeField] int currentColorID = 0;

    [SerializeField] Color currentColor, nextColor;

    public PlayersHUDManager hudManager;

    Vector3 _rotAddition = Vector3.zero;
    [SerializeField] float rotSpeed = 20.0f;

    MeshRenderer mesh;
    float lerpTime = 1, currentTime = 0.0f;
    private Light cubeLight;
    private bool alphaUpOrDown;


    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        alphaUpOrDown = true;
        hudManager = GameObject.Find("HUD").transform.GetComponentInChildren<PlayersHUDManager>();

        currentColor = UseGradientMaterials.GetColor(ref currentColorID);
        currentColorID++;
        nextColor = UseGradientMaterials.GetColor(ref currentColorID);

        currentColor.a = 0.2f;
        nextColor.a = 0.2f;

        Material mat = new Material(mesh.material);

        mat.color = currentColor;
        mesh.material = mat;
        cubeLight = GetComponent<Light>();

        _rotAddition.y = (transform.eulerAngles * rotSpeed).y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player") && !destroy)
        {
            //collision.transform.GetComponent<RandomModifierGet>().GetModifier();

            //Box Picked Particles
            other.transform.parent.GetChild(3).GetChild(6).GetComponent<ParticleSystem>().Play();

            destroy = true;
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            PlayersHUD currHud = hudManager.GetPlayerHUD(other.GetComponentInParent<PlayerData>().id);
            currHud.RollModifiers();
        }
    }

    private void Update()
    {
        Vector3 currRot = transform.rotation.eulerAngles;
        currRot.y += rotSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(currRot);

        ChangeColor();
        if (cubeLight.range >= 1 || cubeLight.range <= 0.7)
            alphaUpOrDown = !alphaUpOrDown;

        if (cubeLight.range > 0.7 && !alphaUpOrDown)
            cubeLight.range -= Time.deltaTime * 0.2f;
        else if(cubeLight.range < 1 && alphaUpOrDown)
            cubeLight.range += Time.deltaTime * 0.2f;

        if (destroy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 || transform.localScale.magnitude <= 0.2f)
                Destroy(gameObject);
            else
                transform.localScale -= new Vector3(5.0f, 5.0f, 5.0f) * Time.deltaTime;
        }
    }

    void ChangeColor()
    {
        currentColor = Color.Lerp(currentColor, nextColor, 0.01f);
        currentTime += Time.deltaTime;
        currentColor.a = 0.2f;

        Material mat = new Material(mesh.material);

        mat.color = currentColor;
        mesh.material = mat;

        if (CompareColors(currentColor, nextColor)) SetNextColor();
    }

    void SetNextColor()
    {
        currentColor = nextColor;

        currentColorID++;
        nextColor = UseGradientMaterials.GetColor(ref currentColorID);
        nextColor.a = 0.2f;
    }

    bool CompareColors(Color _a, Color _b)
    {
        return (_a.r - _b.r < 0.01f && _a.g - _b.g < 0.01f && _a.b - _b.b < 0.01f);
    }

}