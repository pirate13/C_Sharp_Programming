using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace ClockApp;

public sealed class ClockControl : Control
{
    private readonly DispatcherTimer _timer;

    public ClockControl()
    {
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _timer.Tick += (_, _) => InvalidateVisual();
        _timer.Start();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        var now = DateTime.Now;

        // фон
        context.FillRectangle(Brushes.White, new Rect(Bounds.Size));

        // область часов
        double w = Bounds.Width;
        double h = Bounds.Height;

        double size = Math.Min(w, h) - 40;
        if (size <= 0) return;

        var rect = new Rect((w - size) / 2, 20, size, size);
        var center = rect.Center;
        double r = size / 2;

        // циферблат
        var facePen = new Pen(Brushes.Black, 4);
        context.DrawEllipse(null, facePen, center, r, r);

        // деления
        for (int i = 0; i < 60; i++)
        {
            double angle = (Math.PI * 2 * i / 60.0) - Math.PI / 2;
            double outer = r - 6;
            double inner = (i % 5 == 0) ? r - 22 : r - 14;

            var p1 = new Point(center.X + outer * Math.Cos(angle), center.Y + outer * Math.Sin(angle));
            var p2 = new Point(center.X + inner * Math.Cos(angle), center.Y + inner * Math.Sin(angle));

            var pen = new Pen(Brushes.Black, i % 5 == 0 ? 3 : 1);
            context.DrawLine(pen, p1, p2);
        }

        // углы стрелок
        double sec = now.Second + now.Millisecond / 1000.0;
        double min = now.Minute + sec / 60.0;
        double hour = (now.Hour % 12) + min / 60.0;

        double secAngle = (Math.PI * 2 * (sec / 60.0)) - Math.PI / 2;
        double minAngle = (Math.PI * 2 * (min / 60.0)) - Math.PI / 2;
        double hourAngle = (Math.PI * 2 * (hour / 12.0)) - Math.PI / 2;

        // стрелки
        DrawHand(context, center, r * 0.55, hourAngle, Brushes.Black, 6);
        DrawHand(context, center, r * 0.75, minAngle, Brushes.Black, 4);
        DrawHand(context, center, r * 0.85, secAngle, Brushes.Red, 2);

        // центр
        context.DrawEllipse(Brushes.Black, null, center, 5, 5);

        // цифровое время
        var timeText = now.ToString("HH:mm:ss");
        var ft = new FormattedText(
            timeText,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface("Segoe UI"),
            20,
            Brushes.Black
        );

        var tx = (w - ft.Width) / 2;
        var ty = rect.Bottom + 15;
        context.DrawText(ft, new Point(tx, ty));

    }

    private static void DrawHand(DrawingContext ctx, Point center, double length, double angle, IBrush brush, double thickness)
    {
        var end = new Point(
            center.X + length * Math.Cos(angle),
            center.Y + length * Math.Sin(angle)
        );

        var pen = new Pen(brush, thickness, lineCap: PenLineCap.Round);
        ctx.DrawLine(pen, center, end);
    }
}
