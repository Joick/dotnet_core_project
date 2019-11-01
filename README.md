<div align="center">

# 说 明


![](https://img.shields.io/badge/.net%20core-v2.2-blue.svg)
[![](https://img.shields.io/badge/AutoFac-v4.9.4-red.svg)](https://autofac.org/)
[![](https://img.shields.io/badge/dapper-v2.0.30-green.svg)](https://github.com/StackExchange/Dapper)
[![](https://img.shields.io/badge/log4net-v2.0.8-yellow.svg)](http://logging.apache.org/log4net/)
[![](https://img.shields.io/badge/rabbitmq-v3.8.0-purple.svg)](https://www.rabbitmq.com/)
[![](https://img.shields.io/badge/rabbit.client-v5.1.1-orange.svg)](https://www.rabbitmq.com/dotnet.html)
[![](https://img.shields.io/badge/swagger-v4.0.1-brightgreen.svg)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

</div>


### 目的

该项目为本人学习.net core框架以及其他技术集成用项目，后期会考虑开发成一个完整的项目

### 项目结构

* **db_service**  
	该项目负责与数据库的访问与操作
* **service**  
	提供接口服务后端代码
* **task**  
	提供定时任务、监听任务等服务
* **web**  
	前台代码

### 项目中都有什么？

* 项目中使用 ***.net core 2.2*** 框架并在此基础上使用 [***AutoFac***](https://autofac.org/) 来控制依赖注入  
* 使用 [***Dapper***](https://github.com/StackExchange/Dapper) 来进行对数据库的访问操作的封装和使用  
* 数据库操作项目中实现同事对多类型数据库的支持，以及单类型多库结构数据库情况的支持  
* 日志记录功能则是使用了 [***log4net***](http://logging.apache.org/log4net/) 组件实现对日志的记录  
* 使用 [***RabbitMQ***](https://www.rabbitmq.com/) 消息队列实现对日志的记录功能
* 使用接口版本控制，避免后续v2迭代时对v1版本接口造成影响
* 使用 [***Swagger UI***](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)，以便在开发过程中对接口的调试


### 快速上手  

暂无  


### 分支说明
* **master**: 最新代码会在master，所以master是最新的，但是不保证稳定。提交记录可以参考，但不建议直接使用master分支
* **release**：是相对稳定的最新代码分支
* **其它分支**：根据开发需要，大的版本会以版本号为分支名，打一些临时分支。

### 最新Release
* [Release](https://github.com/Joick/dotnet_core_project/releases)  
* [如何部署](https://github.com/Joick/dotnet_core_project/wiki/deploy_manual_cn)  
* [如何使用](https://github.com/Joick/dotnet_core_project/wiki/user_manual_cn)  

### 其它LINKS
* 我想查找详细的文档资料 => [Wiki/Documents/Manual 文档/手册](https://github.com/Joick/dotnet_core_project/wiki)
* 我要反馈问题 => [Issues](https://github.com/Joick/dotnet_core_project/issues)
