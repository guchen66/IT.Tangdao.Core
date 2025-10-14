using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT.Tangdao.Core.Paths
{
    public readonly struct RelativePath : IEquatable<RelativePath>, IComparable<RelativePath>
    {
        private readonly string _path;

        /*---------- 1. 构造 ----------*/

        public RelativePath(string path)
        {
            _path = path == null || path.Length == 0
                ? string.Empty
                : NormalizeSeparators(path);          // 可能返回原引用
        }

        /// <summary>
        /// 统一分隔符，**无替代符时直接返回原引用**
        /// </summary>
        private static string NormalizeSeparators(ReadOnlySpan<char> src)
        {
            char platSep = Path.DirectorySeparatorChar;
            char altSep = platSep == '/' ? '\\' : '/';

            int idx = src.IndexOf(altSep);
            if (idx == -1)                  // 热路径：直接原串
                return src.ToString();

            // 需要复制
            var arr = ArrayPool<char>.Shared.Rent(src.Length);
            try
            {
                src.CopyTo(arr);
                for (int i = idx; i < src.Length; i++)
                    if (arr[i] == altSep)
                        arr[i] = platSep;
                return new string(arr, 0, src.Length);
            }
            finally
            {
                ArrayPool<char>.Shared.Return(arr, clearArray: false);
            }
        }

        /*---------- 2. 空单例 ----------*/
        public static RelativePath Empty => default;

        /*---------- 3. 基本成员 ----------*/
        public string Value => _path;

        public override string ToString() => _path;

        /*---------- 4. 相等 & 比较 ----------*/

        public bool Equals(RelativePath other) =>
            _path.Equals(other._path, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj) =>
            obj is RelativePath other && Equals(other);

        public override int GetHashCode() =>
            StringComparer.OrdinalIgnoreCase.GetHashCode(_path);

        public int CompareTo(RelativePath other) =>
            StringComparer.OrdinalIgnoreCase.Compare(_path, other._path);

        public static bool operator ==(RelativePath left, RelativePath right) => left.Equals(right);

        public static bool operator !=(RelativePath left, RelativePath right) => !left.Equals(right);

        /*---------- 5. 转换 ----------*/

        public static explicit operator string(RelativePath p) => p._path;

        public static implicit operator RelativePath(string? s) => new RelativePath(s);

        /*---------- 6. 路径操作 ----------*/

        public RelativePath Combine(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return this;

            // .NET 6 有 Path.IsPathFullyQualified，但相对路径只要看是否 Rooted
            if (Path.IsPathRooted(relativePath))
                throw new ArgumentException("Cannot combine an absolute path.", nameof(relativePath));

            string combined = Path.Combine(_path, relativePath);
            return new RelativePath(combined);
        }

        public RelativePath WithExtension(string extension) =>
            new(Path.ChangeExtension(_path, extension));

        public RelativePath Parent()
        {
            string? parent = Path.GetDirectoryName(_path);
            return new RelativePath(parent);
        }

        public string FileName => Path.GetFileName(_path);
        public string FileNameWithoutExtension => Path.GetFileNameWithoutExtension(_path);

        /*---------- 7. 语义判断 ----------*/

        public bool IsCurrentDirectory =>
            _path.Length == 0 || _path == ".";

        /// <summary>
        /// 是否以 "../" 或 "..\" 开头（严格段匹配）
        /// </summary>
        public bool StartsWithParentReference
        {
            get
            {
                if (_path.Length < 2) return false;
                return _path.StartsWith("..") &&
                       (_path.Length == 2 || _path[2] == Path.DirectorySeparatorChar);
            }
        }

        /*---------- 8. 高性能 Span 友好重载 ----------*/

        public RelativePath(ReadOnlySpan<char> path) : this(path.ToString())
        {
        }

        public static RelativePath Create(ReadOnlySpan<char> path) => new RelativePath(path);
    }
}