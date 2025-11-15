using IT.Tangdao.Core;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using IT.Tangdao.Core.Ioc;
using IT.Tangdao.Core.Attributes;
using IT.Tangdao.Core.Bootstrap;

// 常规信息
[assembly: AssemblyTitle("Tangdao.Core")]
[assembly: AssemblyProduct("Tangdao.Core")]
[assembly: ComVisible(false)]

// 模块自注册（关键一行）
[assembly: TangdaoModule(typeof(FrameworkDefaultComponentModule))]
[assembly: TangdaoModule(typeof(AutoRegisterComponentModule))]