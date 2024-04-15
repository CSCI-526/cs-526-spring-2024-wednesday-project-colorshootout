using System.Collections;
using System.Collections.Generic;
using Unity.FPS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(Destructable), typeof(Actor), typeof(Health))]

public class CubeInteract : MonoBehaviour
{
    Health m_Health;
    public UnityAction onDamaged;
    public Color m_Color;
    int m_Damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        renderer.material.color = m_Color;
        m_Health = GetComponent<Health>();
        DebugUtility.HandleErrorIfNullGetComponent<Health, CubeInteract>(m_Health, this, gameObject);
        m_Health.OnPointDamaged += OnPointDamaged;
        m_Health.OnDamaged += OnDamaged;
    }

    void OnPointDamaged(float damage, Vector3 point, GameObject damageSource)
    {
        Form form = FindObjectOfType<Form>();
        if (form != null)
            form._ammoReceive[form.cubeMap[gameObject.name]] += 1;
        PlayerCharacterController m_Controller = damageSource.GetComponent<PlayerCharacterController>();
        if (m_Controller != null)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Vector3 localPoint = transform.InverseTransformPoint(point);
            Vector3 eps = new Vector3(0.1f / transform.localScale.x, 0.1f / transform.localScale.y, 0.1f / transform.localScale.z);
            float colorDifference = Vector3.Distance(new Vector3(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b),
                                         new Vector3(m_Controller.color.r, m_Controller.color.g, m_Controller.color.b));
            Vector3 scaleChange = new Vector3(0, 0, 0);
            float changeValue = 0.2f;
            if (localPoint.x <= -0.5f + eps[0]) scaleChange.x = changeValue;
            else if (localPoint.x >= 0.5f - eps[0]) scaleChange.x = changeValue;
            else if (localPoint.y <= -0.5f + eps[1]) scaleChange.y = changeValue;
            else if (localPoint.y >= 0.5f - eps[1]) scaleChange.y = changeValue;
            else if (localPoint.z <= -0.5f + eps[2]) scaleChange.z = changeValue;
            else if (localPoint.z >= 0.5f - eps[2]) scaleChange.z = changeValue;
            if (colorDifference < 1f) // You can adjust this threshold as needed
            {
                // Increase scale if color difference is small
                transform.localScale += scaleChange * 2f;
            }
            else
            {
                if (transform.localScale.x > 0.1f && transform.localScale.y > 0.1f && transform.localScale.z > 0.1f)
                {
                    float minScale = 0.1f; // 最小可能的scale
                    float maxScale = 10.0f; // 最大可能的scale
                    float scaleFactor = (transform.localScale.magnitude - minScale) / (maxScale - minScale);
                    transform.localScale = new Vector3(
                                            Mathf.Max(0.1f, transform.localScale.x - scaleFactor * scaleChange.x),
                                            Mathf.Max(0.1f, transform.localScale.y - scaleFactor * scaleChange.y),
                                            Mathf.Max(0.1f, transform.localScale.z - scaleFactor * scaleChange.z));
                }
                else
                {
                    m_Damage -= 1;
                    if (m_Damage <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    void OnDamaged(float damage, GameObject damageSource)
    {
        PlayerCharacterController m_Controller = damageSource.GetComponent<PlayerCharacterController>();
        if (m_Controller != null)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            // compute direction of the damage, and change scale of the cube
            Vector3 direction = transform.position - damageSource.transform.position;
            direction.Normalize();
            Vector3 scaleChange = new Vector3(direction.x > 0 ? 1 : 0, direction.y > 0 ? 1 : 0, direction.z > 0 ? 1 : 0);
            transform.localScale += scaleChange;

            //if (m_Controller.IsGrounded)
            //{
            //    renderer.material.color = Color.Lerp(renderer.material.color, m_Controller.color, 0.2f);
            //}
            //else
            //{
            //    Color.RGBToHSV(renderer.material.color, out float h1, out float s1, out float v1);
            //    Color.RGBToHSV(m_Controller.color, out float h2, out float s2, out float v2);
            //    float hueDifference = Mathf.Abs(h1 - h2);
            //    float valueDifference = Mathf.Abs(v1 - v2);

            //    if (hueDifference < 0.1f && valueDifference < 0.20f)
            //    {
            //        RepelObject(damageSource);
            //    }
            //    else
            //    {
            //        AttractObject(damageSource);
            //    }
            //}
        }
    }


    void AttractObject(GameObject obj)
    {
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform.position, 100 * Time.deltaTime);
    }

    void RepelObject(GameObject obj)
    {
        Vector3 targetPosition = obj.transform.position + (obj.transform.position - transform.position).normalized * 100;

        Vector3 nextPosition = Vector3.MoveTowards(obj.transform.position, targetPosition, 100 * Time.deltaTime);

        obj.transform.position = nextPosition;
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -500)
        {
            Destroy(gameObject);
        }
    }

}

