## HELLO

欢迎使用由
[AmiableNext](https://github.com/heerheer/AmiableNext)
开发的`赛马AMI.RE`

## 关于

### 鸣谢

- Mirai
    - [mirai-http-api](https://github.com/project-mirai/mirai-api-http)
- MyQQ
- QQ频道(频道不使用赛马AMI,改为全新的ChuMoonBot)

## 多框架支持

### Mirai:终于等到你

#### 大致流程

- 安装mirai-http-api
- 配置 赛马AMI.RE 与 mirai-http-api相关接口与WebHook地址,验证,配置
- 启动赛马AMI.RE!

#### 具体步骤

> 不会安装的话可以在联系方式中寻找我。

##### 1️⃣安装mirai-http-api

这一步不需要我多讲了。

##### 2️⃣配置 mirai-http-api (重要)

1. 适配器:

```yaml
adapters:
  - webhook
  - http
```

2. 必须开启 singleMode

```yaml
singleMode: true
```

3. 配置verifyKey
4. 配置WebHook回调地址

这一步首先要更改**本体**`appsettings.json`的`Urls`选项
```json
"Urls": "http://localhost:8501"
```
紧接着，修改WebHook Adapter的目标地址
```yaml
adapterSettings:
  webhook:
    destinations: 
    - 'localhost:8501/MiraiEvent'
```
8501是默认的赛马AMI.RE监听端口

`/MiraiEvent'`是针对Mirai的事件监听路由


##### 3️⃣配置 赛马AMI.RE

> 具体参考[配置appsettings.json](#配置)

### MYQQ:最佳支持

## 配置

> **配置文件**位于exe同级目录下的`appsettings.json`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Urls": "http://localhost:8501",
  "Mode": "Mirai_HTTP_HOOK",
  "Mirai_HTTP_HOOK": {
    "ApiUrl": "http://localhost:8091",
    "AuthToken": "HEERSSSBBB"
  },
  "MyQQ_HTTP_API": {
    "ApiUrl": "http://localhost:5619/MyQQHTTPAPI",
    "AuthToken": "123123"
  }
}
```

appsettings.json 是由 .Net 生成的，服务于应用程序的配置文件。
对于使用AmibleNext开发的赛马AMI.RE,需要更改以下配置。

### Mode

`"Mode": "Mirai_HTTP_HOOK"`
Mode表示当前程序使用的API模式。

- Mirai_HTTP_HOOK
- MyQQ_HTTP_API

### {Mode}配置

例如

```j
  "Mirai_HTTP_HOOK": {
    "ApiUrl": "http://localhost:8091",
    "AuthToken": "HEERSSSBBB"
  }
```

代表对于`Mirai_HTTP_HOOK`模式的配置。

包括`ApiUrl`与`AuthToken`

针对Mirai来说，这里就要填写mirai-http-api中

http adapter 使用的 host:port 以及 verifyKey

## 指令

@ + .hrami 可以查看hrami指令集
例如
@bot .hrami

## 路线图

## 支持

## 更多