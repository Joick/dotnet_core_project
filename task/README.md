<center>

# 任务项目说明


![](https://img.shields.io/badge/.net%20core-v2.2-blue.svg)
[![](https://img.shields.io/badge/log4net-v2.0.8-yellow.svg)](http://logging.apache.org/log4net/)
[![](https://img.shields.io/badge/rabbitmq-v3.8.0-purple.svg)](https://www.rabbitmq.com/)
[![](https://img.shields.io/badge/rabbit.client-v5.1.1-green.svg)](https://www.rabbitmq.com/dotnet.html)

</center>


### 说明

该项目为提供后台静默服务而开发项目。目前包括消息队列，日志系统。将功能模块独立出来，让主服务项目耦合不会太大，便于后期分模块开发和运维

### 介绍

* 使用 ***.net core*** 框架下的 ***控制台应用程序***来开发自动服务
* 使用 [***log4net***](http://logging.apache.org/log4net/) 组件实现对日志的记录  
* 使用 [***RabbitMQ***](https://www.rabbitmq.com/) 消息队列实现对日志的记录功能
* 在部署服务时，windows环境下推荐使用 [***NSSM***](http://www.nssm.cc/) 将项目发布成windows服务（linux环境下还在摸索中，后期会更新）
* （后期的定时任务也会加入这项目中，在考虑后期如何横向扩展）


### 快速上手  

暂无  

### 其它LINKS
* 我想查找详细的文档资料 => [Wiki/Documents/Manual 文档/手册](https://github.com/Joick/dotnet_core_project/wiki)
* 我要反馈问题 => [Issues](https://github.com/Joick/dotnet_core_project/issues)
