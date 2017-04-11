using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JetBrains.Annotations;

namespace DependencyProperties
{
    public class InterestingButton
        : Button
    {
        [NotNull]
        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register(nameof(HoverColor), typeof(Color), typeof(InterestingButton), new PropertyMetadata(Colors.Orange));

        public Color HoverColor
        {
            get => (Color)GetValue(HoverColorProperty);
            set => SetValue(HoverColorProperty, value);
        }

        public InterestingButton()
        {
            var contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.Name = nameof(contentPresenter);
            contentPresenter.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
            contentPresenter.SetValue(PaddingProperty, new Thickness(1));

            var border = new FrameworkElementFactory(typeof(Border));
            border.Name = nameof(border);

            border.AppendChild(contentPresenter);

            var trigger = new Trigger { Property = IsMouseOverProperty, Value = true };
            trigger.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(HoverColor), nameof(border)));

            var controlTemplate = new ControlTemplate(typeof(Button)) { VisualTree = border };
            controlTemplate.Triggers.Add(trigger);
            Template = controlTemplate;
        }
    }
}
