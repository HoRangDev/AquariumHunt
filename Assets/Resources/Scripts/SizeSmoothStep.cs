using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScaleAxis
{
    AxisX,
    AxisY
}

public class SizeSmoothStep : MonoBehaviour
{
    public Transform Target;
    public float From = 0.0f;
    public float To = 0.0f;
    public float Duration = 0.0f;

    public bool bIsLoop = false;
    public bool bIsReverse = false;
    public bool bIsLoopWithReverse = false;

    public ScaleAxis Axis;

    private float DeltaTime = 0.0f;

    private void Awake()
    {
        if ( bIsLoopWithReverse )
        {
            bIsLoop = true;
        }
    }

    private void Update()
    {
        if ( Target != null )
        {
            Vector2 Origin = Target.localScale;
            if(bIsReverse)
            {
                DeltaTime -= Time.deltaTime;
            }
            else
            {
                DeltaTime += Time.deltaTime;
            }

            if ( DeltaTime <= 0.0f )
            {
                if (bIsLoopWithReverse)
                {
                    if (bIsReverse)
                    {
                        bIsReverse = false;
                    }
                }

                else if ( bIsReverse )
                {
                    DeltaTime = Duration;
                }
            }
            else if ( DeltaTime >= Duration )
            {
                if ( bIsLoopWithReverse )
                {
                    bIsReverse = true;
                }
                else
                {
                    DeltaTime = 0.0f;
                }
            }

            float Ratio = DeltaTime / Duration;
            float Value = Mathf.SmoothStep(From, To, Ratio);
            switch( Axis )
            {
                case ScaleAxis.AxisX:
                    Target.localScale = new Vector2(Value, Origin.y);
                    break;

                case ScaleAxis.AxisY:
                    Target.localScale = new Vector2(Origin.x, Value);
                    break;
            }
        }
    }
}
