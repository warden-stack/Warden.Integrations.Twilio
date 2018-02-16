# Warden Twilio Integration

![Warden](http://spetz.github.io/img/warden_logo.png)

**OPEN SOURCE & CROSS-PLATFORM TOOL FOR SIMPLIFIED MONITORING**

**[getwarden.net](http://getwarden.net)**

|Branch             |Build status                                                  
|-------------------|-----------------------------------------------------
|master             |[![master branch build status](https://api.travis-ci.org/warden-stack/Warden.Integrations.Twilio.svg?branch=master)](https://travis-ci.org/warden-stack/Warden.Integrations.Twilio)
|develop            |[![develop branch build status](https://api.travis-ci.org/warden-stack/Warden.Integrations.Twilio.svg?branch=develop)](https://travis-ci.org/warden-stack/Warden.Integrations.Twilio/branches)

**TwilioIntegration** can be used for sending the _SMS_ using the **[Twilio](https://twilio.com)**.
The required parameters are Account SID with Auth token for the account authentication and the default sender phone number e.g. _+1111222333_.

### Installation:

Available as a **[NuGet package](https://www.nuget.org/packages/Warden.Integrations.Twilio)**. 
```
Install-Package Warden.Integrations.Twilio
```

### Configuration:

 - **WithDefaultMessage()** - default body of the SMS.
 - **WithDefaultReceivers()** - default receiver(s) number(s).
 - **WithTwilioServiceProvider()** - provide a custom _ITwilioService_ which is responsible for the sending _SMS_.

**TwilioIntegration** can be configured by using the **TwilioIntegrationConfiguration** class or via the lambda expression passed to a specialized constructor. 

### Initialization:

In order to register and resolve **TwilioIntegration** make use of the available extension methods while configuring the **Warden**:

```csharp
var wardenConfiguration = WardenConfiguration
    .Create()
    .IntegrateWithTwilio("accountSid", "authToken", "+1111222333", cfg =>
    {
        cfg.WithDefaultMessage("Monitoring status")
           .WithDefaultReceivers("+1123456789");
    })
    .SetGlobalWatcherHooks((hooks, integrations) =>
    {
        hooks.OnStart(check => GlobalHookOnStart(check))
             .OnFailure(result => integrations.Twilio().SendSmsAsync("Monitoring errors have occured."))
    })
    //Configure watchers, hooks etc..
```

### Custom interfaces:
```csharp
public interface ITwilioService
{
    SendSmsAsync(string sender, string receiver, string message)
}
```

**ITwilioService** is responsible for sending the SMS. It can be configured via the *WithTwilioServiceProvider()* method. By default, it is based on the Twilio library.