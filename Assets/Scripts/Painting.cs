using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private Coroutine rippleRoutine;
    [SerializeField] private float rippleTime = 1.5f;
    [SerializeField] private float maxRippleStrength = 0.75f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                var mat = hit.transform.GetComponent<Renderer>().material;
                mat.SetVector("_RippleCenter", hit.point);

                if(rippleRoutine != null)
                {
                    StopCoroutine(rippleRoutine);
                }

                rippleRoutine = StartCoroutine(DoRipple(mat));
            }
        }
    }

    private IEnumerator DoRipple(Material mat)
    {
        for (float t = 0.0f; t < rippleTime; t += Time.deltaTime)
        {
            mat.SetFloat("_RippleStrength", maxRippleStrength * (1.0f - t / rippleTime));
            yield return null;
        }
    }
}

