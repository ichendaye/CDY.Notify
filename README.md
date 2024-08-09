# 消息推送

﻿## 钉钉 推送
```
CDY.Notify.DingTalk dingTalk = new CDY.Notify.DingTalk("程序名称","钉钉的webhook地址","密钥");
dingTalk.SendTextMsgAsync(message, true); // 文本消息
dingTalk.SendErrorMsgAsync(Exception,true); // 异常消息

```

## Bark 推送

```
Bark bark = new Bark(key);
_ = bark.SendAsync("标题", "内容");
_ = bark.SendAsync("标题", "内容", "https://www.aaa.com/icon.png");

```
