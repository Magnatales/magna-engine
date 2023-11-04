using System.Numerics;
using ImGuiNET;

namespace rlImGUI;

public static class SpectrumStyle
{
    private const uint NONE = 0x00000000; // transparent
    private const uint WHITE = 0xFFFFFF;
    private const uint BLACK = 0x000000;


    const uint GRAY50 = 0x252525;
        const uint GRAY75 = 0x2F2F2F;
        const uint GRAY100 = 0x323232;
        const uint GRAY200 = 0x393939;
        const uint GRAY300 = 0x3E3E3E;
        const uint GRAY400 = 0x4D4D4D;
        const uint GRAY500 = 0x5C5C5C;
        const uint GRAY600 = 0x7B7B7B;
        const uint GRAY700 = 0x999999;
        const uint GRAY800 = 0xCDCDCD;
        const uint GRAY900 = 0xFFFFFF;
        const uint BLUE400 = 0x2680EB;
        const uint BLUE500 = 0x378EF0;
        const uint BLUE600 = 0x4B9CF5;
        const uint BLUE700 = 0x5AA9FA;
        const uint RED400 = 0xE34850;
        const uint RED500 = 0xEC5B62;
        const uint RED600 = 0xF76D74;
        const uint RED700 = 0xFF7B82;
        const uint ORANGE400 = 0xE68619;
        const uint ORANGE500 = 0xF29423;
        const uint ORANGE600 = 0xF9A43F;
        const uint ORANGE700 = 0xFFB55B;
        const uint GREEN400 = 0x2D9D78;
        const uint GREEN500 = 0x33AB84;
        const uint GREEN600 = 0x39B990;
        const uint GREEN700 = 0x3FC89C;
        const uint INDIGO400 = 0x6767EC;
        const uint INDIGO500 = 0x7575F1;
        const uint INDIGO600 = 0x8282F6;
        const uint INDIGO700 = 0x9090FA;
        const uint CELERY400 = 0x44B556;
        const uint CELERY500 = 0x4BC35F;
        const uint CELERY600 = 0x51D267;
        const uint CELERY700 = 0x58E06F;
        const uint MAGENTA400 = 0xD83790;
        const uint MAGENTA500 = 0xE2499D;
        const uint MAGENTA600 = 0xEC5AAA;
        const uint MAGENTA700 = 0xF56BB7;
        const uint YELLOW400 = 0xDFBF00;
        const uint YELLOW500 = 0xEDCC00;
        const uint YELLOW600 = 0xFAD900;
        const uint YELLOW700 = 0xFFE22E;
        const uint FUCHSIA400 = 0xC038CC;
        const uint FUCHSIA500 = 0xCF3EDC;
        const uint FUCHSIA600 = 0xD951E5;
        const uint FUCHSIA700 = 0xE366EF;
        const uint SEAFOAM400 = 0x1B959A;
        const uint SEAFOAM500 = 0x20A3A8;
        const uint SEAFOAM600 = 0x23B2B8;
        const uint SEAFOAM700 = 0x26C0C7;
        const uint CHARTREUSE400 = 0x85D044;
        const uint CHARTREUSE500 = 0x8EDE49;
        const uint CHARTREUSE600 = 0x9BEC54;
        const uint CHARTREUSE700 = 0xA3F858;
        const uint PURPLE400 = 0x9256D9;
        const uint PURPLE500 = 0x9D64E1;
        const uint PURPLE600 = 0xA873E9;
        const uint PURPLE700 = 0xB483F0;


