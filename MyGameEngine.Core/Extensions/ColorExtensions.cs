namespace MyGameEngine.Core.Extensions;

public static class ColorExtensions
{
    public static Color AlterBrightness(this Color color, float percent)
    {
        var hsl = color.AsHSL();
        return ColorFromHSL(hsl.Item1, hsl.Item2, Math.Max(0, Math.Min(1, hsl.Item3 + percent)));
    }

    private static Color ColorFromHSL(float h, float s, float l)
    {
        float r = 0, g = 0, b = 0;
        if (l != 0)
        {
            if (s == 0)
                r = g = b = l;
            else
            {
                float temp2;
                if (l < 0.5)
                    temp2 = l * (1.0f + s);
                else
                    temp2 = l + s - (l * s);

                float temp1 = 2.0f * l - temp2;

                r = GetColorComponent(temp1, temp2, h + 1.0f / 3.0f);
                g = GetColorComponent(temp1, temp2, h);
                b = GetColorComponent(temp1, temp2, h - 1.0f / 3.0f);
            }
        }
        return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

    }

    private static float GetColorComponent(float temp1, float temp2, float temp3)
    {
        if (temp3 < 0.0)
            temp3 += 1.0f;
        else if (temp3 > 1.0)
            temp3 -= 1.0f;

        if (temp3 < 1.0 / 6.0)
            return temp1 + (temp2 - temp1) * 6.0f * temp3;
        else if (temp3 < 0.5)
            return temp2;
        else if (temp3 < 2.0 / 3.0)
            return temp1 + ((temp2 - temp1) * ((2.0f / 3.0f) - temp3) * 6.0f);
        else
            return temp1;
    }

    public static (float, float, float) AsHSL(this Color color)
    {
        float _R = (color.R / 255f);
        float _G = (color.G / 255f);
        float _B = (color.B / 255f);

        float _Min = Math.Min(Math.Min(_R, _G), _B);
        float _Max = Math.Max(Math.Max(_R, _G), _B);
        float _Delta = _Max - _Min;

        float H = 0;
        float S = 0;
        float L = (float)((_Max + _Min) / 2.0f);

        if (_Delta != 0)
        {
            if (L < 0.5f)
            {
                S = (float)(_Delta / (_Max + _Min));
            }
            else
            {
                S = (float)(_Delta / (2.0f - _Max - _Min));
            }

            float _Delta_R = (float)(((_Max - _R) / 6.0f + (_Delta / 2.0f)) / _Delta);
            float _Delta_G = (float)(((_Max - _G) / 6.0f + (_Delta / 2.0f)) / _Delta);
            float _Delta_B = (float)(((_Max - _B) / 6.0f + (_Delta / 2.0f)) / _Delta);

            if (_R == _Max)
            {
                H = _Delta_B - _Delta_G;
            }
            else if (_G == _Max)
            {
                H = (1.0f / 3.0f) + _Delta_R - _Delta_B;
            }
            else if (_B == _Max)
            {
                H = (2.0f / 3.0f) + _Delta_G - _Delta_R;
            }

            if (H < 0) H += 1.0f;
            if (H > 1) H -= 1.0f;
        }

        return (H, S, L);
    }
}
