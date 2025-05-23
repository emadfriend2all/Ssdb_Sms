﻿using MudBlazor;

namespace Showmatics.Blazor.Client.Infrastructure.Theme;

public static class CustomTypography
{
    public static Typography FSHTypography => new Typography()
    {
        Default = new Default()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = ".800rem",
            FontWeight = 200,
            LineHeight = 1.43,
            LetterSpacing = ".01071em"
        },
        H1 = new H1()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "6rem",
            FontWeight = 300,
            LineHeight = 1.167,
            LetterSpacing = "-.01562em"
        },
        H2 = new H2()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "3.75rem",
            FontWeight = 300,
            LineHeight = 1.2,
            LetterSpacing = "-.00833em"
        },
        H3 = new H3()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "3rem",
            FontWeight = 400,
            LineHeight = 1.167,
            LetterSpacing = "0"
        },
        H4 = new H4()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "2.125rem",
            FontWeight = 400,
            LineHeight = 1.235,
            LetterSpacing = ".00735em"
        },
        H5 = new H5()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "1.5rem",
            FontWeight = 400,
            LineHeight = 1.334,
            LetterSpacing = "0"
        },
        H6 = new H6()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "1.25rem",
            FontWeight = 400,
            LineHeight = 1.6,
            LetterSpacing = ".0075em"
        },
        Button = new Button()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = ".875rem",
            FontWeight = 400,
            LineHeight = 1.75,
            LetterSpacing = "0"
        },
        Body1 = new Body1()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "1rem",
            FontWeight = 400,
            LineHeight = 1.5,
            LetterSpacing = "0"
        },
        Body2 = new Body2()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = ".875rem",
            FontWeight = 400,
            LineHeight = 1.43,
            LetterSpacing = "0"
        },
        Caption = new Caption()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = ".75rem",
            FontWeight = 200,
            LineHeight = 1.66,
            LetterSpacing = "0"
        },
        Subtitle1 = new Subtitle1()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = "1rem",
            FontWeight = 400,
            LineHeight = 1.57,
            LetterSpacing = ".00714em"
        },
        Subtitle2 = new Subtitle2()
        {
            FontFamily = new[] { "Tajawal", "Helvetica", "Arial", "sans-serif" },
            FontSize = ".875rem",
            FontWeight = 400,
            LineHeight = 1.57,
            LetterSpacing = ".00714em"
        }
    };
}