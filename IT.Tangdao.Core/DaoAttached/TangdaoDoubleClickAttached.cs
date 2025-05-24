using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace IT.Tangdao.Core.DaoAttached
{
    public class TangdaoDoubleClickAttached
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(TangdaoDoubleClickAttached),
                new PropertyMetadata(null, OnCommandChanged));

        public static void SetCommand(DependencyObject obj, ICommand value) =>
            obj.SetValue(CommandProperty, value);

        public static ICommand GetCommand(DependencyObject obj) =>
            (ICommand)obj.GetValue(CommandProperty);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control)
            {
                // 移除旧事件（避免重复订阅）
                control.PreviewMouseLeftButtonDown -= OnMouseDoubleClick;

                // 添加新事件
                if (e.NewValue != null)
                    control.PreviewMouseLeftButtonDown += OnMouseDoubleClick;
            }
        }

        private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // 仅处理双击
            {
                var control = sender as DependencyObject;
                var command = GetCommand(control);

                if (command?.CanExecute(null) == true)
                    command.Execute(null);

                e.Handled = true; // 阻止事件继续冒泡
            }
        }
    }
}