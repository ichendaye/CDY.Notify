## 钉钉消息
```
CDY.Notify.DingTalk dingTalk = new CDY.Notify.DingTalk("程序名称","钉钉的webhook地址","密钥");
dingTalk.SendTextMsgAsync(message, true); // 文本消息
dingTalk.SendErrorMsgAsync(Exception,true); // 异常消息
```
