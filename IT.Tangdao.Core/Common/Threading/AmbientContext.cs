using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Threading
{
    /// <summary>
    /// 兼容 .NET 6+ 的 AmbientContext：
    /// - 值类型：ThreadStatic，0 分配
    /// - 引用类型：AsyncLocal，随 ExecutionContext 流动
    /// - 支持“同类型多份数据”（具名槽）
    /// </summary>
    public static class AmbientContext
    {
        #region ---------- 值类型 ----------

        private static class Slot<T> where T : struct
        {
            [ThreadStatic] private static T _value;

            public static T Value
            {
                get => _value;
                set { _value = value; _initialized = true; }
            }

            private static bool _initialized;
            public static bool IsInitialized => _initialized;

            public static void Clear()
            {
                _value = default;
                _initialized = false;
            }
        }

        public static T GetOrCreate<T>(Func<T> factory) where T : struct
        {
            if (!Slot<T>.IsInitialized)
            {
                Slot<T>.Value = factory();
            }
            return Slot<T>.Value;
        }

        public static void Set<T>(T value) where T : struct => Slot<T>.Value = value;

        public static T Get<T>() where T : struct => Slot<T>.Value;

        public static void Clear<T>() where T : struct => Slot<T>.Clear();

        #endregion ---------- 值类型 ----------

        #region ---------- 引用类型（AsyncLocal） ----------

        // 默认槽
        private static readonly AsyncLocal<object?> _defaultSlot = new();

        // 具名槽：ConditionalWeakTable 避免自己泄漏
        private static readonly ConditionalWeakTable<Type, AsyncLocal<Dictionary<string, object?>>> _namedSlots = new();

        /*-------- 默认槽 --------*/

        public static T? GetCurrent<T>() where T : class => _defaultSlot.Value as T;

        public static void SetCurrent<T>(T? value) where T : class => _defaultSlot.Value = value;

        public static void ClearCurrent<T>() where T : class => _defaultSlot.Value = null;

        /*-------- 具名槽 --------*/

        public static T? GetCurrent<T>(string name) where T : class
        {
            var dict = GetOrCreateNamedDict(typeof(T));
            return dict.TryGetValue(name, out var v) ? v as T : null;
        }

        public static void SetCurrent<T>(string name, T? value) where T : class
        {
            var dict = GetOrCreateNamedDict(typeof(T));
            dict[name] = value;
        }

        public static void ClearCurrent<T>(string name) where T : class
        {
            var dict = GetOrCreateNamedDict(typeof(T));
            dict.Remove(name);
        }

        /*-------- 底层 --------*/

        private static Dictionary<string, object?> GetOrCreateNamedDict(Type type)
        {
            var local = _namedSlots.GetOrCreateValue(type);
            return local.Value ??= new Dictionary<string, object?>();
        }

        #endregion ---------- 引用类型（AsyncLocal） ----------
    }
}