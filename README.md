# DDD「领域驱动设计」分层架构

基于 DDD 传统分层架构实现。

## 前言

这个分层架构是工作中项目正在使用的分层架构，使用了一段时间发现受益匪浅，所以整理好我对该分层架构的一些理解分享给大家，我对于该分层架构还处于学习阶段理解有误的地方请指出。本次会以一个案例来说明各个分层的作用以及他们之间的调用关系，还有本次的重点不在于`DDD`，因为这个我还未能完全理解，当然避免不了中间会涉及`DDD`的一些概念。

## DDD 简单介绍

`DDD` 什么？为什么使用 `DDD` ？

关于这个问题有兴趣的可以自行百度，我相信网络上已经有大量的文章来说明这几个问题。我目前的理解是“业务”，是为了应对现在复杂和多变的业务，是一种开发理念。

这里我就以一个小故事描述吧，有一天你接到任务要实现一个修改用户的功能，非常简单。使用传统三层架构我们会怎么写？

1. 先在 `DAL` 层添加 `UserDAL` 然后实现一个 `Update(UserEntity user)` 方法

2. 接着在 `BLL` 中添加一个 `UserBLL` 在实现一个 `Update(string email,string pwd ...)` 方法。

3. `UI` 层在调用，OK 完成任务下班回家。

接着你接到一个新的需求就是：需要增加用户修改信息的记录。

你立马在 `BLL`的 `Update` 的方法里增加的用户修改信息的操作记录，完成需求。

过了一段时间又来了一个需求：用户改了信息需要通知到管理员，并且用户每天只能修改 3 次信息。

好了之后又经历了几波需求，你的代码也在不断的增加和变化，有一天你接收新的项目或者离开了，那么接收你项目的人完全不清楚这里的业务情况。因为 `Update` 方法并没有直接的反应出里的业务情况，代码目的不明确。代码变得难以维护。

那么在 `DDD` 里这些应该怎么做呢？

1. 首先在方法的命名上做出更改既然业务是修改信息那么命名应该是 `Modify(string email,string pwd ...)`

2. 将用户修改信息的记录代码放在 `DomainService`（领域服务） 中，当然这里的类、方法命名要直接的反应出业务情况，如：`RecordUserModifyDomainService`。

3. 对应的通知管理员的代码也应该放入 `DomainService` 中，`DomainService` 应该尽量简单一般只做一件事情。

## 分层架构图

![DDD 流程图](/doc/images/DDD_1.png)

下面是关于 DDD 分层的一些描述，摘抄至之前看过的一片文章。
> * Presentation 为表示层，负责向用户显示信息和解释用户命令。这里指的用户可以是另一个计算机系统，不一定是使用用户界面的人。
>
> * Application 为应用层，定义软件要完成的任务，并且指挥表达领域概念的对象来解决问题。这一层所负责的工作对业务来说意义重大，也是与其它系统的应用层进行交互的必要渠道。应用层要尽量简单，不包含业务规则或者知识，而只为下一层中的领域对象协调任务，分配工作，使它们互相协作。它没有反映业务情况的状态，但是却可以具有另外一种状态，为用户或程序显示某个任务的进度。
>
> * Domain 为领域层（或模型层），负责表达业务概念，业务状态信息以及业务规则。尽管保存业务状态的技术细节是由基础设施层实现的，但是反映业务情况的状态是由本层控制并且使用的。领域层是业务软件的核心，领域模型位于这一层。
>
> * Infrastructure 层为基础实施层，向其他层提供通用的技术能力：为应用层传递消息，为领域层提供持久化机制，为用户界面层绘制屏幕组件，等等。基础设施层还能够通过架构框架来支持四个层次间的交互模式。

## 说明

如上图每个层中其实对应着具体的项，下面将对每个项进行说明。

1. `Domain` 层分为：`Domain` 、`DomainService` 和 `IDomainService`。
 * 首先 `Domain` 中包含有 `Entity` 和 `IRepository` ，`Entity` 是你的实体一般对于数据库表但是在某些情况下你也可以冗余一些字段。`IRepository` 仓储的方法的定义，该层不会有具体的实现。
 * `DomainService` 和 `IDomainService`，`IDomainService` 只是负责表达业务的概念，`DomainService` 里才是具体业务逻辑代码。在这一层的代码命名上需要注意，我们的命名一般要能直接描述出该代码业务的功能。这里可以参考 `DDD` 的几个概念：通用语言、领域。

2. `Infrastructure` 层分为：`Repository` 和 `CrossCutting`：
 * `Repository` 里面就是 `Domain` 里 `IRepository` 的具体实现。项目中 RepositoryExtensions.cs 是一个扩展类，将所有的仓储注入容器中，方便我们在项目中使用 `DI`（依赖注入）。
 * `CrossCutting` 主要是提供一些各个层通用的东西，如一些枚举、扩展方法、工具类等等。

