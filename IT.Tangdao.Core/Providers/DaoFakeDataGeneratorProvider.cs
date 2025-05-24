using IT.Tangdao.Core.DaoAttributes;
using IT.Tangdao.Core.DaoEnums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Providers
{
    public class DaoFakeDataGeneratorProvider<T> where T : new()
    {
        private static readonly ConcurrentDictionary<string, Action<T, object>> _propertySetters =
            new ConcurrentDictionary<string, Action<T, object>>();

        private static readonly Random _random = new Random();
        private static readonly HashSet<int> _usedIds = new HashSet<int>();

        // 常用数据池
        private static readonly string[] ChineseCities = { "北京", "上海", "广州", "深圳", "杭州", "成都", "武汉", "南京" };

        private static readonly string[] CommonHobbies = { "阅读", "旅行", "摄影", "烹饪", "运动", "音乐", "电影" };

        // 自增ID计数器
        private static int _intIdCounter = 1;

        public List<T> GenerateRandomData(int count)
        {
            var result = new List<T>();
            for (int i = 0; i < count; i++)
            {
                result.Add(CreateRandomInstance());
            }
            // 重置自增计数器（可选，根据需求决定）
            _intIdCounter = 1;
            _usedIds.Clear(); // 清空已使用的ID，以便下次生成
            return result;
        }

        private T CreateRandomInstance()
        {
            var instance = new T();  //调用无参构造器
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);   //获取所有公共属性

            foreach (var property in properties)
            {
                var setter = GetOrCreateSetter(property);         //获取属性设置器委托
                var randomValue = GenerateRandomValue(property);  //生成随机值
                setter(instance, randomValue);                    //设置属性值
            }

            return instance;
        }

        private Action<T, object> GetOrCreateSetter(PropertyInfo property)
        {
            string key = property.Name;

            return _propertySetters.GetOrAdd(key, k =>
            {
                var instanceParam = Expression.Parameter(typeof(T), "instance");
                var valueParam = Expression.Parameter(typeof(object), "value");

                // 转换并设置属性
                var convertedValue = Expression.Convert(valueParam, property.PropertyType);
                var propertyAccess = Expression.Property(instanceParam, property);
                var assign = Expression.Assign(propertyAccess, convertedValue);

                return Expression.Lambda<Action<T, object>>(assign, instanceParam, valueParam).Compile();
            });
        }

        private object GenerateRandomValue(PropertyInfo property)
        {
            if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
                (property.PropertyType == typeof(int) || property.PropertyType == typeof(long)))
            {
                return _intIdCounter++;
            }
            // 检查是否有自定义特性
            var fakeDataAttr = property.GetCustomAttribute<DaoFakeDataInfoAttribute>();

            if (fakeDataAttr != null)
            {
                // 根据特性信息生成数据
                switch (fakeDataAttr.Message)
                {
                    case "姓名":
                        return GetRandomEnumValue<DaoName>().ToString();

                    case "城市":
                        return ChineseCities[_random.Next(ChineseCities.Length)];

                    case "爱好":
                        if (fakeDataAttr.EnumType != null)
                            return GetRandomEnumValue(fakeDataAttr.EnumType);
                        return CommonHobbies[_random.Next(CommonHobbies.Length)];
                        // 可以添加更多自定义类型
                }
            }
            if (property.PropertyType == typeof(int))
            {
                return GenerateUniqueId();
            }

            if (property.PropertyType == typeof(string))
            {
                return GenerateRandomString();
            }

            if (property.PropertyType == typeof(DateTime))
            {
                return GenerateRandomDateTime();
            }

            // 默认值处理
            if (property.PropertyType.IsValueType)
            {
                return Activator.CreateInstance(property.PropertyType);
            }

            return null;
        }

        private int GenerateUniqueId()
        {
            int id;
            do
            {
                id = _random.Next(1, 1000);
            } while (_usedIds.Contains(id));

            _usedIds.Add(id);
            return id;
        }

        private string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private DateTime GenerateRandomDateTime()
        {
            DateTime start = new DateTime(1990, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

        private object GetRandomEnumValue(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            return values.GetValue(_random.Next(values.Length));
        }

        private TEnum GetRandomEnumValue<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum));
            return (TEnum)values.GetValue(_random.Next(values.Length));
        }
    }
}