    public static ImGuiStylePtr GetStyle()
    {
        var style = ImGui.GetStyle();
        style.GrabRounding = 4.0f;
        var colors = style.Colors;

        //colors[(int)ImGuiCol.Text] = ImGui.ColorConvertU32ToFloat4(GRAY800);
        //colors[(int)ImGuiCol.TextDisabled] = ImGui.ColorConvertU32ToFloat4(GRAY500);
        colors[(int)ImGuiCol.WindowBg] = new Vector4(217, 130, 137, 255f);
        colors[(int)ImGuiCol.ChildBg] = new Vector4(217, 130, 137, 255f);
        colors[(int)ImGuiCol.PopupBg] = ImGui.ColorConvertU32ToFloat4(GRAY50);
        colors[(int)ImGuiCol.Border] = ImGui.ColorConvertU32ToFloat4(GRAY300);
        colors[(int)ImGuiCol.BorderShadow] = ImGui.ColorConvertU32ToFloat4(NONE);
        colors[(int)ImGuiCol.FrameBg] = ImGui.ColorConvertU32ToFloat4(GRAY75);
        colors[(int)ImGuiCol.FrameBgHovered] = ImGui.ColorConvertU32ToFloat4(GRAY50);
        colors[(int)ImGuiCol.FrameBgActive] = ImGui.ColorConvertU32ToFloat4(GRAY200);
        colors[(int)ImGuiCol.TitleBg] = ImGui.ColorConvertU32ToFloat4(GRAY300);
        colors[(int)ImGuiCol.TitleBgActive] = ImGui.ColorConvertU32ToFloat4(GRAY200);
        colors[(int)ImGuiCol.TitleBgCollapsed] = ImGui.ColorConvertU32ToFloat4(GRAY400);
        colors[(int)ImGuiCol.MenuBarBg] = ImGui.ColorConvertU32ToFloat4(GRAY100);
        colors[(int)ImGuiCol.ScrollbarBg] = ImGui.ColorConvertU32ToFloat4(GRAY100);
        colors[(int)ImGuiCol.ScrollbarGrab] = ImGui.ColorConvertU32ToFloat4(GRAY400);
        colors[(int)ImGuiCol.ScrollbarGrabHovered] = ImGui.ColorConvertU32ToFloat4(GRAY600);
        colors[(int)ImGuiCol.ScrollbarGrabActive] = ImGui.ColorConvertU32ToFloat4(GRAY700);
        colors[(int)ImGuiCol.CheckMark] = ImGui.ColorConvertU32ToFloat4(BLUE500);
        colors[(int)ImGuiCol.SliderGrab] = ImGui.ColorConvertU32ToFloat4(GRAY700);
        colors[(int)ImGuiCol.SliderGrabActive] = ImGui.ColorConvertU32ToFloat4(GRAY800);
        colors[(int)ImGuiCol.Button] = ImGui.ColorConvertU32ToFloat4(GRAY75);
        colors[(int)ImGuiCol.ButtonHovered] = ImGui.ColorConvertU32ToFloat4(GRAY50);
        colors[(int)ImGuiCol.ButtonActive] = ImGui.ColorConvertU32ToFloat4(GRAY200);
        colors[(int)ImGuiCol.Header] = ImGui.ColorConvertU32ToFloat4(BLUE400);
        colors[(int)ImGuiCol.HeaderHovered] = ImGui.ColorConvertU32ToFloat4(BLUE500);
        colors[(int)ImGuiCol.HeaderActive] = ImGui.ColorConvertU32ToFloat4(BLUE600);
        colors[(int)ImGuiCol.Separator] = ImGui.ColorConvertU32ToFloat4(GRAY400);
        colors[(int)ImGuiCol.SeparatorHovered] = ImGui.ColorConvertU32ToFloat4(GRAY600);
        colors[(int)ImGuiCol.SeparatorActive] = ImGui.ColorConvertU32ToFloat4(GRAY700);
        colors[(int)ImGuiCol.ResizeGrip] = ImGui.ColorConvertU32ToFloat4(GRAY400);
        colors[(int)ImGuiCol.ResizeGripHovered] = ImGui.ColorConvertU32ToFloat4(GRAY600);
        colors[(int)ImGuiCol.ResizeGripActive] = ImGui.ColorConvertU32ToFloat4(GRAY700);
        colors[(int)ImGuiCol.PlotLines] = ImGui.ColorConvertU32ToFloat4(BLUE400);
        colors[(int)ImGuiCol.PlotLinesHovered] = ImGui.ColorConvertU32ToFloat4(BLUE600);
        colors[(int)ImGuiCol.PlotHistogram] = ImGui.ColorConvertU32ToFloat4(BLUE400);
        colors[(int)ImGuiCol.PlotHistogramHovered] = ImGui.ColorConvertU32ToFloat4(BLUE600);
        colors[(int)ImGuiCol.TextSelectedBg] = ImGui.ColorConvertU32ToFloat4((BLUE400 & 0x00FFFFFF) | 0x33000000);
        colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1, 1, 0, 0.9f);
        colors[(int)ImGuiCol.NavHighlight] = ImGui.ColorConvertU32ToFloat4((GRAY900 & 0x00FFFFFF) | 0x0A000000);
        colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
        colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
        colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.35f);
        return style;
    }
}