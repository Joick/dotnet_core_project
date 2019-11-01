<div align="center">

# 主接口服务项目说明

![](https://img.shields.io/badge/.net%20core-v2.2-blue.svg)
[![](https://img.shields.io/badge/AutoFac-v4.9.4-red.svg)](https://autofac.org/)
[![](https://img.shields.io/badge/rabbitmq-v3.8.0-purple.svg)](https://www.rabbitmq.com/)
[![](https://img.shields.io/badge/rabbitmq.client-v5.1.1-skyblue.svg)](https://www.rabbitmq.com/dotnet.html)
[![](https://img.shields.io/badge/swagger-v4.0.1-brightgreen.svg)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

</div>


### 说明

提供主要功能服务的接口支持

### 介绍

* 在 ***.net core*** 框架中加入 [***AutoFac***](https://autofac.org/) 来替代*core*框架内自带的依赖注入   
* 使用 [***RabbitMQ***](https://www.rabbitmq.com/dotnet.html) 消息队列实现对日志的记录功能
* 服务接口使用版本控制，避免后续 ***v2*** 迭代，规避与 ***v1*** 版接口的耦合
* 使用 [***Swagger UI***](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)，以便在开发过程中对接口的调试  
* 项目中自定义注册请求和响应记录日志记录中间件以及全局报错拦截中间件


### 快速上手  

暂无  

### 其它Links
* 我想查找详细的文档资料 => [Wiki/Documents/Manual 文档/手册](https://github.com/Joick/dotnet_core_project/wiki)
* 我要反馈问题 => [Issues](https://github.com/Joick/dotnet_core_project/issues)
