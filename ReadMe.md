#### 1、命令

正常命令：TangdaoCommand   

异步命令：TangdaoAsyncCommand

静态命令：MinidaoCommand

对TextBlock等没有命令的控件增加命令附加属性

````js
 <TextBlock Text="点击我执行简单命令" 
                   local:CommandBehavior.Command="{Binding SimpleCommand}"
                   Margin="10" Padding="10"
                   Background="LightBlue" Cursor="Hand"/>
````



#### 2、事件聚合器

用于父子组件的通讯

```C#
_eventTransmit.Publish<T>();

_eventTransmit.Subscribe<T>(Execute);

T:DaoEventBase
```

#### 3、增加另外一种全新的方式去发送数据

```C#
MainViewModel: 发送
private void Execute()
{
      ITangdaoParameter tangdaoParameter = new TangdaoParameter();
      tangdaoParameter.Add("001",Name);
      this.RunSameLevelWindowAsync<LoginView>(tangdaoParameter);
}
LoginViewModel:接收
 public void Response(ITangdaoParameter tangdaoParameter)
 {
     Name = tangdaoParameter.Get<string>("001");
 }
```



#### 2、容器

ITangdaoContainer

对PLC的读取进行了扩展未完成

```c#
  container.RegisterPlcServer(plc => 
  {
      plc.PlcType= PlcType.Siemens;
      plc.PlcIpAddress = "127.0.0.1";
      plc.Port = "502";

  });

  container.RegisterType<IPlcReadService,PlcReadService>();
  var plcservice=provider.Resolve<IPlcReadService>();
  plcservice.ReadAsync("DM200");
```



#### 3、扩展方法

StringExtension 可以方便一些代码

CryptoHelper进行加密解密

```json
 string originalText = "Hello, 这是一个测试消息！";
 Console.WriteLine("原始文本: " + originalText);

 // 加密
 string encryptedText = CryptoHelper.Encrypt(originalText);
 Console.WriteLine("加密后的文本: " + encryptedText);

 // 解密
 string decryptedText = CryptoHelper.Decrypt(encryptedText);
 Console.WriteLine("解密后的文本: " + decryptedText);
```



#### 4、增加一些常用的Helper类

DirectoryHelper

#### 5、读取数据

使用IReadService读取数据

读取根目录节点的XML

```json
readService.Current.SelectNode("");
```

读取根目录下的Json

```js
 "GeneratorData": {
   "Title": "SqlServer",
   "IsGenerator": "true", //初始化数据库和表格
   "IsSeedData": "false" //自动生成表数据
 }
```

```json
  IReadService readService = new ReadService();
  readService.Current.JsonData = "AppConfig.json";
  readService.Current["GeneratorData"].SelectValue("Title");
```

读取指定地址下的txt文件

```json
readService.Read("");
```

如果json的格式是

```json
 "WCF": {
   "Id": "001",
   "Name": "IgniteWeb",
   "Startup": false,
 },
```

读取json的时候可以写

```json
 _readService.Load("appsetting.json", DaoFileType.Json);
 var s1 = _readService.Current["WCF"].SelectValue("Id").Value;
```

读取根目录下所有json文件名称

```js
JsonConverHelper.GetRootJsonFileNames();
```

读取json文件的内容

```js
JsonConverHelper.GetJsonContent("AppConfig.json","WCF");
```



读取Config的时候可以写

```json
var model = _readService.Current.SelectCustomConfig("unity.config", "Tangdao").Result;
if (model is Dictionary<string, string> dicts)
{
    List<HomeMenuItem> menuItems = dicts.Select(kvp => new HomeMenuItem
    {
        Title = kvp.Key,
        ViewModelName = kvp.Value
    }).ToList();
    HomeMenuItems.AddRange(menuItems);
}
```

其中unity.config可以写

```
<configSections>
<section name="Tangdao" type="IT.Tangdao.Framework.DaoCommon.TangdaoMenuSection,IT.Tangdao.Core" />
</configSections>
<Tangdao>
	<menus>
		<add title="首页" value="DefaultViewModel" />
		<add title="用户信息" value="UserInfoViewModel" />
		<add title="设置" value="SetViewModel" />
		<add title="监控" value="MonitorViewModel" />
		<add title="维护" value="MaintionViewModel" />
		<add title="配方" value="RecipeViewModel" />
		<add title="标定" value="CalibrationViewModel" />
	</menus>
</Tangdao>
```

菜单的配置读取

```
// 初始化
var menuProvider = new MenuProvider();

// 添加监控
menuProvider.Watch("header/user", item => 
{
    Console.WriteLine($"用户菜单变更: {item.Value}");
});

// 设置菜单值
menuProvider.Root["header/title"].Value = "首页";
menuProvider.Root["header/user"].Value = "张三";

// 获取菜单值
string username = menuProvider.Root["header/user"].Value;
```



#### 6、选择器

###### 1、时间选择器

获取当前时间，用法：

```js
xmlns:selector="clr-namespace:IT.Tangdao.Core.DaoSelectors;assembly=IT.Tangdao.Core"
```

```
 <TextBlock Text="{Binding Source={x:Static selector:DateTimeSelector.Instance}, Path=CurrentDate, StringFormat='yyyy-MM-dd HH:mm:ss'}" />
```

###### 2、文件选择器

###### 3、设备选择器

#### 7、自动生成器

可以自动生成虚假数据，用于平时调试

在WPF可以这样使用

```js
 public class MainWindowViewModel : BindableBase
 {
     private ObservableCollection<Student> _students;

     public ObservableCollection<Student> Students
     {
         get => _students;
         set => SetProperty(ref _students, value);
     }

     public MainWindowViewModel()
     {
         Loaded();
     }

     private void Loaded()
     {
         var generator = new DaoFakeDataGeneratorProvider<Student>();
         List<Student> randomStudents = generator.GenerateRandomData(10);
         Students = new ObservableCollection<Student>(randomStudents);
     }
 }
```

