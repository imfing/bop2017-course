// 引入bot构建器
var builder = require('botbuilder')

// 建立连接器(connector)，使其监听
var connector = new builder.ConsoleConnector().listen();

// 新建bot对象
var bot = new builder.UniversalBot(connector);

// 添加bot的对话
bot.dialog('/',[
    function(session) {
        builder.Prompts.text(session, '你的名字是？');
    },
    function(session, result){
        session.send('你好, '+ result.response);
    }
]);
