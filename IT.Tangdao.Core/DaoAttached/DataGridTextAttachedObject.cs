using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace IT.Tangdao.Core.DaoAttached
{
    public static class DataGridTextAttachedObject
    {
        #region Background 附加属性

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached(
                "Background",
                typeof(Brush),
                typeof(DataGridTextAttachedObject),
                new PropertyMetadata(Brushes.Transparent, OnBackgroundChanged));

        public static Brush GetBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundProperty);
        }

        public static void SetBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundProperty, value);
        }

        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGridTextColumn column)
            {
                // 创建新样式（或使用现有样式）
                var style = new Style(typeof(DataGridCell));

                // 设置背景属性
                style.Setters.Add(new Setter(Control.BackgroundProperty, e.NewValue));

                // 应用样式
                column.CellStyle = style;
            }
        }

        #endregion Background 附加属性
    }
}