3. `Application` 层分为：`Application` 和 `ApplicationContract`。
 * `ApplicationContract` 里主要包含 `DTO`、`ViewModel`、`IXXXService`。`DTO` 是数据传输对象，主要负责给展现层提供展示数据，`DTO` 里应该只有值类型存在，当然根据具体情况也可存在其他的 `DTO` 。`ViewModel` 用于展现层传入的模型，简单的说 `DTO` 输出，`ViewModel` 输入。`IXXXService` 就是应用层的方法定义。
 * `Application` 里面主要是用于 实现 `ApplicationContract` 里的 `IXXXService`，还有 `Entity` 和 `DTO` 的映射也属于该层的工作。ApplicationExtensions.cs 扩展方法是用于实现 `DI`。

4. `Presentation` 层里目前只有一个 WebAPI。展现层的代码一般有：对传入模型的校验。

## 案例

本次以一个用户注册的流程为案例，来简单说明如何使用该分层架构进行项目开发。

1. 首先在 `Domain` 中建一个 UserEntity，有 Id、Mobile、Name、Age、RegisterDateTime 属性。接着建立 IUserRepository，编写需要定义的方法，这里我定义了一个 GetByMobile(string mobile) 方法。

``` cs
[Table(Name = "User")]
public class UserEntity
{
    [Column(IsIdentity = true)]
    public Guid Id { get; set; }

    public string Mobile { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public DateTime RegisterDateTime { get; set; } = DateTime.Now;
}

public interface IUserRepository : IBasicRepository<UserEntity, Guid>
{
    Task<UserEntity> GetByMobileAsync(string mobile);
}
```

IBasicRepository 是使用了 [FreeSql](https://github.com/2881099/FreeSql)，你们可以自己实现。

2. 然后在 Repository 中建 UserRepository 类，该类继承 IUserRepository 并且实现该接口的所有方法。

``` cs
public class UserRepository : GuidRepository<UserEntity>, IUserRepository
{
    public UserRepository(IFreeSql freeSql)
        : base(freeSql)
    {
    }

    #region Implementation of IUserRepository

    public async Task<UserEntity> GetByMobileAsync(string mobile)
    {
        return await this.Where(u => u.Mobile == mobile).FirstAsync();
    }

    #endregion
}
```

3. 仓储基本好了后就是 `Application` ，首先需要在 ApplicationContract 中建 UsesDTO，根据业务情况你也可以建 UserSimpleDTO 、UserDetailDTO。`DTO` 里包含你需要返回的数据，我这里有 Id、Name、Mobile、Age、ProfilePhotoSrc（头像地址根据 Id 拼接，这里我用 imgage/Id.png 的格式）。

``` cs
public class UserDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Mobile { get; set; }

    public int Age { get; set; }

    public string ProfilePhotoSrc { get; set; }
}
```

4. 添加好 UserDTO 后，然后添加 IUserService.cs 接口，接着在 Application 的 Service 中添加对应的 UserService，并且 UserService 继承 IUserService。

``` cs
public interface IUserService
{
    /// <summary>
    /// 用户注册
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="mobile">手机号</param>
    /// <param name="age">年龄</param>
    /// <returns></returns>
    Task<bool> Register(string userName, string mobile, int age);

    List<UserDTO> GetList();
}

 public class UserService : IUserService
{
    readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #region Implementation of IUserService

    /// <summary>
    /// 用户注册
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="mobile">手机</param>
    /// <param name="age">年龄</param>
    /// <returns></returns>
    public async Task<bool> Register(string userName, string mobile, int age)
    {
        var userEnity = await _userRepository.GetByMobileAsync(mobile);

        if (userEnity != null)
        {
            return false;
        }

        var addUserEntity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Age = age,
            Name = userName,
            Mobile = mobile
        };

        return await _userRepository.InsertAsync(addUserEntity) != null;
    }

    public List<UserDTO> GetList()
    {
        return _userRepository.Select
            .ToList().ToDTOList();
    }

    #endregion
}
```

5. UserServcie 是对应展现层的控制器 UserController ---> IUserService。

6. 最后展现层的 WebAPI 只需要注入 IUserService，就可以开心的使用了。

``` cs
[HttpPost]
public async Task<IActionResult> Post()
{
    var second = DateTime.Now.Second.ToString("00");
    bool isSuccess = await _userService.Register("Wigor", $"188888888{second}", 22);

    return Ok(isSuccess);
}
```

就这样这个简单的案例就完成了，你可以参考着上面 说明 对比着去看看，当然这里有一些东西并没有体现，如 DomainServie，如果按照 DDD 来说还有 值对象、聚合、通用语言……，对于「通用语言」的话其实上面的小故事就体现出了一点。
