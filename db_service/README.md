<center>

# 数据库访问项目说明


![](https://img.shields.io/badge/.net%20core-v2.2-blue.svg)
![](https://img.shields.io/badge/dapper-v2.0.30-orange.svg)
![](https://img.shields.io/badge/rabbitmq-v3.8.0-purple.svg)
![](https://img.shields.io/badge/rabbit.client-v5.1.1-green.svg)
![](https://img.shields.io/badge/swagger-v4.0.1-brightgreen.svg)

</center>


### 说明

该项目为对数据库进行访问与操作。所有与数据库的操作都将在这个项目中执行。后期考虑加入 ***Radis*** 等缓存中间件 
  
**好处：** 统一对数据库访问的入口，独立数据库操作模块，便于后期维护与发布  
**弊端：** 小项目没必要对数据库的访问单独分离，人员不充足的情况下会加重运维人员的部署和维护  

### 介绍

* 使用 ***RESTful*** 风格定义接口
* 使用 ***swaggerui***，以便在开发过程中对接口的调试
* 使用 ***AutoFac*** 替代 ***.netcore*** 自带的依赖注入
* 使用 ***dapper*** 进行对数据库操作
* 支持对多类型数据库的支持，以及单类型多库结构数据库情况的支持  
* 使用 ***RabbitMQ*** 消息队列实现日志记录，记录请求数据包与响应数据包，同时定义全局错误拦截中间件，捕获未知异常日志  


### 快速上手  

暂无  


### 其它LINKS
* 我想查找详细的文档资料 => [Wiki/Documents/Manual 文档/手册](https://github.com/Joick/dotnet_core_project/wiki)
* 我要反馈问题 => [Issues](https://github.com/Joick/dotnet_core_project/issues)
