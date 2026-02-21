using Godot;
using System;

public partial class SinePatterns : Node
{
    private float Amplitude = 100f;
    private float Frequency = 0.02f;
    private float Speed = 200f;
    private float YLimit = 300f;
    private float direction = 1f; // 1 = right, -1 = left

    public enum Patterns
    {
        RegularSine,
        DoubleFrequency,
        SuperWave,
        AbsoluteSine,
        SinePlusSine,
        Elliptical,
        DampedWave
    }

    Patterns currentPattern = Patterns.RegularSine;

    public void SetCurrentPattern(Patterns newPattern)
    {
        currentPattern = newPattern;
    }

    public void ProcessCurrentPattern(ref float yValue, ref float xOffset, double delta)
    {
        yValue += direction * Speed * (float)delta;

        // Reverse when reaching bounds
        if (yValue > YLimit)
            direction = -1f;
        else if (yValue < -YLimit)
            direction = 1f;

        xOffset = CalculateCurrentPattern(yValue);
    }

    public float CalculateCurrentPattern(float yValue)
    {
        switch(currentPattern)
        {
            case Patterns.RegularSine:
                return RegularSine(yValue);
            case Patterns.DoubleFrequency:
                return DoubleFrequency(yValue);
            case Patterns.SuperWave:
                return SuperWave(yValue);
            case Patterns.AbsoluteSine:
                return AbsoluteSine(yValue);
            case Patterns.SinePlusSine:
                return SinePlusSine(yValue);
            case Patterns.Elliptical:
                return Elliptical(yValue);
            case Patterns.DampedWave:
                return DampedWave(yValue);
            default:
                return RegularSine(yValue);
        }
    }

    private float RegularSine(float yValue)
    {
        return 100f * Mathf.Sin(yValue * 0.02f);
    }

    private float DoubleFrequency(float yValue)
    {
        return 100.0f * Mathf.Sin(20f*yValue * 0.002f);
    }

    private float SuperWave(float yValue)
    {
        return 150f * Mathf.Pow(Mathf.Sin(15f*yValue * 0.002f), 3f);
    }

    private float AbsoluteSine(float yValue)
    {
        return 200f * Mathf.Abs(Mathf.Sin(10f*yValue * 0.002f)) - 100f;
    }

    private float SinePlusSine(float yValue)
    {
        return 100f * Mathf.Sin(10f*yValue * 0.002f) + 50f * Mathf.Sin(20f*yValue * 0.002f);
    }

    private float Elliptical(float yValue)
    {
        return 150f * Mathf.Sin(10f*yValue * 0.002f) * Mathf.Cos(25f*yValue * 0.002f);
    }

    private float DampedWave(float yValue)
    {
        return 100f * Mathf.Sin(10*yValue * 0.002f) * Mathf.Exp(-0.02f * yValue * yValue * 0.002f);
    }
}
