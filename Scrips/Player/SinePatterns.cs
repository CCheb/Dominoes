using Godot;
using System;

public partial class SinePatterns : Node
{
    private float RandAmplitude = 100f;
    private float RandFrequency = 0.02f;
    private float Speed = 200f;
    private float SpeedRandAdjustment = 0.0f;
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

    public void RandomizeValues()
    {
        RandAmplitude = (float)GD.RandRange(1f, 60f);
        RandFrequency = (float)GD.RandRange(0.0f, 0.002f);
        SpeedRandAdjustment = (float)GD.RandRange(0f, 400f);
    }

    public void ProcessCurrentPattern(ref float yValue, ref float xOffset, double delta)
    {
        yValue += direction * (Speed+SpeedRandAdjustment) * (float)delta;

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
        return (100f+RandAmplitude) * Mathf.Sin(yValue * (0.02f+RandFrequency));
    }

    private float DoubleFrequency(float yValue)
    {
        return (100.0f+RandAmplitude) * Mathf.Sin(20f*yValue * (0.002f+RandFrequency));
    }

    private float SuperWave(float yValue)
    {
        return (150f+RandAmplitude) * Mathf.Pow(Mathf.Sin(15f*yValue * (0.002f+RandFrequency)), 3f);
    }

    private float AbsoluteSine(float yValue)
    {
        return (200f+RandAmplitude) * Mathf.Abs(Mathf.Sin(10f*yValue * (0.002f+RandFrequency))) - 100f;
    }

    private float SinePlusSine(float yValue)
    {
        return (100f+RandAmplitude) * Mathf.Sin(10f*yValue * (0.002f+RandFrequency)) + (50f+RandAmplitude) * Mathf.Sin(20f*yValue * (0.002f+RandFrequency));
    }

    private float Elliptical(float yValue)
    {
        return (150f+RandAmplitude) * Mathf.Sin(10f*yValue * (0.002f+RandFrequency)) * Mathf.Cos(25f*yValue * (0.002f+RandFrequency));
    }

    private float DampedWave(float yValue)
    {
        return (100f+RandAmplitude) * Mathf.Sin(10*yValue * (0.002f+RandFrequency)) * Mathf.Exp(-0.02f * yValue * yValue * (0.002f+RandFrequency));
    }
}
