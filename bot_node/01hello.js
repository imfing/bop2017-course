// 引入bot构建器
var builder = require('botbuilder')

// 建立连接器(connector)，使其监听
var connector = new builder.ConsoleConnector().listen();

// 新建bot对象
var bot = new builder.UniversalBot(connector);

// 添加bot的对话
bot.dialog('/', function (session) {
    session.send('你好，主人!');
});