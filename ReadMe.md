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

```C#
 public IDaoEventAggregator _daoEventAggregator;

_daoEventAggregator.Publish<T>();

_daoEventAggregator.Subscribe<T>(Execute);

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

使用SelectNode读取xml节点

例如xml文档如下

```
<?xml version="1.0" encoding="utf-8"?>
<UserInfo>
  <Login Id="0">
    <UserName>Admin</UserName>
    <Password>2</Password>
    <Role>管理员</Role>
    <IsAdmin>True</IsAdmin>
    <IP>192.168.0.1</IP>
  </Login>
  <Register Id="1">
    <UserName>Ad</UserName>
    <Password>12</Password>
    <Role>普通用户</Role>
    <IsAdmin>False</IsAdmin>
    <IP>127.0.0.1</IP>
  </Register>
</UserInfo>
```

使用方式

```
// 正确调用（多节点必须指定索引）
var ip1 = _readService.Current[1].SelectNode("IP").Value;

// 错误调用（多节点未指定索引）
var ip2 = _readService.Current.SelectNode("IP").Value; 
// 返回错误："存在多个节点，请指定索引"

// 正确调用（单节点可不指定索引）
var ip3 = _readService.Current.SelectNode("IP").Value; 
```

优化繁琐的读取,不需要知道类的所有属性

```
  var readResult = _readService.Current.SelectNodes("ProcessItem", x => new ProcessItem
  {
      Name = x.Element("Name")?.Value,
      IsFeeding = (bool)x.Element("IsFeeding"),
      IsBoardMade = (bool)x.Element("IsBoardMade"),
      IsBoardCheck = (bool)x.Element("IsBoardCheck"),
      IsSeal = (bool)x.Element("IsSeal"),
      IsSafe = (bool)x.Element("IsSafe"),
      IsCharge = (bool)x.Element("IsCharge"),
      IsBlanking = (bool)x.Element("IsBlanking"),
  });

```

直接通过反射+泛型

```
 var readResult = _readService.Current.SelectNodes<ProcessItem>();
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
 public class Student
 {
     public int Id { get; set; }

     [DaoFakeDataInfo("姓名")] // 使用 DaoName 枚举生成姓名
     public string Name { get; set; }

     public int Age { get; set; }

     [DaoFakeDataInfo("爱好")] // 使用 DaoHobby 枚举生成爱好
     public string Hobby { get; set; }

     [DaoFakeDataInfo("城市")] // 使用 ChineseCities 数组生成城市
     public string Address { get; set; }
 }
```

#### 8、增加IRouter路由导航

```C#
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="*" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>

    <!--  路由视图容器  -->
    <ContentControl Grid.Row="0" Content="{Binding Router.CurrentView}" />

    <!--  导航控制  -->
    <StackPanel
        Grid.Row="1"
        HorizontalAlignment="Right"
        Orientation="Horizontal">

        <Button
            Margin="2"
            Command="{Binding GoBackCommand}"
            Content="◄"
            IsEnabled="{Binding Router.CanGoBack}"
            ToolTip="上一页" />
        <Button
            Margin="2"
            Command="{Binding GoForwardCommand}"
            Content="►"
            IsEnabled="{Binding Router.CanGoForward}"
            ToolTip="下一页" />

        <Button
            Margin="5"
            Command="{Binding GoToStudentListCommand}"
            Content="学生列表" />
        <Button
            Margin="5"
            Command="{Binding GoToDashboardCommand}"
            Content="仪表盘" />
    </StackPanel>
</Grid>
```

```C#
 public class MainWindowViewModel : BindableBase
 {
     public IRouter Router { get; }
     public ICommand GoBackCommand { get; set; }
     public ICommand GoForwardCommand { get; set; }
     public ICommand GoToStudentListCommand { get; set; }
     public ICommand GoToDashboardCommand { get; set; }
     public StudentViewModel StudentViewModel { get; } = new StudentViewModel();

     public MainWindowViewModel()
     {
         // var fake = new DaoFakeDataGeneratorProvider<Student>();
         // var students = fake.GenerateRandomData(300);
         // Students = new ObservableCollection<Student>(students);
         Router = new Router();

         // 注册页面
         Router.RegisterPage<StudentListViewModel>();
         Router.RegisterPage<DashboardViewModel>();

         // 设置命令
         GoBackCommand = new DelegateCommand(Router.GoBack, () => Router.CanGoBack);

         GoForwardCommand = new DelegateCommand(Router.GoForward, () => Router.CanGoForward);

         GoToStudentListCommand = new DelegateCommand<StudentListViewModel>(Router.NavigateTo<StudentListViewModel>);

         GoToDashboardCommand = new DelegateCommand<DashboardViewModel>(Router.NavigateTo<DashboardViewModel>);

         // 初始导航
         Router.NavigateTo<StudentListViewModel>("StudentListViewModel");
         //Router.NavigateTo<DashboardViewModel>("DashboardViewModel");
     }
 }
```

#### 9、时间轮

```
class Program
{
    static async Task Main(string[] args)
    {
        // 创建时间轮实例
        var timeWheel = new TimeWheel<Student>();
        timeWheel.Start(); // 启动时间轮
        
        // 创建几个学生
        var student1 = new Student { Id = 1, Name = "张三", Grade = 3 };
        var student2 = new Student { Id = 2, Name = "李四", Grade = 2 };
        var student3 = new Student { Id = 3, Name = "王五", Grade = 1 };
        
        Console.WriteLine($"当前时间: {DateTime.Now:HH:mm:ss}");
        
        // 添加任务：5秒后打印学生信息
        await timeWheel.AddTaskAsync(5, student1, async s => 
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} - 处理学生1: {s}");
            await Task.Delay(100); // 模拟异步工作
        });
        
        // 添加任务：10秒后升级学生年级
        await timeWheel.AddTaskAsync(10, student2, async s => 
        {
            s.Grade++;
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} - {s.Name}升级到{s.Grade}年级");
            await Task.Delay(100);
        });
        
        // 添加任务：15秒后发送通知
        await timeWheel.AddTaskAsync(15, student3, async s => 
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss} - 发送通知给{s.Name}的家长");
            await Task.Delay(100);
        });
        
        // 防止程序退出
        Console.ReadLine();
    }
}
```

#### 10、增加文本监控

在程序启动时，注册事件

```
 protected override void OnLaunch()
  {
      base.OnLaunch();
      // 启动监控服务
      var monitorService = Container.Get<IMonitorService>();
      monitorService.FileChanged += OnFileChanged;
      monitorService.StartMonitoring();
  }

  private void OnFileChanged(object sender, DaoFileChangedEventArgs e)
  {
      Logger.WriteLocal($"XML 文件变化: {e.FilePath}, 变化类型: {e.ChangeType}，变化详情：{e.ChangeDetails}，old:{e.OldContent},new:{e.NewContent}");
  }
```

注册代码

```
// 注册配置
 Bind<FileMonitorConfig>().ToFactory(container =>
 {
     return new FileMonitorConfig
     {
         MonitorRootPath = @"E:\IgniteDatas\",
         IncludeSubdirectories = true,
         MonitorFileTypes = new List<DaoFileType>
         {
             DaoFileType.Xml,
            // DaoFileType.Config,
           //  DaoFileType.Json
         },
         DebounceMilliseconds = 800,
         FileReadRetryCount = 3
     };
 }).InSingletonScope();

 // 注册监控服务
 Bind<IMonitorService>().To<FileMonitorService>().InSingletonScope();
```

