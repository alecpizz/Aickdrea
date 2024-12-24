﻿using System.Numerics;
using System.Reflection;
using ImGuiNET;
using Raylib_cs.BleedingEdge;

namespace Engine;

public static class ImGUIUtils
{
    public static void SetupSteamTheme()
    {
        var style = ImGui.GetStyle();
        style.Alpha = 1.0f;
        style.DisabledAlpha = 0.6000000238418579f;
        style.WindowPadding = new Vector2(8.0f, 8.0f);
        style.WindowRounding = 0.0f;
        style.WindowBorderSize = 1.0f;
        style.WindowMinSize = new Vector2(32.0f, 32.0f);
        style.WindowTitleAlign = new Vector2(0.0f, 0.5f);
        style.WindowMenuButtonPosition = ImGuiDir.Left;
        style.ChildRounding = 0.0f;
        style.ChildBorderSize = 1.0f;
        style.PopupRounding = 0.0f;
        style.PopupBorderSize = 1.0f;
        style.FramePadding = new Vector2(4.0f, 3.0f);
        style.FrameRounding = 0.0f;
        style.FrameBorderSize = 1.0f;
        style.ItemSpacing = new Vector2(8.0f, 4.0f);
        style.ItemInnerSpacing = new Vector2(4.0f, 4.0f);
        style.CellPadding = new Vector2(4.0f, 2.0f);
        style.IndentSpacing = 21.0f;
        style.ColumnsMinSpacing = 6.0f;
        style.ScrollbarSize = 14.0f;
        style.ScrollbarRounding = 0.0f;
        style.GrabMinSize = 10.0f;
        style.GrabRounding = 0.0f;
        style.TabRounding = 0.0f;
        style.TabBorderSize = 0.0f;
        style.TabMinWidthForCloseButton = 0.0f;
        style.ColorButtonPosition = ImGuiDir.Right;
        style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
        style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

        style.Colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        style.Colors[(int)ImGuiCol.TextDisabled] =
            new Vector4(0.4980392158031464f, 0.4980392158031464f, 0.4980392158031464f, 1.0f);
        style.Colors[(int)ImGuiCol.WindowBg] =
            new Vector4(0.2862745225429535f, 0.3372549116611481f, 0.2588235437870026f, 1.0f);
        style.Colors[(int)ImGuiCol.ChildBg] =
            new Vector4(0.2862745225429535f, 0.3372549116611481f, 0.2588235437870026f, 1.0f);
        style.Colors[(int)ImGuiCol.PopupBg] =
            new Vector4(0.239215686917305f, 0.2666666805744171f, 0.2000000029802322f, 1.0f);
        style.Colors[(int)ImGuiCol.Border] =
            new Vector4(0.5372549295425415f, 0.5686274766921997f, 0.5098039507865906f, 0.5f);
        style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.1372549086809158f, 0.1568627506494522f,
            0.1098039224743843f, 0.5199999809265137f);
        style.Colors[(int)ImGuiCol.FrameBg] =
            new Vector4(0.239215686917305f, 0.2666666805744171f, 0.2000000029802322f, 1.0f);
        style.Colors[(int)ImGuiCol.FrameBgHovered] =
            new Vector4(0.2666666805744171f, 0.2980392277240753f, 0.2274509817361832f, 1.0f);
        style.Colors[(int)ImGuiCol.FrameBgActive] =
            new Vector4(0.2980392277240753f, 0.3372549116611481f, 0.2588235437870026f, 1.0f);
        style.Colors[(int)ImGuiCol.TitleBg] =
            new Vector4(0.239215686917305f, 0.2666666805744171f, 0.2000000029802322f, 1.0f);
        style.Colors[(int)ImGuiCol.TitleBgActive] =
            new Vector4(0.2862745225429535f, 0.3372549116611481f, 0.2588235437870026f, 1.0f);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.0f, 0.0f, 0.0f, 0.5099999904632568f);
        style.Colors[(int)ImGuiCol.MenuBarBg] =
            new Vector4(0.239215686917305f, 0.2666666805744171f, 0.2000000029802322f, 1.0f);
        style.Colors[(int)ImGuiCol.ScrollbarBg] =
            new Vector4(0.3490196168422699f, 0.4196078479290009f, 0.3098039329051971f, 1.0f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab] =
            new Vector4(0.2784313857555389f, 0.3176470696926117f, 0.239215686917305f, 1.0f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] =
            new Vector4(0.2470588237047195f, 0.2980392277240753f, 0.2196078449487686f, 1.0f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] =
            new Vector4(0.2274509817361832f, 0.2666666805744171f, 0.2078431397676468f, 1.0f);
        style.Colors[(int)ImGuiCol.CheckMark] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.SliderGrab] =
            new Vector4(0.3490196168422699f, 0.4196078479290009f, 0.3098039329051971f, 1.0f);
        style.Colors[(int)ImGuiCol.SliderGrabActive] =
            new Vector4(0.5372549295425415f, 0.5686274766921997f, 0.5098039507865906f, 0.5f);
        style.Colors[(int)ImGuiCol.Button] = new Vector4(0.2862745225429535f, 0.3372549116611481f,
            0.2588235437870026f, 0.4000000059604645f);
        style.Colors[(int)ImGuiCol.ButtonHovered] =
            new Vector4(0.3490196168422699f, 0.4196078479290009f, 0.3098039329051971f, 1.0f);
        style.Colors[(int)ImGuiCol.ButtonActive] =
            new Vector4(0.5372549295425415f, 0.5686274766921997f, 0.5098039507865906f, 0.5f);
        style.Colors[(int)ImGuiCol.Header] =
            new Vector4(0.3490196168422699f, 0.4196078479290009f, 0.3098039329051971f, 1.0f);
        style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.3490196168422699f, 0.4196078479290009f,
            0.3098039329051971f, 0.6000000238418579f);
        style.Colors[(int)ImGuiCol.HeaderActive] =
            new Vector4(0.5372549295425415f, 0.5686274766921997f, 0.5098039507865906f, 0.5f);
        style.Colors[(int)ImGuiCol.Separator] =
            new Vector4(0.1372549086809158f, 0.1568627506494522f, 0.1098039224743843f, 1.0f);
        style.Colors[(int)ImGuiCol.SeparatorHovered] =
            new Vector4(0.5372549295425415f, 0.5686274766921997f, 0.5098039507865906f, 1.0f);
        style.Colors[(int)ImGuiCol.SeparatorActive] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.ResizeGrip] =
            new Vector4(0.1882352977991104f, 0.2274509817361832f, 0.1764705926179886f, 0.0f);
        style.Colors[(int)ImGuiCol.ResizeGripHovered] =
            new Vector4(0.5372549295425415f, 0.5686274766921997f, 0.5098039507865906f, 1.0f);
        style.Colors[(int)ImGuiCol.ResizeGripActive] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.Tab] =
            new Vector4(0.3490196168422699f, 0.4196078479290009f, 0.3098039329051971f, 1.0f);
        style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.5372549295425415f, 0.5686274766921997f,
            0.5098039507865906f, 0.7799999713897705f);
        style.Colors[(int)ImGuiCol.TabSelected] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.TabDimmed] =
            new Vector4(0.239215686917305f, 0.2666666805744171f, 0.2000000029802322f, 1.0f);
        style.Colors[(int)ImGuiCol.TabDimmedSelected] =
            new Vector4(0.3490196168422699f, 0.4196078479290009f, 0.3098039329051971f, 1.0f);
        style.Colors[(int)ImGuiCol.PlotLines] =
            new Vector4(0.6078431606292725f, 0.6078431606292725f, 0.6078431606292725f, 1.0f);
        style.Colors[(int)ImGuiCol.PlotLinesHovered] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.PlotHistogram] =
            new Vector4(1.0f, 0.7764706015586853f, 0.2784313857555389f, 1.0f);
        style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.0f, 0.6000000238418579f, 0.0f, 1.0f);
        style.Colors[(int)ImGuiCol.TableHeaderBg] =
            new Vector4(0.1882352977991104f, 0.1882352977991104f, 0.2000000029802322f, 1.0f);
        style.Colors[(int)ImGuiCol.TableBorderStrong] =
            new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3490196168422699f, 1.0f);
        style.Colors[(int)ImGuiCol.TableBorderLight] =
            new Vector4(0.2274509817361832f, 0.2274509817361832f, 0.2470588237047195f, 1.0f);
        style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
        style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 1.0f, 1.0f, 0.05999999865889549f);
        style.Colors[(int)ImGuiCol.TextSelectedBg] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.DragDropTarget] =
            new Vector4(0.729411780834198f, 0.6666666865348816f, 0.239215686917305f, 1.0f);
        style.Colors[(int)ImGuiCol.NavHighlight] =
            new Vector4(0.5882353186607361f, 0.5372549295425415f, 0.1764705926179886f, 1.0f);
        style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 1.0f, 1.0f, 0.699999988079071f);
        style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f,
            0.800000011920929f, 0.2000000029802322f);
        style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.800000011920929f, 0.800000011920929f,
            0.800000011920929f, 0.3499999940395355f);
    }

    public static void SetupModernTheme(float alphaThreshold)
    {
        var style = ImGui.GetStyle();
        style.Colors[(int)ImGuiCol.Text] = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
        style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.94f, 0.94f, 0.94f, 0.94f);
        style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
        style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(1.00f, 1.00f, 1.00f, 0.94f);
        style.Colors[(int)ImGuiCol.Border] = new Vector4(0.00f, 0.00f, 0.00f, 0.39f);
        style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(1.00f, 1.00f, 1.00f, 0.10f);
        style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(1.00f, 1.00f, 1.00f, 0.94f);
        style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.40f);
        style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.67f);
        style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.96f, 0.96f, 0.96f, 1.00f);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(1.00f, 1.00f, 1.00f, 0.51f);
        style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.82f, 0.82f, 0.82f, 1.00f);
        style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.86f, 0.86f, 0.86f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.98f, 0.98f, 0.98f, 0.53f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.69f, 0.69f, 0.69f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.59f, 0.59f, 0.59f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.49f, 0.49f, 0.49f, 1.00f);
        style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.24f, 0.52f, 0.88f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
        style.Colors[(int)ImGuiCol.Button] = new Vector4(0.26f, 0.59f, 0.98f, 0.40f);
        style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
        style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.06f, 0.53f, 0.98f, 1.00f);
        style.Colors[(int)ImGuiCol.Header] = new Vector4(0.26f, 0.59f, 0.98f, 0.31f);
        style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.80f);
        style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
        style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.00f, 1.00f, 1.00f, 0.50f);
        style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.26f, 0.59f, 0.98f, 0.67f);
        style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.26f, 0.59f, 0.98f, 0.95f);
        style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.39f, 0.39f, 0.39f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
        style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.26f, 0.59f, 0.98f, 0.35f);
        style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.35f);

        for (ImGuiCol i = 0; i < ImGuiCol.COUNT; i++)
        {
            var color = style.Colors[(int)i];
            ImGui.ColorConvertRGBtoHSV(color.X, color.Y, color.Z, out float h, out float s, out float v);
            if (s < 0.1f)
            {
                v = 1.0f - v;
            }

            ImGui.ColorConvertHSVtoRGB(h, s, v, out float r, out float g, out float b);
            color.X = r;
            color.Y = g;
            color.Z = b;
            if (color.W < alphaThreshold || i == ImGuiCol.FrameBg || i == ImGuiCol.WindowBg ||
                i == ImGuiCol.ChildBg)
            {
                color.W *= alphaThreshold;
            }

            style.Colors[(int)i] = color;
        }

        style.ChildBorderSize = 1.0f;
        style.FrameBorderSize = 0.0f;
        style.PopupBorderSize = 1.0f;
        style.WindowBorderSize = 0.0f;
        style.FrameRounding = 3.0f;
        style.Alpha = 1.0f;
        style.ChildRounding = 3.0f;
        style.WindowRounding = 3.0f;
    }

    public static void DrawTransform(ref Transform transform)
    {
        ImGui.InputFloat3("Position", ref transform.Translation);
        Vector3 rot = Raymath.QuaternionToEuler(transform.Rotation);
        if(ImGui.InputFloat3("Rotation", ref rot))
        {
            transform.Rotation = Raymath.QuaternionFromEuler(rot.X, rot.Y, rot.Z);
        }

        ImGui.InputFloat3("Scale", ref transform.Scale);
    }

    private static Dictionary<Object, Dictionary<FieldInfo, string>> _fieldsCache =
        new Dictionary<object, Dictionary<FieldInfo, string>>();

    private static void AddFields(Object obj)
    {
        var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        _fieldsCache[obj] = new Dictionary<FieldInfo, string>();
        foreach (var fieldInfo in fields)
        {
            var attribute = fieldInfo.GetCustomAttributes<Attribute>();
            if (attribute != null!)
            {
                _fieldsCache[obj].Add(fieldInfo, StringUtils.GetPretty(fieldInfo.Name));
            }
        }
    }

    public static void ClearFields()
    {
        _fieldsCache.Clear();
    }
    
    //we could probably build the dictionaries in runtime.
    //then for every caller, we index into that field dictionary.
    public static void DrawFields(Object obj)
    {
        if (!_fieldsCache.ContainsKey(obj))
        {
            AddFields(obj);
        }

        var fields = _fieldsCache[obj];
        foreach (var fieldInfo in fields)
        {
            var header = fieldInfo.Key.GetCustomAttribute<HeaderAttribute>();
            if (header != null!)
            {
                ImGui.Text(header.Label);
            }

            var serializeField = fieldInfo.Key.GetCustomAttribute<SerializeFieldAttribute>();
            if (serializeField != null!)
            {
                var value = fieldInfo.Key.GetValue(obj);
                if (value is Vector3 v)
                {
                    if (ImGui.InputFloat3(fieldInfo.Value, ref v))
                    {
                        fieldInfo.Key.SetValue(obj, v);
                    }
                }
                else if (value is float f)
                {
                    if (ImGui.InputFloat(fieldInfo.Value, ref f))
                    {
                        fieldInfo.Key.SetValue(obj, f);
                    }
                }
            }
        }
    }
}